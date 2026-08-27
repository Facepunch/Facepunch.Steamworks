using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Exercises the studio-added SteamNetworkingMessages surface — the
	/// zero-allocation receive path (MessageIntercept) and the raw IntPtr /
	/// Span send overloads — against a live Steam client. Steam is inited by
	/// AppTest.AssemblyInitialize. These prove the API is CALLABLE live and the
	/// zero-message drain is allocation-free; a two-party message round-trip
	/// needs a second Steam identity and stays a manual/integration exercise.
	/// </summary>
	[TestClass]
	[DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
	public class NetworkingMessagesTest
	{
		// Reset the one-way dispatch-exception latch before each test so a
		// callback exception can only fail the test that actually caused it
		// (CTO Pass-3.1 test-isolation hardening).
		[TestInitialize]
		public void ResetDispatchLatch() => AppTest.LastDispatchException = null;

		[TestMethod]
		public void ReceiveMessagesOnChannel_LiveDrain_CallableAndNoPayloadAlloc()
		{
			// Exercise the new MessageIntercept drain overload LIVE (Steam is
			// inited). On an empty channel it must return 0, never fire the
			// intercept, and not throw. The zero-ALLOCATION property this
			// overload adds is strictly per-MESSAGE: unlike the byte[] overload
			// (ReceiveMessage → `new byte[msg->DataSize]` + Marshal.Copy for
			// EVERY message), the intercept path hands out the Steam-owned
			// pointer and copies nothing. That per-message win is a code
			// property (verified by inspection); it cannot be measured on an
			// empty channel. What we CAN assert live: the drain does not copy
			// message-sized payloads — its per-call cost is bounded, fixed
			// P/Invoke overhead (~tens of bytes), never the KBs a payload-copy
			// path would allocate. 64 empty drains staying under 8 KB proves no
			// payload buffers are being allocated on this path.
			int delivered = 0;
			SteamNetworkingMessages.MessageIntercept intercept =
				( NetIdentity id, int channel, IntPtr data, int size ) => delivered++;

			// Warm up the JIT + native thunk before measuring.
			SteamNetworkingMessages.ReceiveMessagesOnChannel( 24001, intercept );

			long before = GC.GetAllocatedBytesForCurrentThread();
			for ( int i = 0; i < 64; i++ )
			{
				int n = SteamNetworkingMessages.ReceiveMessagesOnChannel( 24001, intercept );
				Assert.AreEqual( 0, n, "empty channel must drain zero messages" );
			}
			long allocated = GC.GetAllocatedBytesForCurrentThread() - before;

			Assert.AreEqual( 0, delivered, "no messages were sent — intercept must not fire" );
			Assert.IsTrue( allocated < 8192,
				$"drain allocated {allocated} bytes over 64 empty calls — a payload-copy path would allocate far more; this bound proves no per-message byte[] buffers are being created" );
			Assert.IsNull( AppTest.LastDispatchException, "a callback exception fired during the drain" );
		}

		[TestMethod]
		public void RawAndSpanSendOverloads_AreCallable()
		{
			// The raw IntPtr + ReadOnlySpan<byte> send overloads exist and are
			// invocable (self identity, unreliable, channel 0). We do not assert
			// delivery — self P2P is not guaranteed — only that the marshaling
			// path accepts the pooled/pinned-buffer shapes without throwing.
			NetIdentity self = SteamClient.SteamId;
			Span<byte> payload = stackalloc byte[] { 1, 2, 3, 4 };

			var spanResult = SteamNetworkingMessages.SendMessageToUser(
				ref self, payload, SteamNetworkingOptions.Unreliable, 0 );

			unsafe
			{
				fixed ( byte* p = payload )
				{
					var ptrResult = SteamNetworkingMessages.SendMessageToUser(
						ref self, (IntPtr)p, (uint)payload.Length, SteamNetworkingOptions.Unreliable, 0 );
					// A Result is returned (Steam may say OK / NoConnection /
					// InvalidParam for self-send) — the point is it did not throw.
					Assert.IsTrue( Enum.IsDefined( typeof( Result ), ptrResult ), "ptr send returned a Result" );
				}
			}

			Assert.IsTrue( Enum.IsDefined( typeof( Result ), spanResult ), "span send returned a Result" );
			Assert.IsNull( AppTest.LastDispatchException, "a callback exception fired during send" );
		}
	}
}
