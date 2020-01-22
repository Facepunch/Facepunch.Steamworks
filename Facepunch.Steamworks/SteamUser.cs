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
	public static class SteamUser
	{
		static ISteamUser _internal;
		internal static ISteamUser Internal
		{
			get
			{
				SteamClient.ValidCheck();

				if ( _internal == null )
				{
					_internal = new ISteamUser();
					_internal.Init();

					richPresence = new Dictionary<string, string>();

					SampleRate = OptimalSampleRate;
				}

				return _internal;
			}
		}
		internal static void Shutdown()
		{
			_internal = null;
		}

		static Dictionary<string, string> richPresence;

		internal static void InstallEvents()
		{
			SteamServersConnected_t.Install( x => OnSteamServersConnected?.Invoke() );
			SteamServerConnectFailure_t.Install( x => OnSteamServerConnectFailure?.Invoke() );
			SteamServersDisconnected_t.Install( x => OnSteamServersDisconnected?.Invoke() );
			ClientGameServerDeny_t.Install( x => OnClientGameServerDeny?.Invoke() );
			LicensesUpdated_t.Install( x => OnLicensesUpdated?.Invoke() );
			ValidateAuthTicketResponse_t.Install( x => OnValidateAuthTicketResponse?.Invoke( x.SteamID, x.OwnerSteamID, x.AuthSessionResponse ) );
			MicroTxnAuthorizationResponse_t.Install( x => OnMicroTxnAuthorizationResponse?.Invoke( x.AppID, x.OrderID, x.Authorized != 0 ) );
			GameWebCallback_t.Install( x => OnGameWebCallback?.Invoke( x.URLUTF8() ) );
			GetAuthSessionTicketResponse_t.Install( x => OnGetAuthSessionTicketResponse?.Invoke( x ) );
		}

		/// <summary>
		/// Called when a connections to the Steam back-end has been established.
		/// This means the Steam client now has a working connection to the Steam servers. 
		/// Usually this will have occurred before the game has launched, and should only be seen if the 
		/// user has dropped connection due to a networking issue or a Steam server update.
		/// </summary>
		public static event Action OnSteamServersConnected;

		/// <summary>
		/// Called when a connection attempt has failed.
		///	This will occur periodically if the Steam client is not connected, 
		///	and has failed when retrying to establish a connection.
		/// </summary>
		public static event Action OnSteamServerConnectFailure;

		/// <summary>
		/// Called if the client has lost connection to the Steam servers.
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
		/// Called whenever the users licenses (owned packages) changes.
		/// </summary>
		public static event Action OnLicensesUpdated;

		/// <summary>
		/// Called when an auth ticket has been validated. 
		/// The first parameter is the steamid of this user
		/// The second is the Steam ID that owns the game, this will be different from the first 
		/// if the game is being borrowed via Steam Family Sharing
		/// </summary>
		public static event Action<SteamId, SteamId, AuthResponse> OnValidateAuthTicketResponse;

		/// <summary>
		/// Used internally for GetAuthSessionTicketAsync
		/// </summary>
		internal static event Action<GetAuthSessionTicketResponse_t> OnGetAuthSessionTicketResponse;

		/// <summary>
		/// Called when a user has responded to a microtransaction authorization request.
		/// ( appid, orderid, user authorized )
		/// </summary>
		public static event Action<AppId, ulong, bool> OnMicroTxnAuthorizationResponse;

		/// <summary>
		/// Sent to your game in response to a steam://gamewebcallback/ command from a user clicking a link in the Steam overlay browser.
		/// You can use this to add support for external site signups where you want to pop back into the browser after some web page 
		/// signup sequence, and optionally get back some detail about that.
		/// </summary>
		public static event Action<string> OnGameWebCallback;




		static bool _recordingVoice;

		/// <summary>
		/// Starts/Stops voice recording.
		/// Once started, use GetAvailableVoice and GetVoice to get the data, and then call StopVoiceRecording 
		/// when the user has released their push-to-talk hotkey or the game session has completed.
		/// </summary>

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
		/// Returns true if we have voice data waiting to be read
		/// </summary>
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
		/// Reads the voice data and returns the number of bytes written.
		/// The compressed data can be transmitted by your application and decoded back into raw audio data using 
		/// DecompressVoice on the other side. The compressed data provided is in an arbitrary format and is not meant to be played directly.
		/// This should be called once per frame, and at worst no more than four times a second to keep the microphone input delay as low as 
		/// possible. Calling this any less may result in gaps in the returned stream.
		/// </summary>
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
		/// Reads the voice data and returns the bytes. You should obviously ideally be using
		/// ReadVoiceData because it won't be creating a new byte array every call. But this 
		/// makes it easier to get it working, so let the babies have their bottle.
		/// </summary>
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

		public static uint OptimalSampleRate => Internal.GetVoiceOptimalSampleRate();


		/// <summary>
		/// Decodes the compressed voice data returned by GetVoice.
		/// The output data is raw single-channel 16-bit PCM audio.The decoder supports any sample rate from 11025 to 48000.
		/// </summary>
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
		/// Retrieve a authentication ticket to be sent to the entity who wishes to authenticate you.
		/// </summary>
		public static unsafe AuthTicket GetAuthSessionTicket()
		{
			var data = Helpers.TakeBuffer( 1024 );

			fixed ( byte* b = data )
			{
				uint ticketLength = 0;
				uint ticket = Internal.GetAuthSessionTicket( (IntPtr)b, data.Length, ref ticketLength );

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
		/// the ticket is definitely ready to go as soon as it returns. Will return null if the callback
		/// times out or returns negatively.
		/// </summary>
		public static async Task<AuthTicket> GetAuthSessionTicketAsync( double timeoutSeconds = 10.0f )
		{
			var result = Result.Pending;
			AuthTicket ticket = null;
			var stopwatch = Stopwatch.StartNew();

			Action<GetAuthSessionTicketResponse_t> f = ( t ) =>
			{
				if ( t.AuthTicket != ticket.Handle ) return;
				result = t.Result;
			};

			OnGetAuthSessionTicketResponse += f;

			try
			{
				ticket = GetAuthSessionTicket();
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

		public static unsafe BeginAuthResult BeginAuthSession( byte[] ticketData, SteamId steamid )
		{
			fixed ( byte* ptr = ticketData )
			{
				return Internal.BeginAuthSession( (IntPtr) ptr, ticketData.Length, steamid );
			}
		}

		public static void EndAuthSession( SteamId steamid ) => Internal.EndAuthSession( steamid );


		// UserHasLicenseForApp - SERVER VERSION ( DLC CHECKING )

		/// <summary>
		/// Checks if the current users looks like they are behind a NAT device.
		/// This is only valid if the user is connected to the Steam servers and may not catch all forms of NAT.
		/// </summary>
		public static bool IsBehindNAT => Internal.BIsBehindNAT();

		/// <summary>
		/// Gets the Steam level of the user, as shown on their Steam community profile.
		/// </summary>
		public static int SteamLevel => Internal.GetPlayerSteamLevel();

		/// <summary>
		/// Requests a URL which authenticates an in-game browser for store check-out, and then redirects to the specified URL.
		/// As long as the in-game browser accepts and handles session cookies, Steam microtransaction checkout pages will automatically recognize the user instead of presenting a login page.
		/// NOTE: The URL has a very short lifetime to prevent history-snooping attacks, so you should only call this API when you are about to launch the browser, or else immediately navigate to the result URL using a hidden browser window.
		/// NOTE: The resulting authorization cookie has an expiration time of one day, so it would be a good idea to request and visit a new auth URL every 12 hours.
		/// </summary>
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
		public static bool IsPhoneVerified => Internal.BIsPhoneVerified();

		/// <summary>
		/// Checks whether the current user has Steam Guard two factor authentication enabled on their account.
		/// </summary>
		public static bool IsTwoFactorEnabled => Internal.BIsTwoFactorEnabled();

		/// <summary>
		/// Checks whether the user's phone number is used to uniquely identify them.
		/// </summary>
		public static bool IsPhoneIdentifying => Internal.BIsPhoneIdentifying();

		/// <summary>
		/// Checks whether the current user's phone number is awaiting (re)verification.
		/// </summary>
		public static bool IsPhoneRequiringVerification => Internal.BIsPhoneRequiringVerification();

		/// <summary>
		/// Requests an application ticket encrypted with the secret "encrypted app ticket key".
		/// The encryption key can be obtained from the Encrypted App Ticket Key page on the App Admin for your app.
		/// There can only be one call pending, and this call is subject to a 60 second rate limit.
		/// This can fail if you don't have an encrypted ticket set for your app here https://partner.steamgames.com/apps/sdkauth/
		/// </summary>
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

	}
}