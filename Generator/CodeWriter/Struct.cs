using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        public class TypeDef
        {
            public string Name;
            public string NativeType;
            public string ManagedType;
        }

        private Dictionary<string, TypeDef> TypeDefs = new Dictionary<string, TypeDef>();

        //
        // Don't give a fuck about these classes, they just cause us trouble
        //
        public readonly static string[] SkipStructs = new string[]
        {
            "CSteamID",
            "CSteamAPIContext",
            "CCallResult",
            "CCallback",
            "ValvePackingSentinel_t",
            "CCallbackBase"
        };

        public readonly static string[] ForceLargePackStructs = new string[]
        {
            "LeaderboardEntry_t"
        };

        void Structs()
        {
            var callbackList = new List<SteamApiDefinition.StructDef>();

            foreach ( var c in def.structs )
            {
                if ( SkipStructs.Contains( c.Name ) )
                    continue;

                if ( c.Name.Contains( "::" ) )
                    continue;

                int defaultPack = 8;

                if (  c.Fields.Any( x => x.Type.Contains( "class CSteamID" ) ) && !ForceLargePackStructs.Contains( c.Name ) )
                    defaultPack = 4;

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                StartBlock( $"internal struct {c.Name}" );
                {
                    if ( !string.IsNullOrEmpty( c.CallbackId ) )
                    {
                        WriteLine( "internal const int CallbackId = " + c.CallbackId  + ";" );
                    }

                    //
                    // The fields
                    //
                    StructFields( c.Fields );

                    WriteLine();
                    WriteLine( "//" );
                    WriteLine( "// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff." );
                    WriteLine( "//" );
                    StartBlock( $"internal static {c.Name} FromPointer( IntPtr p )" );
                    {
                        WriteLine( $"if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );" );

                        WriteLine( $"return ({c.Name}) Marshal.PtrToStructure( p, typeof({c.Name}) );" );
                    }
                    EndBlock();

                    WriteLine();
                    WriteLine( "//" );
                    WriteLine( "// Get the size of the structure we're going to be using." );
                    WriteLine( "//" );
                    StartBlock( $"internal static int StructSize()" );
                    {
                        WriteLine( $"if ( Platform.PackSmall ) return System.Runtime.InteropServices.Marshal.SizeOf( typeof(PackSmall) );" );

                        WriteLine( $"return System.Runtime.InteropServices.Marshal.SizeOf( typeof({c.Name}) );" );
                    }
                    EndBlock();

                    if ( defaultPack == 8 )
                        defaultPack = 4;

                    //
                    // Small packed struct (for osx, linux)
                    //
                    WriteLine();
                    WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                    StartBlock( $"internal struct PackSmall" );
                    {
                        StructFields( c.Fields );

                        //
                        // Implicit convert from PackSmall to regular
                        //
                        WriteLine();
                        WriteLine( "//" );
                        WriteLine( $"// Easily convert from PackSmall to {c.Name}" );
                        WriteLine( "//" );
                        StartBlock( $"public static implicit operator {c.Name} (  {c.Name}.PackSmall d )" );
                        {
                            StartBlock( $"return new {c.Name}()" );
                            {
                                foreach ( var f in c.Fields )
                                {
                                    WriteLine( $"{CleanMemberName( f.Name )} = d.{CleanMemberName( f.Name )}," );
                                }
                            }
                            EndBlock( ";" );
                        }
                        EndBlock();

                    }
                    EndBlock();

                    if ( c.IsCallResult )
                    {
                        CallResult( c );
                    }

                    if ( !string.IsNullOrEmpty( c.CallbackId ) )
                    {
                        Callback( c );
                        callbackList.Add( c );
                    }

                }
                EndBlock();
                WriteLine();
            }

            StartBlock( $"internal static class Callbacks" );
            StartBlock( $"internal static void RegisterCallbacks( Facepunch.Steamworks.BaseSteamworks steamworks )" );
            {
                foreach ( var c in callbackList )
                {
                    WriteLine( $"{c.Name}.Register( steamworks );" );
                }
            }
            EndBlock();
            EndBlock();
        }

        private void StructFields( SteamApiDefinition.StructDef.StructFields[] fields )
        {
            foreach ( var m in fields )
            {
                var t = ToManagedType( m.Type );

                if ( TypeDefs.ContainsKey( t ) )
                {
                    t = TypeDefs[t].ManagedType;
                }

                if ( t == "bool" )
                {
                    WriteLine( "[MarshalAs(UnmanagedType.I1)]" );
                }

                if ( t.StartsWith( "char " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "char", "" ).Trim( '[', ']', ' ' );
                    t = "string";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValTStr, SizeConst = {num})]" );
                }

                if ( t.StartsWith( "uint8 " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint8", "" ).Trim( '[', ']', ' ' );
                    t = "char";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValTStr, SizeConst = {num})]" );
                }

                if ( t.StartsWith( "CSteamID " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "CSteamID", "" ).Trim( '[', ']', ' ' );
                    t = $"ulong[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "PublishedFileId_t " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "PublishedFileId_t", "" ).Trim( '[', ']', ' ' );
                    t = $"ulong[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
                }

                if ( t.StartsWith( "uint32 " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "uint32", "" ).Trim( '[', ']', ' ' );
                    t = $"uint[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U4)]" );
                }

                if ( t.StartsWith( "float " ) && t.Contains( "[" ) )
                {
                    var num = t.Replace( "float", "" ).Trim( '[', ']', ' ' );
                    t = $"float[]";
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.R4)]" );
                }

                if ( t == "const char **" )
                {
                    t = "IntPtr";
                }

                if (t.StartsWith("AppId_t ") && t.Contains("["))
                {
                    var num = t.Replace("AppId_t", "").Trim('[', ']', ' ');
                    t = $"AppId_t[]";
                    WriteLine($"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U4)]");
                }

                WriteLine( $"internal {t} {CleanMemberName( m.Name )}; // {m.Name} {m.Type}" );
            }
        }

        private void Callback( SteamApiDefinition.StructDef c )
        {           
            WriteLine();
            StartBlock( $"internal static void Register( Facepunch.Steamworks.BaseSteamworks steamworks )" );
            {
                WriteLine( $"var handle = new CallbackHandle( steamworks );" );
                WriteLine( $"" );

                CallbackCall( c );

                WriteLine( "" );
                WriteLine( "//" );
                WriteLine( "// Register the callback with Steam" );
                WriteLine( "//" );
                WriteLine( $"steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );" );

                WriteLine();
                WriteLine( "steamworks.RegisterCallbackHandle( handle );" );
            }
            EndBlock();

            WriteLine();
            WriteLine( "[MonoPInvokeCallback]" );
            WriteLine( "internal static void OnResultThis( IntPtr self, IntPtr param ){ OnResult( param ); }" );
            WriteLine( "[MonoPInvokeCallback]" );
            WriteLine( "internal static void OnResultWithInfoThis( IntPtr self, IntPtr param, bool failure, SteamNative.SteamAPICall_t call ){ OnResultWithInfo( param, failure, call ); }" );
            WriteLine( "[MonoPInvokeCallback]" );
            WriteLine( "internal static int OnGetSizeThis( IntPtr self ){ return OnGetSize(); }" );
            WriteLine( "[MonoPInvokeCallback]" );
            WriteLine( "internal static int OnGetSize(){ return StructSize(); }" );

            WriteLine();
            WriteLine( "[MonoPInvokeCallback]" );
            StartBlock( "internal static void OnResult( IntPtr param )" );
            {
                WriteLine( $"OnResultWithInfo( param, false, 0 );" );
            }
            EndBlock();

            WriteLine();
            WriteLine( "[MonoPInvokeCallback]" );
            StartBlock( "internal static void OnResultWithInfo( IntPtr param, bool failure, SteamNative.SteamAPICall_t call )" );
            {
                WriteLine( $"if ( failure ) return;" );
                WriteLine();
                WriteLine( "var value = FromPointer( param );" );

                WriteLine();
                WriteLine( "if ( Facepunch.Steamworks.Client.Instance != null )" );
                WriteLine( $"    Facepunch.Steamworks.Client.Instance.OnCallback<{c.Name}>( value );" );

                WriteLine();
                WriteLine( "if ( Facepunch.Steamworks.Server.Instance != null )" );
                WriteLine( $"    Facepunch.Steamworks.Server.Instance.OnCallback<{c.Name}>( value );" );
            }
            EndBlock();
        }


        private void CallResult( SteamApiDefinition.StructDef c )
        {
            WriteLine();
            StartBlock( $"internal static CallResult<{c.Name}> CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<{c.Name}, bool> CallbackFunction )" );
            {
                WriteLine( $"return new CallResult<{c.Name}>( steamworks, call, CallbackFunction, FromPointer, StructSize(), CallbackId );" );
            }
            EndBlock();
        }


        private void CallbackCall( SteamApiDefinition.StructDef c )
        {
            WriteLine( "//" );
            WriteLine( "// Create the functions we need for the vtable" );
            WriteLine( "//" );

            StartBlock( "if ( Facepunch.Steamworks.Config.UseThisCall )" );
            {
                CallFunctions( c, "ThisCall", "_" );
            }
            Else();
            {
                CallFunctions( c, "StdCall", "" );
            }
            EndBlock();

            WriteLine( "" );
            WriteLine( "//" );
            WriteLine( "// Create the callback object" );
            WriteLine( "//" );
            WriteLine( $"var cb = new Callback();" );
            WriteLine( $"cb.vTablePtr = handle.vTablePtr;" );
            WriteLine( $"cb.CallbackFlags = steamworks.IsGameServer ? (byte) SteamNative.Callback.Flags.GameServer : (byte) 0;" );
            WriteLine( $"cb.CallbackId = CallbackId;" );

            WriteLine( "" );
            WriteLine( "//" );
            WriteLine( "// Pin the callback, so it doesn't get garbage collected and we can pass the pointer to native" );
            WriteLine( "//" );
            WriteLine( $"handle.PinnedCallback = GCHandle.Alloc( cb, GCHandleType.Pinned );" );
        }

        private void CallFunctions( SteamApiDefinition.StructDef c, string ThisCall, string ThisArg )
        {
            var ThisArgC = ThisArg.Length > 0 ? $"{ThisArg}, " : "";
            var This = ThisArg.Length > 0 ? "This" : "";

            WriteLine( "//" );
            WriteLine( "// Create the VTable by manually allocating the memory and copying across" );
            WriteLine( "//" );
            StartBlock( "if ( Platform.IsWindows )" );
            {
                WriteLine( $"handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTableWin{This} ) ) );" );
                StartBlock( $"var vTable = new Callback.VTableWin{This}" );
                {
                    WriteLine( $"ResultA = OnResult{This}," );
                    WriteLine( $"ResultB = OnResultWithInfo{This}," );
                    WriteLine( $"GetSize = OnGetSize{This}," );
                }
                EndBlock( ";" );

                WriteLine( "handle.FuncA = GCHandle.Alloc( vTable.ResultA );" );
                WriteLine( "handle.FuncB = GCHandle.Alloc( vTable.ResultB );" );
                WriteLine( "handle.FuncC = GCHandle.Alloc( vTable.GetSize );" );

                WriteLine( "Marshal.StructureToPtr( vTable, handle.vTablePtr, false );" );
            }
            Else();
            {
                WriteLine( $"handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable{This} ) ) );" );
                StartBlock( $"var vTable = new Callback.VTable{This}" );
                {
                    WriteLine( $"ResultA = OnResult{This}," );
                    WriteLine( $"ResultB = OnResultWithInfo{This}," );
                    WriteLine( $"GetSize = OnGetSize{This}," );
                }
                EndBlock( ";" );

                WriteLine( "handle.FuncA = GCHandle.Alloc( vTable.ResultA );" );
                WriteLine( "handle.FuncB = GCHandle.Alloc( vTable.ResultB );" );
                WriteLine( "handle.FuncC = GCHandle.Alloc( vTable.GetSize );" );

                WriteLine( "Marshal.StructureToPtr( vTable, handle.vTablePtr, false );" );
            }
            EndBlock();

            
        }
    }
}
