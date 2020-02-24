using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        void StructFunctions()
        {
            foreach ( var c in def.structs.Union( def.callback_structs.Select( x => x as SteamApiDefinition.StructDef ) ).OrderBy( x => x.Name ) )
            {
				var name = Cleanup.ConvertType( c.Name );

                if ( name.Contains( "::" ) )
                    continue;

                if ( c.Methods == null || c.Methods.Length == 0 )
                    continue;

                //
                // Main struct
                //
                StartBlock( $"{Cleanup.Expose( name )} partial struct {name}" );
                {
                    foreach ( var func in c.Methods )
                    {
                        if ( func.Name.Contains( "operator" ) )
                            func.Name = func.FlatName.Substring( func.FlatName.LastIndexOf( '_' ) + 1 );

                        var returnType = BaseType.Parse( func.ReturnType, null, func.CallResult );
                        returnType.Func = func.Name;

                        var args = func.Params.Select( x =>
                        {
                            var bt = BaseType.Parse( x.ParamType, x.ParamName );
                            bt.Func = func.Name;
                            return bt;
                        } ).ToArray();

                        for ( int i = 0; i < args.Length; i++ )
                        {
                            if ( args[i] is FetchStringType )
                            {
                                if ( args[i + 1] is IntType || args[i + 1] is UIntType || args[i + 1] is UIntPtrType )
                                {
                                    if ( string.IsNullOrEmpty( args[i + 1].Ref ) )
                                    {
                                        args[i + 1] = new LiteralType( args[i + 1], "(1024 * 32)" );
                                    }
                                }
                                else
                                {
                                    throw new System.Exception( $"String Builder Next Type Is {args[i + 1].GetType()}" );
                                }
                            }
                        }

                        var delegateargstr = string.Join( ", ", args.Select( x => x.AsNativeArgument() ) );

                        if ( !string.IsNullOrEmpty( func.Desc ) )
                        {
                            WriteLine( "/// <summary>" );
                            WriteLine( $"/// {func.Desc}" );
                            WriteLine( "/// </summary>" );
                        }

                        if ( returnType.ReturnAttribute != null )
                            WriteLine( returnType.ReturnAttribute );

                        var _unsafe = "";
                        var firstArg = $"ref {name} self";

                        //
                        // If this is NetMsg then the ORIGINAL pointer address is important 
                        // because we need to pass in the original pointer - not just the data
                        //
                        if ( name == "NetMsg" )
                        {
                            firstArg = $"{name}* self";
                            _unsafe = " unsafe";
                        }

                        WriteLine( $"[DllImport( Platform.LibraryName, EntryPoint = \"{func.FlatName}\", CallingConvention = Platform.CC)]" );
                        WriteLine( $"internal static{_unsafe} extern {returnType.TypeNameFrom} Internal{func.Name}( {firstArg}, {delegateargstr} );".Replace( $"( {firstArg},  )", $"( {firstArg} )" ) );
                        WriteLine();

                    }


                }
                EndBlock();
                WriteLine();
            }
        }
    }
}
