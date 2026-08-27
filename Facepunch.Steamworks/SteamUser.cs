using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Functions for accessing and manipulating Steam user information.
	/// This is also where the APIs for Steam Voice are exposed.
	/// </summary>
	public class SteamUser : SteamClientClass<SteamUser>
	{
		internal static ISteamUser Internal => Interface as ISteamUser;

		internal override bool InitializeInterface( bool server )
		{
			SetInterface( server, new ISteamUser( server ) );
			if ( Interface.Self == IntPtr.Zero ) return false;

			InstallEvents();

			richPresence = new Dictionary<string, string>();
			SampleRate = OptimalSampleRate;

			return true;
		}

		static Dictionary<string, string> richPresence;

		internal static void InstallEvents()
		{
			Dispatch.Install<SteamServersConnected_t>( x => OnSteamServersConnected?.Invoke() );
			Dispatch.Install<SteamServerConnectFailure_t>( x => OnSteamServerConnectFailure?.Invoke() );
			Dispatch.Install<SteamServersDisconnected_t>( x => OnSteamServersDisconnected?.Invoke() );
			Dispatch.Install<ClientGameServerDeny_t>( x => OnClientGameServerDeny?.Invoke() );
			Dispatch.Install<LicensesUpdated_t>( x => OnLicensesUpdated?.Invoke() );
			Dispatch.Install<ValidateAuthTicketResponse_t>( x => OnValidateAuthTicketResponse?.Invoke( x.SteamID, x.OwnerSteamID, x.AuthSessionResponse ) );
			Dispatch.Install<MicroTxnAuthorizationResponse_t>( x => OnMicroTxnAuthorizationResponse?.Invoke( x.AppID, x.OrderID, x.Authorized != 0 ) );
			Dispatch.Install<GameWebCallback_t>( x => OnGameWebCallback?.Invoke( x.URLUTF8() ) );
			Dispatch.Install<GetAuthSessionTicketResponse_t>( x => OnGetAuthSessionTicketResponse?.Invoke( x ) );
			Dispatch.Install<GetTicketForWebApiResponse_t>( x => OnGetTicketForWebApiResponse?.Invoke( x ) );
			Dispatch.Install<DurationControl_t>( x => OnDurationControl?.Invoke( new DurationControl { _inner = x } ) );
		}

		/// <summary>
		/// Invoked when a connections to the Steam back-end has been established.
		/// This means the Steam client now has a working connection to the Steam servers. 
		/// Usually this will have occurred before the game has launched, and should only be seen if the 
		/// user has dropped connection due to a networking issue or a Steam server update.
		/// </summary>
		public static event Action OnSteamServersConnected;

		/// <summary>
		/// Invoked when a connection attempt has failed.
		///	This will occur periodically if the Steam client is not connected, 
		///	and has failed when retrying to establish a connection.
		/// </summary>
		public static event Action OnSteamServerConnectFailure;

		/// <summary>
		/// Invoked when the client has lost connection to the Steam servers.
		/// Real-time services will be disabled until a matching OnSteamServersConnected has been posted.
		/// </summary>
		public static event Action OnSteamServersDisconnected;

		/// <summary>
		/// Sent by the Steam server to the client telling it to disconnect from the specified game server, 
		/// which it may be in the process of or already connected to.
		/// The game client should immediately disconnect upon receiving this message.
		/// This can usually occur if the user doesn't have rights to play on the game server.
		/// </summary>
		public static event Action OnClientGameServerDeny;

		/// <summary>
		/// Invoked whenever the users licenses (owned packages) changes.
		/// </summary>
		public static event Action OnLicensesUpdated;

		/// <summary>
		/// Invoked when an auth ticket has been validated. 
		/// The first parameter is the <see cref="SteamId"/> of this user
		/// The second is the <see cref="SteamId"/> that owns the game, which will be different from the first 
		/// if the game is being borrowed via Steam Family Sharing.
		/// </summary>
		public static event Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse;

		/// <summary>
		/// Used internally for <see cref="GetAuthSessionTicketAsync(NetIdentity, double)"/>.
		/// </summary>
		internal static event Action<GetAuthSessionTicketResponse_t> OnGetAuthSessionTicketResponse;

		/// <summary>
		/// Used internally for <see cref="GetAuthTicketForWebApiAsync(string, double)"/>.
		/// </summary>
		internal static event Action<GetTicketForWebApiResponse_t> OnGetTicketForWebApiResponse;

		/// <summary>
		/// Invoked when a user has responded to a microtransaction authorization request.
		/// ( appid, orderid, user authorized )
		/// </summary>
		public static event Action<AppId, ulong, bool> OnMicroTxnAuthorizationResponse;

		/// <summary>
		/// Sent to your game in response to a steam://gamewebcallback/(appid)/command/stuff command from a user clicking a 
		/// link in the Steam overlay browser.
		/// You can use this to add support for external site signups where you want to pop back into the browser after some web page 
		/// signup sequence, and optionally get back some detail about that.
		/// </summary>
		public static event Action<string> OnGameWebCallback;

		/// <summary>
		/// Sent for games with enabled anti indulgence / duration control, for enabled users.
		/// Lets the game know whether persistent rewards or XP should be granted at normal rate, 
		/// half rate, or zero rate.
		/// </summary>
		public static event Action<DurationControl> OnDurationControl;

		static bool _recordingVoice;

		/// <summary>
		/// Turns microphone capture on and off. Setting this to <see langword="true"/> makes Steam start
		/// buffering compressed voice from the user's chosen recording device; setting it to
		/// <see langword="false"/> stops it. Steam transmits nothing on your behalf - you drain the buffer
		/// with <see cref="HasVoiceData"/> / <see cref="ReadVoiceData"/> and send the bytes over your own
		/// transport.
		/// </summary>
		/// <value>
		/// <see langword="true"/> while capture is running. This is the last value written through this
		/// property rather than a query of Steam's state - Valve exposes no "am I recording" call, so the
		/// cached field is the only answer available.
		/// </value>
		/// <remarks>
		/// <para>
		/// Capture does not stop by itself. This is a latch, not a per-frame flag: once set to
		/// <see langword="true"/> the microphone stays open until something sets it back to
		/// <see langword="false"/>. Push-to-talk therefore has to clear it on key release, and so does
		/// every early-out path - exception, disconnect, level change, test teardown. The test suite in
		/// this repository demonstrates the failure mode: <c>UserTest.GetVoice</c> sets it
		/// <see langword="true"/> and never clears it, leaving the microphone recording for the remaining
		/// lifetime of the process.
		/// </para>
		/// <para>
		/// Valve notes that stopping is not instantaneous - because people release push-to-talk keys early,
		/// the system keeps recording for a short time after the stop call. Keep calling
		/// <see cref="ReadVoiceData"/> after clearing this flag until it stops returning data, otherwise you
		/// truncate the tail of the last utterance.
		/// </para>
		/// </remarks>
		public static bool VoiceRecord
		{
			get => _recordingVoice;
			set
			{
				_recordingVoice = value;
				if ( value ) Internal.StartVoiceRecording();
				else Internal.StopVoiceRecording();
			}
		}


		/// <summary>
		/// Whether Steam currently holds buffered compressed voice waiting to be read. Poll this before
		/// calling <see cref="ReadVoiceData"/> so you do no work while the user is silent.
		/// </summary>
		/// <value>
		/// <see langword="true"/> when at least one byte of compressed voice is available.
		/// <see langword="false"/> covers both "nothing captured" and every error case - recording was
		/// never started, voice is restricted for this user, the interface is not initialized - because the
		/// underlying <c>EVoiceResult</c> is collapsed to a <see langword="bool"/> here. Those cases are not
		/// distinguishable through this property.
		/// </value>
		/// <remarks>
		/// This stays <see langword="false"/> forever if <see cref="VoiceRecord"/> was never set to
		/// <see langword="true"/>, with no exception and no log to tell you why. Only the compressed size is
		/// queried: Valve documents the uncompressed path as deprecated and warns that the available-size
		/// query is not precisely accurate when an uncompressed size is requested, which is why this binding
		/// never asks for one.
		/// </remarks>
		public static bool HasVoiceData
		{
			get
			{
				uint szCompressed = 0, deprecated = 0;

				if ( Internal.GetAvailableVoice( ref szCompressed, ref deprecated, 0 ) != VoiceResult.OK )
					return false;

				return szCompressed > 0;
			}
		}

		static byte[] readBuffer = new byte[1024*128];

		/// <summary>
		/// Drains the compressed voice Steam has buffered into <paramref name="stream"/>. The bytes are in
		/// Steam's own opaque codec format: transmit them to the other players verbatim and run
		/// <c>DecompressVoice</c> on the receiving end. They are not playable audio, the format is
		/// undocumented, and it is not stable - do not parse, resample, or persist them.
		/// </summary>
		/// <param name="stream">
		/// Destination for the compressed bytes. Written at its current position and never rewound,
		/// flushed, or closed, so a <c>MemoryStream</c> accumulates successive frames back to back. Must not
		/// be <see langword="null"/> when there is data available.
		/// </param>
		/// <returns>
		/// Number of bytes written. <c>0</c> means either "no voice available" or "the read failed" - the
		/// two are not distinguishable, and nothing is written to <paramref name="stream"/> in either case.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Valve asks for this at least once per frame, and preferably every few milliseconds, to keep
		/// microphone latency low. Calling it fewer than about four times a second can leave gaps in the
		/// captured stream.
		/// </para>
		/// <para>
		/// One 128 KB <see langword="static"/> buffer is shared between this method and
		/// <see cref="ReadVoiceDataBytes"/>, so neither is safe to call from two threads at once. If Steam
		/// has more than 128 KB buffered, the underlying call reports a buffer-too-small result, which this
		/// binding collapses into a return of <c>0</c>.
		/// </para>
		/// </remarks>
		public static unsafe int ReadVoiceData( System.IO.Stream stream )
		{
			if ( !HasVoiceData )
				return 0;

			uint szWritten = 0;
			uint deprecated = 0;

			fixed ( byte* b = readBuffer )
			{
				if ( Internal.GetVoice( true, (IntPtr)b, (uint)readBuffer.Length, ref szWritten, false, IntPtr.Zero, 0, ref deprecated, 0 ) != VoiceResult.OK )
					return 0;
			}

			if ( szWritten == 0 )
				return 0;

			stream.Write( readBuffer, 0, (int) szWritten );

			return (int) szWritten;
		}

		/// <summary>
		/// Convenience form of <see cref="ReadVoiceData"/> that hands back a freshly allocated array instead
		/// of writing into a stream. It allocates on every call that produces data, so prefer
		/// <see cref="ReadVoiceData"/> on the per-frame capture path.
		/// </summary>
		/// <returns>
		/// A new array sized exactly to the compressed bytes read, or <see langword="null"/> when there was
		/// nothing to read or the read failed. It never returns a zero-length array, so a
		/// <see langword="null"/> check is sufficient - but that also means silence and error look
		/// identical.
		/// </returns>
		/// <remarks>
		/// Shares the same single 128 KB <see langword="static"/> scratch buffer as
		/// <see cref="ReadVoiceData"/>; the returned array is a copy, but the read itself is not
		/// thread-safe.
		/// </remarks>
		public static unsafe byte[] ReadVoiceDataBytes()
		{
			if ( !HasVoiceData )
				return null;

			uint szWritten = 0;
			uint deprecated = 0;

			fixed ( byte* b = readBuffer )
			{
				if ( Internal.GetVoice( true, (IntPtr)b, (uint)readBuffer.Length, ref szWritten, false, IntPtr.Zero, 0, ref deprecated, 0 ) != VoiceResult.OK )
					return null;
			}

			if ( szWritten == 0 )
				return null;

			var arry = new byte[szWritten];
			Array.Copy( readBuffer, 0, arry, 0, szWritten );
			return arry;
		}

		static uint sampleRate = 48000;

		/// <summary>
		/// The sample rate requested from Steam's decoder by every <c>DecompressVoice</c> overload. Set this
		/// to your audio output device's native rate when that sounds better than Steam's own optimal rate.
		/// </summary>
		/// <value>
		/// Samples per second. Valve's decoder accepts 11025 to 48000. The field starts at 48000 and is
		/// overwritten with <see cref="OptimalSampleRate"/> when the interface is initialized, so reading it
		/// before initialization gives you 48000 rather than the machine's actual optimum.
		/// </value>
		/// <remarks>
		/// <para>
		/// This is a decode-side setting only. Capture is unaffected - <see cref="ReadVoiceData"/> returns
		/// compressed data whose internal rate is Steam's business - and changing this partway through a
		/// stream changes the rate of the audio you produce mid-stream.
		/// </para>
		/// <para>
		/// The range guard in the setter tests the value already stored rather than the incoming one, so an
		/// out-of-range assignment is accepted silently. Treat 11025-48000 as a contract you have to honour
		/// yourself: a rate outside it fails inside Steam, and the <c>DecompressVoice</c> overloads report
		/// that only as a return of <c>0</c>.
		/// </para>
		/// </remarks>
		/// <exception cref="System.Exception">
		/// Thrown when the rate is outside 11025-48000 - but see the remarks: the check reads the stored
		/// value, so in practice it only fires when the previously stored rate was already out of range.
		/// </exception>
		public static uint SampleRate
		{
			get => sampleRate;
			
			set
			{
				if ( SampleRate < 11025 ) throw new System.Exception( "Sample Rate must be between 11025 and 48000" );
				if ( SampleRate > 48000 ) throw new System.Exception( "Sample Rate must be between 11025 and 48000" );

				sampleRate = value;
			}
		}

		/// <summary>
		/// The native sample rate of Steam's voice decompressor. Decoding at this rate costs the least CPU,
		/// but Valve notes the audible result depends on how well your audio device copes with lower rates -
		/// using your output device's native rate (usually 48000 or 44100) often sounds better.
		/// </summary>
		/// <value>
		/// Samples per second, queried from Steam on every read rather than cached. Valve documents no
		/// failure value for this call; the test suite in this repository asserts it is non-zero, and this
		/// binding assigns it to <see cref="SampleRate"/> at initialization without validating it.
		/// </value>
		public static uint OptimalSampleRate => Internal.GetVoiceOptimalSampleRate();


		/// <summary>
		/// Decodes compressed voice received from another player back into playable audio. The output is raw
		/// single-channel 16-bit PCM at <see cref="SampleRate"/>.
		/// </summary>
		/// <param name="input">
		/// Stream holding the compressed bytes exactly as <see cref="ReadVoiceData"/> produced them on the
		/// sender. Read from its current position to the end.
		/// </param>
		/// <param name="length">
		/// Size in bytes of the compressed payload. This is used both to size the staging buffer and as the
		/// length handed to Steam - it is never derived from how much was actually copied out of
		/// <paramref name="input"/>. Passing a value larger than the stream holds means the tail handed to
		/// the decoder is stale content from a pooled buffer.
		/// </param>
		/// <param name="output">
		/// Destination for the decoded PCM. Written at its current position; not rewound or flushed.
		/// </param>
		/// <returns>
		/// Bytes of PCM written to <paramref name="output"/>. <c>0</c> means the decode failed or produced
		/// nothing; the specific reason - corrupted data, unsupported codec, output buffer too small - is
		/// discarded by this binding.
		/// </returns>
		/// <remarks>
		/// Both the staging buffer and the 64 KB output buffer come from a shared pool, so this is not safe
		/// to call concurrently with itself or with the other <c>DecompressVoice</c> overloads. More than
		/// 64 KB of decoded audio in one call cannot be returned: Valve's contract is that the decoder
		/// reports the size it needed and returns a buffer-too-small result, which becomes a return of
		/// <c>0</c> here with the required size lost.
		/// </remarks>
		public static unsafe int DecompressVoice( System.IO.Stream input, int length, System.IO.Stream output )
		{
			var from = Helpers.TakeBuffer( length );
			var to = Helpers.TakeBuffer( 1024 * 64 );

			//
			// Copy from input stream to a pinnable buffer
			//
			using ( var s = new System.IO.MemoryStream( from ) )
			{
				input.CopyTo( s );
			}

			uint szWritten = 0;

			fixed ( byte* frm = from )
			fixed ( byte* dst = to )
			{
				if ( Internal.DecompressVoice( (IntPtr) frm, (uint) length, (IntPtr)dst, (uint)to.Length, ref szWritten, SampleRate ) != VoiceResult.OK )
					return 0;
			}

			if ( szWritten == 0 )
				return 0;

			//
			// Copy to output buffer
			//
			output.Write( to, 0, (int)szWritten );
			return (int)szWritten;
		}

		/// <summary>
		/// Decodes a compressed voice payload you already hold as an array. Same decode as the stream
		/// overload, without the staging copy.
		/// </summary>
		/// <param name="from">
		/// The compressed bytes exactly as <see cref="ReadVoiceDataBytes"/> produced them on the sender. The
		/// whole array is decoded - the length passed to Steam is <c>from.Length</c> and nothing else - so
		/// slice an oversized receive buffer down before calling.
		/// </param>
		/// <param name="output">Destination for the decoded 16-bit PCM. Written at its current position.</param>
		/// <returns>
		/// Bytes of PCM written to <paramref name="output"/>, or <c>0</c> on any failure. Failure reasons
		/// are not surfaced.
		/// </returns>
		/// <remarks>
		/// The 64 KB output staging buffer is pooled and shared with the other <c>DecompressVoice</c>
		/// overloads, so concurrent calls corrupt each other.
		/// </remarks>
		public static unsafe int DecompressVoice( byte[] from, System.IO.Stream output )
		{
			var to = Helpers.TakeBuffer( 1024 * 64 );

			uint szWritten = 0;

			fixed ( byte* frm = from )
			fixed ( byte* dst = to )
			{
				if ( Internal.DecompressVoice( (IntPtr)frm, (uint)from.Length, (IntPtr)dst, (uint)to.Length, ref szWritten, SampleRate ) != VoiceResult.OK )
					return 0;
			}

			if ( szWritten == 0 )
				return 0;

			//
			// Copy to output buffer
			//
			output.Write( to, 0, (int)szWritten );
			return (int)szWritten;
		}

		/// <summary>
		/// Decodes straight between caller-owned native buffers, with no pooled staging and no managed
		/// allocation. Use this when the compressed bytes are already pinned and you want the PCM written
		/// directly into your audio engine's buffer.
		/// </summary>
		/// <param name="from">
		/// Pointer to the compressed voice data. Must remain valid and pinned for the duration of the call.
		/// It is not null-checked - passing <see cref="System.IntPtr.Zero"/> is undefined behaviour inside
		/// Steam, not a managed exception.
		/// </param>
		/// <param name="length">
		/// Number of compressed bytes at <paramref name="from"/>. Must be greater than zero.
		/// </param>
		/// <param name="to">
		/// Pointer to the destination buffer that receives raw single-channel 16-bit PCM at
		/// <see cref="SampleRate"/>. Caller allocates, owns, and frees it.
		/// </param>
		/// <param name="bufferSize">
		/// Capacity of <paramref name="to"/> in bytes. Must be greater than zero. Valve suggests starting
		/// around 20 KB and growing as needed; too small a buffer makes the decode fail rather than writing
		/// a partial result.
		/// </param>
		/// <returns>
		/// Bytes of PCM written into <paramref name="to"/>, or <c>0</c> if the decode failed. Because the
		/// underlying result code is discarded, a too-small <paramref name="bufferSize"/> is
		/// indistinguishable from corrupted input - and unlike Valve's own API you are not told the size you
		/// should have used.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// <paramref name="length"/> or <paramref name="bufferSize"/> is not greater than zero. Note that
		/// this is the only overload that validates its inputs at all.
		/// </exception>
		public static unsafe int DecompressVoice( IntPtr from, int length, IntPtr to, int bufferSize )
		{
			if ( length <= 0 ) throw new ArgumentException( $"length should be > 0 " );
			if ( bufferSize <= 0 ) throw new ArgumentException( $"bufferSize should be > 0 " );

			uint szWritten = 0;

			if ( Internal.DecompressVoice( from, (uint) length, to, (uint)bufferSize, ref szWritten, SampleRate ) != VoiceResult.OK )
				return 0;
			
			return (int)szWritten;
		}

		/// <summary>
		/// Mints an authentication ticket that proves to a game server (or a peer) that this Steam account
		/// is who it claims to be and owns the app. You send the raw <see cref="AuthTicket.Data"/> bytes
		/// over your own transport; the receiver feeds them to <see cref="SteamServer.BeginAuthSession"/> or
		/// <see cref="BeginAuthSession"/> and waits for the verdict on the validate-response callback.
		/// </summary>
		/// <param name="identity">
		/// Who the ticket is being issued to. Pass a <see cref="SteamId"/> and Steam will only accept the
		/// ticket when that account redeems it; pass a <see cref="NetAddress"/> and only an entity at that
		/// IP can redeem it. This is the anti-relay binding - a ticket minted for one server is not usable
		/// at another, and a mismatch surfaces on the receiver as
		/// <see cref="AuthResponse.AuthTicketNetworkIdentityFailure"/>. Passing <c>default</c> produces an
		/// unbound ticket that anyone who intercepts it can redeem.
		/// </param>
		/// <returns>
		/// The ticket, or <see langword="null"/> if Steam refused to issue one - typically because the user
		/// is not logged on. The returned object implements <see cref="System.IDisposable"/> and owns a live
		/// Steam handle, so it is not a plain data carrier.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The ticket comes back before Steam has confirmed it with the backend. The bytes are populated
		/// immediately, but are not guaranteed usable until the get-ticket response callback fires with a
		/// success result; sending them straight away invites a legitimate rejection at the far end. Use
		/// <see cref="GetAuthSessionTicketAsync"/> when you want that wait handled for you.
		/// </para>
		/// <para>
		/// Every ticket you obtain must be cancelled. <see cref="AuthTicket.Cancel"/> - or disposing the
		/// object, which calls it - releases the handle and tells the far end the session is over. Dropping
		/// the reference instead leaks the session on Steam's side for the life of the process. Cancel when
		/// you disconnect from the server, not when you have finished sending the bytes.
		/// </para>
		/// <para>
		/// Tickets expire, and one ticket cannot be redeemed twice - a replay shows up as
		/// <see cref="AuthResponse.AuthTicketInvalidAlreadyUsed"/>. Mint a fresh one per connection attempt
		/// rather than caching. Valve also warns that this ticket is the wrong one for the
		/// <c>ISteamUserAuth/AuthenticateUserTicket</c> Web API call, which will fail with it; use
		/// <see cref="GetAuthTicketForWebApiAsync"/> for that.
		/// </para>
		/// <para>
		/// The ticket is copied out of a 2560-byte pooled buffer into a right-sized array, so
		/// <see cref="AuthTicket.Data"/> is safe to hold on to and to hand to another thread.
		/// </para>
		/// </remarks>
		/// <example>
		/// Client side of the round trip - obtain, send, and always clean up:
		/// <code>
		/// // Binding the ticket to the server's identity stops it being replayed elsewhere.
		/// using ( var ticket = SteamUser.GetAuthSessionTicket( serverSteamId ) )
		/// {
		///     if ( ticket == null )
		///         return; // not logged on - nothing to send
		///
		///     connection.Send( ticket.Data );
		///
		///     await PlayUntilDisconnected();
		/// }
		/// // Dispose() calls Cancel(). Without it the session leaks on the Steam backend.
		/// </code>
		/// </example>
		public static unsafe AuthTicket GetAuthSessionTicket( NetIdentity identity )
		{
			var data = Helpers.TakeBuffer( 2560 );

			fixed ( byte* b = data )
			{
				uint ticketLength = 0;
				uint ticket = Internal.GetAuthSessionTicket( (IntPtr)b, data.Length, ref ticketLength, ref identity );

				if ( ticket == 0 )
					return null;

				return new AuthTicket()
				{
					Data = data.Take( (int)ticketLength ).ToArray(),
					Handle = ticket
				};
			}
		}

		/// <summary>
		/// Retrieve a authentication ticket to be sent to the entity who wishes to authenticate you.
		/// This waits for a positive response from the backend before returning the ticket. This means
		/// the ticket is definitely ready to go as soon as it returns. Will return <see langword="null"/> if the callback
		/// times out or returns negatively.
		/// </summary>
		/// <param name="identity">
		/// The entity the ticket is issued to. See <see cref="GetAuthSessionTicket"/> for what binding to a
		/// <see cref="SteamId"/> versus a <see cref="NetAddress"/> actually enforces.
		/// </param>
		/// <param name="timeoutSeconds">
		/// How long to wait for Steam's confirmation callback before giving up. On timeout the partially
		/// issued ticket is cancelled for you before <see langword="null"/> is returned, so a timeout does
		/// not leak a handle.
		/// </param>
		/// <returns>
		/// A ticket the backend has already confirmed, or <see langword="null"/> if Steam refused to issue
		/// one, the backend returned a non-OK result, or the wait timed out. All three collapse to
		/// <see langword="null"/> - the underlying <see cref="Result"/> is not surfaced, so you cannot tell
		/// "try again" from "this user will never get a ticket".
		/// </returns>
		/// <remarks>
		/// <para>
		/// This polls in 10 ms steps while waiting for the callback, so callbacks must actually be being
		/// pumped. If you initialized with <c>asyncCallbacks: false</c> you have to be calling
		/// <see cref="SteamClient.RunCallbacks"/> from somewhere else concurrently, or this will always run
		/// to the timeout.
		/// </para>
		/// <para>
		/// Cancellation is handled here only on the failure paths. A ticket that is successfully returned is
		/// yours to cancel - the lifecycle notes on <see cref="GetAuthSessionTicket"/> apply unchanged.
		/// </para>
		/// </remarks>
		public static async Task<AuthTicket> GetAuthSessionTicketAsync( NetIdentity identity, double timeoutSeconds = 10.0f )
		{
			var result = Result.Pending;
			AuthTicket ticket = null;
			var stopwatch = Stopwatch.StartNew();

			void f( GetAuthSessionTicketResponse_t t )
			{
				if ( t.AuthTicket != ticket.Handle ) return;
				result = t.Result;
			}

			OnGetAuthSessionTicketResponse += f;

			try
			{
				ticket = GetAuthSessionTicket( identity );
				if ( ticket == null )
					return null;

				while ( result == Result.Pending )
				{
					await Task.Delay( 10 );

					if ( stopwatch.Elapsed.TotalSeconds > timeoutSeconds )
					{
						ticket.Cancel();
						return null;
					}
				}

				if ( result == Result.OK )
					return ticket;

				ticket.Cancel();
				return null;
			}
			finally
			{
				OnGetAuthSessionTicketResponse -= f;
			}
		}

		/// <summary>
		/// Starts a Web API ticket request. Only the handle is available synchronously; the ticket bytes
		/// arrive later on the response callback, which is why this is private and
		/// <see cref="GetAuthTicketForWebApiAsync"/> is the public entry point.
		/// </summary>
		/// <param name="identity">Identifier for the service that will redeem the ticket.</param>
		/// <returns>
		/// A ticket object with <see cref="AuthTicket.Handle"/> set and <see cref="AuthTicket.Data"/> still
		/// <see langword="null"/>, or <see langword="null"/> if Steam refused the request.
		/// </returns>
		private static unsafe AuthTicket GetAuthTicketForWebApi( string identity )
		{
			uint ticket = Internal.GetAuthTicketForWebApi( identity );

			if ( ticket == 0 )
				return null;

			return new AuthTicket()
			{
				Handle = ticket
			};
		}

		/// <summary>
		/// Retrieve a authentication ticket to be sent to the entity who wishes to authenticate you.
		/// This waits for a positive response from the backend before returning the ticket. This means
		/// the ticket is definitely ready to go as soon as it returns. Will return <see langword="null"/> if the callback
		/// times out or returns negatively.
		/// </summary>
		/// <param name="identity">
		/// A string naming the service that will redeem the ticket. This is not a
		/// <see cref="NetIdentity"/> and it is not a <see cref="SteamId"/> - Valve documents it only as an
		/// optional identifier for the service the ticket will be sent to. Your backend must present the
		/// same string when it calls <c>ISteamUserAuth/AuthenticateUserTicket</c>, or verification fails.
		/// </param>
		/// <param name="timeoutSeconds">
		/// How long to wait for the response callback carrying the ticket bytes. On timeout the ticket is
		/// cancelled for you and <see langword="null"/> is returned.
		/// </param>
		/// <returns>
		/// The ticket, or <see langword="null"/> if Steam refused the request, the backend returned a non-OK
		/// result, or the wait timed out - the three are not distinguishable.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This is the only ticket flavour the <c>ISteamUserAuth/AuthenticateUserTicket</c> Web API accepts;
		/// a ticket from <see cref="GetAuthSessionTicket"/> will be rejected by it. Conversely, do not feed a
		/// Web API ticket to <see cref="BeginAuthSession"/>.
		/// </para>
		/// <para>
		/// <see cref="AuthTicket.Data"/> here is the full 2560-byte fixed buffer copied out of the callback,
		/// not the used prefix. Valve's callback carries a used-length field, but this binding does not
		/// surface it, so the array you get has trailing padding of unknown significance. Valve does not
		/// document whether the Web API tolerates that padding; if verification fails, that is the first
		/// thing to look at.
		/// </para>
		/// <para>
		/// The returned ticket still owns a Steam handle. Cancel or dispose it once your backend has
		/// finished with it, exactly as for <see cref="GetAuthSessionTicket"/>.
		/// </para>
		/// </remarks>
		public static async Task<AuthTicket> GetAuthTicketForWebApiAsync( string identity, double timeoutSeconds = 10.0f )
		{
			var result = Result.Pending;
			AuthTicket ticket = null;
			var stopwatch = Stopwatch.StartNew();

			void f( GetTicketForWebApiResponse_t t )
			{
				if ( t.AuthTicket != ticket.Handle ) return;
				result = t.Result;
				ticket.Data = CopyWebApiTicket( ref t );
			}

			OnGetTicketForWebApiResponse += f;

			try
			{
				ticket = GetAuthTicketForWebApi( identity );
				if ( ticket == null )
					return null;

				while ( result == Result.Pending )
				{
					await Task.Delay( 10 );

					if ( stopwatch.Elapsed.TotalSeconds > timeoutSeconds )
					{
						ticket.Cancel();
						return null;
					}
				}

				if ( result == Result.OK )
					return ticket;

				ticket.Cancel();
				return null;
			}
			finally
			{
				OnGetTicketForWebApiResponse -= f;
			}
		}

		/// <summary>
		/// k_nMaxAuthTicketForWebApiSize - the length of m_rgubTicket.
		/// </summary>
		private const int WebApiTicketMaxLength = 2560;

		/// <summary>
		/// Copies the ticket out into a buffer the caller owns.
		///
		/// <para>
		/// m_rgubTicket is a fixed size buffer held inline in the callback struct now, so it
		/// cannot simply be handed over by reference - and it would not survive the callback
		/// if it could. The whole buffer is copied, exactly as before; <c>m_cbTicket</c> is
		/// the used length, but <see cref="AuthTicket.Data"/> has always carried all of it.
		/// </para>
		/// </summary>
		/// <param name="response">
		/// The callback payload. Passed by <see langword="ref"/> only to avoid copying the 2560-byte inline
		/// buffer; it is not modified.
		/// </param>
		/// <returns>
		/// A newly allocated 2560-byte array - always that exact length, regardless of how many bytes the
		/// ticket actually uses.
		/// </returns>
		private static unsafe byte[] CopyWebApiTicket( ref GetTicketForWebApiResponse_t response )
		{
			var data = new byte[WebApiTicketMaxLength];

			fixed ( byte* ticket = response.GubTicket )
			{
				Marshal.Copy( (IntPtr)ticket, data, 0, data.Length );
			}

			return data;
		}

		/// <summary>
		/// Starts verifying a ticket another player sent you, from this client. This is the peer-to-peer and
		/// listen-server counterpart of <see cref="SteamServer.BeginAuthSession"/> - use it when your game
		/// authenticates players without a separate dedicated-server process.
		/// </summary>
		/// <param name="ticketData">
		/// The raw ticket bytes as the far end produced them: <see cref="AuthTicket.Data"/> transported
		/// verbatim. Do not truncate, re-encode, or append to them. Must not be <see langword="null"/>.
		/// </param>
		/// <param name="steamid">
		/// The account that claims to own the ticket. Steam checks the ticket against this ID, so an
		/// impersonation attempt is caught here rather than being yours to detect.
		/// </param>
		/// <returns>
		/// <see cref="BeginAuthResult.OK"/> means only that the ticket passed structural checks and a
		/// session is now being tracked - it is not an authorization verdict. The real answer arrives later
		/// on <see cref="OnValidateAuthTicketResponse"/>. Any other value means no session was started and
		/// no callback will ever arrive: <see cref="BeginAuthResult.InvalidTicket"/>,
		/// <see cref="BeginAuthResult.DuplicateRequest"/> (a session for this ID is already open),
		/// <see cref="BeginAuthResult.InvalidVersion"/>, <see cref="BeginAuthResult.GameMismatch"/> (the
		/// ticket was issued for a different app), or <see cref="BeginAuthResult.ExpiredTicket"/>. These are
		/// distinguishable, so log which one you got.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Every successful call must be matched by <see cref="EndAuthSession"/> when the player leaves.
		/// Skip it and Steam keeps tracking a session that no longer exists, so that player's next join
		/// attempt is rejected with <see cref="BeginAuthResult.DuplicateRequest"/> until the process
		/// restarts.
		/// </para>
		/// <para>
		/// Do not treat the player as authenticated on <see cref="BeginAuthResult.OK"/> alone - wait for
		/// <see cref="AuthResponse.OK"/> on the callback, and stay subscribed afterwards: the same callback
		/// fires again later to revoke a player mid-session with values such as
		/// <see cref="AuthResponse.VACBanned"/>, <see cref="AuthResponse.AuthTicketCanceled"/>, or
		/// <see cref="AuthResponse.LoggedInElseWhere"/>.
		/// </para>
		/// </remarks>
		public static unsafe BeginAuthResult BeginAuthSession( byte[] ticketData, SteamId steamid )
		{
			fixed ( byte* ptr = ticketData )
			{
				return Internal.BeginAuthSession( (IntPtr) ptr, ticketData.Length, steamid );
			}
		}

		/// <summary>
		/// Stops tracking the auth session started by <see cref="BeginAuthSession"/>. Call this whenever the
		/// player leaves - including when authentication failed, when they timed out, and when you kicked
		/// them - not only on a clean disconnect.
		/// </summary>
		/// <param name="steamid">
		/// The account whose session should be closed. Must be the same ID you passed to
		/// <see cref="BeginAuthSession"/>.
		/// </param>
		/// <remarks>
		/// There is no return value and no error path: calling it for an ID with no open session does
		/// nothing. Omitting it is what hurts - the session stays open on Steam's side, the player still
		/// counts as being on your server, and their next join is refused with
		/// <see cref="BeginAuthResult.DuplicateRequest"/>. This does not cancel the ticket; cancelling is the
		/// issuing client's job via <see cref="AuthTicket.Cancel"/>.
		/// </remarks>
		/// <example>
		/// Receiving side of the round trip - begin on join, resolve on callback, end on every exit path:
		/// <code>
		/// SteamUser.OnValidateAuthTicketResponse += ( steamid, ownerid, response ) =&gt;
		/// {
		///     if ( response != AuthResponse.OK )
		///     {
		///         Kick( steamid, response.ToString() );  // also fires mid-session on a VAC ban
		///         return;
		///     }
		///
		///     // ownerid differs from steamid when the game is borrowed via Family Sharing.
		///     MarkAuthenticated( steamid, ownerid );
		/// };
		///
		/// void OnClientJoined( SteamId steamid, byte[] ticketBytes )
		/// {
		///     var begin = SteamUser.BeginAuthSession( ticketBytes, steamid );
		///     if ( begin != BeginAuthResult.OK )
		///         Kick( steamid, begin.ToString() );  // no callback is coming
		/// }
		///
		/// void OnClientLeft( SteamId steamid )
		/// {
		///     SteamUser.EndAuthSession( steamid );  // required on every exit path
		/// }
		/// </code>
		/// </example>
		public static void EndAuthSession( SteamId steamid ) => Internal.EndAuthSession( steamid );


		// UserHasLicenseForApp - SERVER VERSION ( DLC CHECKING )

		/// <summary>
		/// Checks if the current users looks like they are behind a NAT device.
		/// This is only valid if the user is connected to the Steam servers and may not catch all forms of NAT.
		/// </summary>
		/// <value>
		/// <see langword="true"/> when Steam believes the user is behind NAT. A <see langword="false"/> is
		/// weak evidence: Valve states the result is only meaningful once the user is connected to Steam,
		/// and that the detection misses some forms of NAT. There is no separate "don't know" value, so
		/// treat this as a hint for choosing a connection strategy, never as a guarantee that a direct
		/// connection will work.
		/// </value>
		public static bool IsBehindNAT => Internal.BIsBehindNAT();

		/// <summary>
		/// Set data to be replicated to friends so that they can join your game.
		/// Sets the "current server" the user is on for the friends list / rich presence
		/// join flow. Pass a zero <paramref name="gameServerId"/> and 0/0 to clear.
		/// (Studio addition — previously shipped only in the compiled binaries; source
		/// restored here so the DLL is reproducible from this repo.)
		/// </summary>
		/// <param name="gameServerId">
		/// The <see cref="SteamId"/> of the game server the user is playing on, as reported by the server
		/// itself - see <see cref="SteamServer.SteamId"/>. Pass <c>0</c> together with a zero
		/// <paramref name="ip"/> and <paramref name="port"/> to clear the advertisement when the user leaves.
		/// </param>
		/// <param name="ip">
		/// The IPv4 address of the server as a packed 32-bit value in host order, so 127.0.0.1 is
		/// <c>0x7F000001</c>. This is the address friends will try to connect to, which means a LAN address
		/// here advertises something unroutable to everyone outside your network.
		/// </param>
		/// <param name="port">
		/// The port clients connect to for gameplay - the game port, not the Steam query port.
		/// </param>
		/// <remarks>
		/// This only populates the friends list / rich presence "Join Game" affordance. It performs no
		/// authentication and starts no session, so it is not a substitute for the ticket flow in
		/// <see cref="GetAuthSessionTicket"/>. There is no return value: a wrong address fails silently and
		/// shows up only as friends being unable to join.
		/// </remarks>
		public static void AdvertiseGame( SteamId gameServerId, uint ip, ushort port )
		{
			Internal.AdvertiseGame( gameServerId, ip, port );
		}

		/// <summary>
		/// Gets the Steam level of the user, as shown on their Steam community profile.
		/// </summary>
		/// <value>
		/// The profile level. This is a Steam-account statistic and has nothing to do with your game's own
		/// progression. Valve documents no error value; <c>0</c> is a legitimate level for a new account, so
		/// it cannot be read as failure.
		/// </value>
		public static int SteamLevel => Internal.GetPlayerSteamLevel();

		/// <summary>
		/// Requests a URL which authenticates an in-game browser for store check-out, and then redirects to the specified URL.
		/// As long as the in-game browser accepts and handles session cookies, Steam microtransaction checkout pages will automatically recognize the user instead of presenting a login page.
		/// NOTE: The URL has a very short lifetime to prevent history-snooping attacks, so you should only call this API when you are about to launch the browser, or else immediately navigate to the result URL using a hidden browser window.
		/// NOTE: The resulting authorization cookie has an expiration time of one day, so it would be a good idea to request and visit a new auth URL every 12 hours.
		/// </summary>
		/// <param name="url">
		/// The store page to land on after authentication, for example an itemstore URL. It is sent to Steam
		/// as the redirect target, so it must be a Steam store URL the authentication flow is willing to
		/// redirect to.
		/// </param>
		/// <returns>
		/// A single-use authenticating URL to navigate the in-game browser to, or <see langword="null"/> if
		/// the call failed. Failure and "Steam returned an empty URL" are not distinguished. Treat the
		/// result as short-lived: request it immediately before you open the browser, never at startup.
		/// </returns>
		/// <remarks>
		/// This is a call result, so it needs callbacks to be running to complete - with
		/// <c>asyncCallbacks: false</c> you must be pumping <see cref="SteamClient.RunCallbacks"/>
		/// concurrently or the await never returns.
		/// </remarks>
		public static async Task<string> GetStoreAuthUrlAsync( string url )
		{
			var response = await Internal.RequestStoreAuthURL( url );
			if ( !response.HasValue )
				return null;

			return response.Value.URLUTF8();
		}

		/// <summary>
		/// Checks whether the current user has verified their phone number.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if a verified phone number is on the account. Commonly used as a
		/// lightweight anti-alt signal before granting trades or ranked access. Valve documents no error
		/// value, so a disconnected client is indistinguishable from an unverified one - do not gate
		/// anything irreversible on a single read.
		/// </value>
		public static bool IsPhoneVerified => Internal.BIsPhoneVerified();

		/// <summary>
		/// Checks whether the current user has Steam Guard two factor authentication enabled on their account.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if Steam Guard two-factor is enabled. As with the other account-status
		/// flags, there is no distinct failure value.
		/// </value>
		public static bool IsTwoFactorEnabled => Internal.BIsTwoFactorEnabled();

		/// <summary>
		/// Checks whether the user's phone number is used to uniquely identify them.
		/// </summary>
		/// <value>
		/// <see langword="true"/> if the phone number on the account is treated by Steam as an identifying
		/// number - a stronger signal than <see cref="IsPhoneVerified"/>, because an identifying number is
		/// not shared across accounts.
		/// </value>
		public static bool IsPhoneIdentifying => Internal.BIsPhoneIdentifying();

		/// <summary>
		/// Checks whether the current user's phone number is awaiting (re)verification.
		/// </summary>
		/// <value>
		/// <see langword="true"/> while Steam is waiting for the user to (re)verify their number. This is a
		/// transient state - a user who is in it is not verified right now even if they were yesterday.
		/// </value>
		public static bool IsPhoneRequiringVerification => Internal.BIsPhoneRequiringVerification();

		/// <summary>
		/// Requests an application ticket encrypted with the secret "encrypted app ticket key".
		/// The encryption key can be obtained from the Encrypted App Ticket Key page on the App Admin for your app.
		/// There can only be one call pending, and this call is subject to a 60 second rate limit.
		/// If you get a null result from this it's probably because you're calling it too often.
		/// This can fail if you don't have an encrypted ticket set for your app here https://partner.steamgames.com/apps/sdkauth/
		/// </summary>
		/// <param name="dataToInclude">
		/// Arbitrary bytes to encrypt into the ticket, readable by your backend once it decrypts with the
		/// app ticket key. A nonce or session id belongs here - it is how you bind the ticket to a specific
		/// request instead of accepting any ticket for that user. Must not be <see langword="null"/>; use
		/// the parameterless overload when you have nothing to include.
		/// </param>
		/// <returns>
		/// The encrypted ticket bytes, or <see langword="null"/> on failure. Every failure looks the same:
		/// the 60-second rate limit, a request already in flight, no encrypted app ticket key configured for
		/// the app, or a decode failure all return <see langword="null"/>. In practice a
		/// <see langword="null"/> on a correctly configured app almost always means you called it too often.
		/// </returns>
		/// <remarks>
		/// Only one request may be pending at a time and the call is rate limited to one per 60 seconds -
		/// so this is a login-time operation, not something to call per match. The retrieval buffer is fixed
		/// at 1024 bytes; a ticket larger than that fails the copy and also yields <see langword="null"/>.
		/// </remarks>
		public static async Task<byte[]> RequestEncryptedAppTicketAsync( byte[] dataToInclude )
		{
			var dataPtr = Marshal.AllocHGlobal( dataToInclude.Length );
			Marshal.Copy( dataToInclude, 0, dataPtr, dataToInclude.Length );

			try
			{
				var result = await Internal.RequestEncryptedAppTicket( dataPtr, dataToInclude.Length );
				if ( !result.HasValue || result.Value.Result != Result.OK ) return null;

				var ticketData = Marshal.AllocHGlobal( 1024 );
				uint outSize = 0;
				byte[] data = null;

				if ( Internal.GetEncryptedAppTicket( ticketData, 1024, ref outSize ) )
				{
					data = new byte[outSize];
					Marshal.Copy( ticketData, data, 0, (int) outSize );
				}

				Marshal.FreeHGlobal( ticketData );

				return data;
			}
			finally
			{
				Marshal.FreeHGlobal( dataPtr );
			}
		}

		/// <summary>
		/// Requests an application ticket encrypted with the secret "encrypted app ticket key".
		/// The encryption key can be obtained from the Encrypted App Ticket Key page on the App Admin for your app.
		/// There can only be one call pending, and this call is subject to a 60 second rate limit.
		/// This can fail if you don't have an encrypted ticket set for your app here https://partner.steamgames.com/apps/sdkauth/
		/// </summary>
		/// <returns>
		/// The encrypted ticket bytes, or <see langword="null"/> on failure. Same indistinguishable failure
		/// modes and same 60-second rate limit as the overload that takes payload data. Without embedded
		/// data the ticket only attests identity and ownership - it cannot be bound to a particular request,
		/// so your backend has no replay protection of its own.
		/// </returns>
		public static async Task<byte[]> RequestEncryptedAppTicketAsync()
		{
			var result = await Internal.RequestEncryptedAppTicket( IntPtr.Zero, 0 );
			if ( !result.HasValue || result.Value.Result != Result.OK ) return null;

			var ticketData = Marshal.AllocHGlobal( 1024 );
			uint outSize = 0;
			byte[] data = null;

			if ( Internal.GetEncryptedAppTicket( ticketData, 1024, ref outSize ) )
			{
				data = new byte[outSize];
				Marshal.Copy( ticketData, data, 0, (int)outSize );
			}

			Marshal.FreeHGlobal( ticketData );

			return data;

		}


		/// <summary>
		/// Asks Steam how much play time the current user has left under anti-indulgence / duration control
		/// regulations, and whether rewards should still be granted at full rate. Only meaningful in
		/// territories where the regime applies; elsewhere the response reports that it is not applicable.
		/// </summary>
		/// <returns>
		/// The current duration-control state, or <c>default</c> if the call did not return a value. Because
		/// the failure value is <c>default</c> rather than <see langword="null"/>, a failed call is
		/// indistinguishable from a genuine all-zero response - check the applicability and result fields on
		/// the returned <see cref="DurationControl"/> before acting on it, and never treat <c>default</c> as
		/// "the user may keep playing".
		/// </returns>
		/// <remarks>
		/// The same information also arrives unprompted on <see cref="OnDurationControl"/> as Steam's timers
		/// fire; that event is the one to react to during a session, while this call is for querying state
		/// on demand.
		/// </remarks>
		public static async Task<DurationControl> GetDurationControl()
		{
			var response = await Internal.GetDurationControl();
			if ( !response.HasValue ) return default;

			return new DurationControl { _inner = response.Value };
		}
	}
}
