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
            foreach ( var c in def.structs )
            {
                if ( SkipStructs.Contains( c.Name ) )
                    continue;

                if ( c.Name.Contains( "::" ) )
                    continue;

                int defaultPack = 8;

                if ( c.Fields.Any( x => x.Type.Contains( "class CSteamID" ) ) && !ForceLargePackStructs.Contains( c.Name ) )
                    defaultPack = 4;

                //
                // Main struct
                //
                WriteLine( $"[StructLayout( LayoutKind.Sequential, Pack = {defaultPack} )]" );
                StartBlock( $"internal struct {c.Name}" );
                {
                    if ( !string.IsNullOrEmpty( c.CallbackId ) )
                    {
                        WriteLine( "public const int CallbackId = " + c.CallbackId + ";" );
                    }

                    //
                    // The fields
                    //
                    StructFields( c.Fields );

                    WriteLine();
                    WriteLine( "//" );
                    WriteLine( "// Read this struct from a pointer, usually from Native. It will automatically do the awesome stuff." );
                    WriteLine( "//" );
                    StartBlock( $"public static {c.Name} FromPointer( IntPtr p )" );
                    {
                        WriteLine( $"if ( Platform.PackSmall ) return (PackSmall) Marshal.PtrToStructure( p, typeof(PackSmall) );" );

                        WriteLine( $"return ({c.Name}) Marshal.PtrToStructure( p, typeof({c.Name}) );" );
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
                    }

                }
                EndBlock();
                WriteLine();
            }
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
                    WriteLine( $"[MarshalAs(UnmanagedType.ByValArray, SizeConst = {num}, ArraySubType = UnmanagedType.U8)]" );
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

                WriteLine( $"public {t} {CleanMemberName( m.Name )}; // {m.Name} {m.Type}" );
            }
        }

        private void Callback( SteamApiDefinition.StructDef c )
        {
            WriteLine();
            StartBlock( $"public static void RegisterCallback( Facepunch.Steamworks.BaseSteamworks steamworks, Action<{c.Name}, bool> CallbackFunction )" );
            {
                WriteLine( $"var handle = new CallbackHandle();" );
                WriteLine( $"handle.steamworks = steamworks;" );
                WriteLine( $"" );

                CallbackCallresultShared( c, false );

                WriteLine( "" );
                WriteLine( "//" );
                WriteLine( "// Register the callback with Steam" );
                WriteLine( "//" );
                WriteLine( $"steamworks.native.api.SteamAPI_RegisterCallback( handle.PinnedCallback.AddrOfPinnedObject(), CallbackId );" );

                WriteLine();
                WriteLine( "steamworks.RegisterCallbackHandle( handle );" );
            }
            EndBlock();
        }


        private void CallResult( SteamApiDefinition.StructDef c )
        {
            WriteLine();
            StartBlock( $"public static CallbackHandle CallResult( Facepunch.Steamworks.BaseSteamworks steamworks, SteamAPICall_t call, Action<{c.Name}, bool> CallbackFunction )" );
            {
                WriteLine( $"var handle = new CallbackHandle();" );
                WriteLine( $"handle.steamworks = steamworks;" );
                WriteLine( $"handle.CallResultHandle = call;" );
                WriteLine( $"handle.CallResult = true;" );
                WriteLine( $"" );

                CallbackCallresultShared( c, true );

                WriteLine( "" );
                WriteLine( "//" );
                WriteLine( "// Register the callback with Steam" );
                WriteLine( "//" );
                WriteLine( $"steamworks.native.api.SteamAPI_RegisterCallResult( handle.PinnedCallback.AddrOfPinnedObject(), call );" );

                WriteLine();
                WriteLine( "return handle;" );
            }
            EndBlock();
        }


        private void CallbackCallresultShared( SteamApiDefinition.StructDef c, bool Result )
        {
            WriteLine( "//" );
            WriteLine( "// Create the functions we need for the vtable" );
            WriteLine( "//" );

            StartBlock( "if ( Facepunch.Steamworks.Config.UseThisCall )" );
            {
                CallresultFunctions( c, Result, "ThisCall", "_" );
            }
            Else();
            {
                CallresultFunctions( c, Result, "StdCall", "" );
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

        private void CallresultFunctions( SteamApiDefinition.StructDef c, bool Result, string ThisCall, string ThisArg )
        {
            var ThisArgC = ThisArg.Length > 0 ? $"{ThisArg}, " : "";

            if ( Result )
            {
                WriteLine( $"Callback.{ThisCall}.Result         funcA = ( {ThisArgC}p ) => {{  handle.Dispose(); CallbackFunction( FromPointer( p ), false ); }};" );
                StartBlock( $"Callback.{ThisCall}.ResultWithInfo funcB = ( {ThisArgC}p, bIOFailure, hSteamAPICall ) => " );
                {
                    WriteLine( "if ( hSteamAPICall != call ) return;" );
                    WriteLine();
                    WriteLine( "handle.CallResultHandle = 0;" );
                    WriteLine( "handle.Dispose();" );
                    WriteLine();
                    WriteLine( "CallbackFunction( FromPointer( p ), bIOFailure );" );
                }
                EndBlock( ";" );
            }
            else
            {
                WriteLine( $"Callback.{ThisCall}.Result         funcA = ( {ThisArgC}p ) => {{ CallbackFunction( FromPointer( p ), false ); }};" );
                WriteLine( $"Callback.{ThisCall}.ResultWithInfo funcB = ( {ThisArgC}p, bIOFailure, hSteamAPICall ) => {{ CallbackFunction( FromPointer( p ), bIOFailure ); }};" );
            }

            WriteLine( $"Callback.{ThisCall}.GetSize        funcC = ( {ThisArg} ) => Marshal.SizeOf( typeof( {c.Name} ) );" );
            WriteLine();
            WriteLine( "//" );
            WriteLine( "// If this platform is PackSmall, use PackSmall versions of everything instead" );
            WriteLine( "//" );
            StartBlock( "if ( Platform.PackSmall )" );
            {
                WriteLine( $"funcC = ( {ThisArg} ) => Marshal.SizeOf( typeof( PackSmall ) );" );
            }
            EndBlock();

            WriteLine( "" );
            WriteLine( "//" );
            WriteLine( "// Allocate a handle to each function, so they don't get disposed" );
            WriteLine( "//" );
            WriteLine( "handle.FuncA = GCHandle.Alloc( funcA, GCHandleType.Pinned );" );
            WriteLine( "handle.FuncB = GCHandle.Alloc( funcB, GCHandleType.Pinned );" );
            WriteLine( "handle.FuncC = GCHandle.Alloc( funcC, GCHandleType.Pinned );" );
            WriteLine();

            WriteLine( "//" );
            WriteLine( "// Create the VTable by manually allocating the memory and copying across" );
            WriteLine( "//" );
            WriteLine( "handle.vTablePtr = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Callback.VTable ) ) );" );
            StartBlock( "var vTable = new Callback.VTable()" );
            {
                WriteLine( "ResultA = Marshal.GetFunctionPointerForDelegate( funcA )," );
                WriteLine( "ResultB = Marshal.GetFunctionPointerForDelegate( funcB )," );
                WriteLine( "GetSize = Marshal.GetFunctionPointerForDelegate( funcC )," );
            }
            EndBlock( ";" );

            WriteLine( "//" );
            WriteLine( "// The order of these functions are swapped on Windows" );
            WriteLine( "//" );
            StartBlock( "if ( Platform.IsWindows )" );
            {
                WriteLine( "vTable.ResultA = Marshal.GetFunctionPointerForDelegate( funcB );" );
                WriteLine( "vTable.ResultB = Marshal.GetFunctionPointerForDelegate( funcA );" );
            }
            EndBlock();

            WriteLine( "Marshal.StructureToPtr( vTable, handle.vTablePtr, false );" );
        }
    }
}
