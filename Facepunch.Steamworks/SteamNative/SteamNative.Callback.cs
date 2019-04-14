using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Facepunch.Steamworks;

namespace SteamNative
{
    [StructLayout( LayoutKind.Sequential )]
    internal class Callback
    {
        internal enum Flags : byte
        {
            Registered = 0x01,
            GameServer = 0x02
        }

        public IntPtr vTablePtr;
        public byte CallbackFlags;
        public int CallbackId;

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTable
        {
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultD( IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultWithInfoD( IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate int GetSizeD();

            public ResultD ResultA;
            public ResultWithInfoD ResultB;
            public GetSizeD GetSize;

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );

				var vTable = new Callback.VTable
				{
					ResultA = onResultThis,
					ResultB = onResultWithInfoThis,
					GetSize = onGetSizeThis,
				};

				allocations.Add( GCHandle.Alloc( vTable.ResultA ) );
				allocations.Add( GCHandle.Alloc( vTable.ResultB ) );
				allocations.Add( GCHandle.Alloc( vTable.GetSize ) );

				Marshal.StructureToPtr( vTable, vTablePtr, false );

				return vTablePtr;
			}
		}

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableWin
        {
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultD( IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate void ResultWithInfoD( IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.StdCall )] public delegate int GetSizeD();

            public ResultWithInfoD ResultB;
            public ResultD ResultA;
            public GetSizeD GetSize;

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableWin ) ) );

				var vTable = new Callback.VTableWin
				{
					ResultA = onResultThis,
					ResultB = onResultWithInfoThis,
					GetSize = onGetSizeThis,
				};

				allocations.Add( GCHandle.Alloc( vTable.ResultA ) );
				allocations.Add( GCHandle.Alloc( vTable.ResultB ) );
				allocations.Add( GCHandle.Alloc( vTable.GetSize ) );

				Marshal.StructureToPtr( vTable, vTablePtr, false );

				return vTablePtr;
			}
		}

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableThis
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultD( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultWithInfoD( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableThis ) ) );

				var vTable = new Callback.VTableThis
				{
					ResultA = onResultThis,
					ResultB = onResultWithInfoThis,
					GetSize = onGetSizeThis,
				};

				allocations.Add( GCHandle.Alloc( vTable.ResultA ) );
				allocations.Add( GCHandle.Alloc( vTable.ResultB ) );
				allocations.Add( GCHandle.Alloc( vTable.GetSize ) );

				Marshal.StructureToPtr( vTable, vTablePtr, false );

				return vTablePtr;
			}

			[UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate int GetSizeD( IntPtr thisptr );

            public ResultD ResultA;
            public ResultWithInfoD ResultB;
            public GetSizeD GetSize;
        }

        [StructLayout( LayoutKind.Sequential, Pack = 1 )]
        public class VTableWinThis
        {
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultD( IntPtr thisptr, IntPtr pvParam );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate void ResultWithInfoD( IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamNative.SteamAPICall_t hSteamAPICall );
            [UnmanagedFunctionPointer( CallingConvention.ThisCall )] public delegate int GetSizeD( IntPtr thisptr );

            public ResultWithInfoD ResultB;
            public ResultD ResultA;
            public GetSizeD GetSize;

			internal static IntPtr GetVTable( ResultD onResultThis, ResultWithInfoD onResultWithInfoThis, GetSizeD onGetSizeThis, List<GCHandle> allocations )
			{
				var vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableWinThis ) ) );

				var vTable = new Callback.VTableWinThis
				{
					ResultA = onResultThis,
					ResultB = onResultWithInfoThis,
					GetSize = onGetSizeThis,
				};

				allocations.Add( GCHandle.Alloc( vTable.ResultA ) );
				allocations.Add( GCHandle.Alloc( vTable.ResultB ) );
				allocations.Add( GCHandle.Alloc( vTable.GetSize ) );

				Marshal.StructureToPtr( vTable, vTablePtr, false );

				return vTablePtr;
			}
		}
    };

    //
    // Created on registration of a callback
    //
    internal class CallbackHandle : IDisposable
    {
        internal BaseSteamworks Steamworks;

        // Get Rid
        internal GCHandle FuncA;
        internal GCHandle FuncB;
        internal GCHandle FuncC;
        internal IntPtr vTablePtr;
        internal GCHandle PinnedCallback;

        internal CallbackHandle( Facepunch.Steamworks.BaseSteamworks steamworks )
        {
            Steamworks = steamworks;
        }

        public void Dispose()
        {
            UnregisterCallback();

            if ( FuncA.IsAllocated )
                FuncA.Free();

            if ( FuncB.IsAllocated )
                FuncB.Free();

            if ( FuncC.IsAllocated )
                FuncC.Free();

            if ( PinnedCallback.IsAllocated )
                PinnedCallback.Free();

            if ( vTablePtr != IntPtr.Zero )
            {
                Marshal.FreeHGlobal( vTablePtr );
                vTablePtr = IntPtr.Zero;
            }
        }

        private void UnregisterCallback()
        {
            if ( !PinnedCallback.IsAllocated )
                return;

            Steamworks.native.api.SteamAPI_UnregisterCallback( PinnedCallback.AddrOfPinnedObject() );
        }

        public virtual bool IsValid { get { return true; } }
    }

	internal class CallbackHandle<T> : CallbackHandle where T: struct, Steamworks.ISteamCallback
	{
		T template;

		internal CallbackHandle( Facepunch.Steamworks.BaseSteamworks steamworks ) : base( steamworks )
		{
			template = new T();

			//
			// Create the functions we need for the vtable
			//
			if ( Facepunch.Steamworks.Config.UseThisCall )
			{
				//
				// Create the VTable by manually allocating the memory and copying across
				//
				if ( Platform.IsWindows )
				{
					vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableWinThis ) ) );
					var vTable = new Callback.VTableWinThis
					{
						ResultA = OnResultThis,
						ResultB = OnResultWithInfoThis,
						GetSize = OnGetSizeThis,
					};
					FuncA = GCHandle.Alloc( vTable.ResultA );
					FuncB = GCHandle.Alloc( vTable.ResultB );
					FuncC = GCHandle.Alloc( vTable.GetSize );
					Marshal.StructureToPtr( vTable, vTablePtr, false );
				}
				else
				{
					vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableThis ) ) );
					var vTable = new Callback.VTableThis
					{
						ResultA = OnResultThis,
						ResultB = OnResultWithInfoThis,
						GetSize = OnGetSizeThis,
					};
					FuncA = GCHandle.Alloc( vTable.ResultA );
					FuncB = GCHandle.Alloc( vTable.ResultB );
					FuncC = GCHandle.Alloc( vTable.GetSize );
					Marshal.StructureToPtr( vTable, vTablePtr, false );
				}
			}
			else
			{
				//
				// Create the VTable by manually allocating the memory and copying across
				//
				if ( Platform.IsWindows )
				{
					vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableWin ) ) );
					var vTable = new Callback.VTableWin
					{
						ResultA = OnResult,
						ResultB = OnResultWithInfo,
						GetSize = OnGetSize,
					};
					FuncA = GCHandle.Alloc( vTable.ResultA );
					FuncB = GCHandle.Alloc( vTable.ResultB );
					FuncC = GCHandle.Alloc( vTable.GetSize );
					Marshal.StructureToPtr( vTable, vTablePtr, false );
				}
				else
				{
					vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );
					var vTable = new Callback.VTable
					{
						ResultA = OnResult,
						ResultB = OnResultWithInfo,
						GetSize = OnGetSize,
					};
					FuncA = GCHandle.Alloc( vTable.ResultA );
					FuncB = GCHandle.Alloc( vTable.ResultB );
					FuncC = GCHandle.Alloc( vTable.GetSize );
					Marshal.StructureToPtr( vTable, vTablePtr, false );
				}
			}

			//
			// Create the callback object
			//
			var cb = new Callback();
			cb.vTablePtr = vTablePtr;
			cb.CallbackFlags = steamworks.IsGameServer ? (byte)SteamNative.Callback.Flags.GameServer : (byte)0;
			cb.CallbackId = template.GetCallbackId();

			//
			// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native
			//
			PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );

			//
			// Register the callback with Steam
			//
			steamworks.native.api.SteamAPI_RegisterCallback( PinnedCallback.AddrOfPinnedObject(), cb.CallbackId );

			steamworks.RegisterCallbackHandle( this );
		}

		[MonoPInvokeCallback]
		internal void OnResultThis( IntPtr self, IntPtr param ) { OnResult( param ); }
		[MonoPInvokeCallback]
		internal void OnResultWithInfoThis( IntPtr self, IntPtr param, bool failure, SteamNative.SteamAPICall_t call ) { OnResultWithInfo( param, failure, call ); }
		[MonoPInvokeCallback]
		internal int OnGetSizeThis( IntPtr self ) { return OnGetSize(); }
		[MonoPInvokeCallback]
		internal int OnGetSize() { return template.GetStructSize(); }

		[MonoPInvokeCallback]
		internal void OnResult( IntPtr param )
		{
			OnResultWithInfo( param, false, 0 );
		}

		[MonoPInvokeCallback]
		internal void OnResultWithInfo( IntPtr param, bool failure, SteamNative.SteamAPICall_t call )
		{
			if ( failure ) return;

			var value = (T) template.Fill( param );

			if ( Facepunch.Steamworks.Client.Instance != null )
				Facepunch.Steamworks.Client.Instance.OnCallback<T>( value );

			if ( Facepunch.Steamworks.Server.Instance != null )
				Facepunch.Steamworks.Server.Instance.OnCallback<T>( value );
		}
	}

	internal abstract class CallResult : CallbackHandle
    {
        internal SteamAPICall_t Call;
        public override bool IsValid { get { return Call > 0; } }


        internal CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call ) : base( steamworks )
        {
            Call = call;
        }

        internal void Try()
        {
            bool failed = false;

            if ( !Steamworks.native.utils.IsAPICallCompleted( Call, ref failed ))
                return;

            Steamworks.UnregisterCallResult( this );

            RunCallback();
        }

        internal abstract void RunCallback();
    }


    internal class CallResult<T> : CallResult where T : struct, Steamworks.ISteamCallback
    {
		T template;

        private static byte[] resultBuffer = new byte[1024 * 16];

        internal delegate T ConvertFromPointer( IntPtr p );

        Action<T, bool> CallbackFunction;

        internal CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<T, bool> callbackFunction ) : base( steamworks, call )
        {
			template = new T();
			CallbackFunction = callbackFunction;

            Steamworks.RegisterCallResult( this );
        }

        public override string ToString()
        {
            return $"CallResult( {typeof(T).Name}, {template.GetCallbackId()}, {template.GetStructSize()}b )";
        }

        unsafe internal override void RunCallback()
        {
            bool failed = false;

            fixed ( byte* ptr = resultBuffer )
            {
                if ( !Steamworks.native.utils.GetAPICallResult( Call, (IntPtr)ptr, resultBuffer.Length, template.GetCallbackId(), ref failed ) || failed )
                {
                    CallbackFunction( default(T), true );
                    return;
                }

                var val = (T) template.Fill( (IntPtr)ptr );
                CallbackFunction( val, false );
            }
        }
    }

    internal class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute() { }
    }
}
