using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public partial class CodeWriter
	{
		public void GenerateInterface( SteamApiDefinition.Interface iface, string folder )
		{
			sb = new StringBuilder();

			WriteLine( $"using System;" );
			WriteLine( $"using System.Runtime.InteropServices;" );
			WriteLine( $"using System.Text;" );
			WriteLine( $"using System.Threading.Tasks;" );
			WriteLine( $"using Steamworks.Data;" );
			WriteLine();

			WriteLine();

			StartBlock( $"namespace Steamworks" );
			{
				StartBlock( $"internal unsafe class {iface.Name} : SteamInterface" );
				{
					WriteLine();
					StartBlock( $"internal {iface.Name}( bool IsGameServer )" );
					{
						WriteLine( $"SetupInterface( IsGameServer );" );
					}
					EndBlock();
					WriteLine();

					if ( iface.Accessors != null )
					{
						foreach ( var func in iface.Accessors )
						{
							WriteLine( $"[DllImport( Platform.LibraryName, EntryPoint = \"{func.Name_Flat}\", CallingConvention = Platform.CC)]" );
							WriteLine( $"internal static extern IntPtr {func.Name_Flat}();" );

							if ( func.Kind == "user" )
							{
								WriteLine( $"public override IntPtr GetUserInterfacePointer() => {func.Name_Flat}();" );
							}
							else  if ( func.Kind == "gameserver" )
							{
								WriteLine( $"public override IntPtr GetServerInterfacePointer() => {func.Name_Flat}();" );
							}
							else if ( func.Kind == "global" )
							{
								WriteLine( $"public override IntPtr GetGlobalInterfacePointer() => {func.Name_Flat}();" );
							}
							else
							{
								throw new Exception( $"unknown Kind {func.Kind}" );
							}
						}

						WriteLine();
						WriteLine();
					}

					foreach ( var func in iface.Methods )
					{
						if ( Cleanup.IsDeprecated( $"{iface.Name}.{func.Name}" ) )
							continue;

						WriteFunction( iface, func );
						WriteLine();
					}

				}
				EndBlock();
			}
			EndBlock();

			System.IO.File.WriteAllText( $"{folder}{iface.Name}.cs", sb.ToString() );
		}

		private void WriteFunction( SteamApiDefinition.Interface iface, SteamApiDefinition.Interface.Method func )
		{
			var returnType = BaseType.Parse( func.ReturnType, null, func.CallResult );
			returnType.Func = func.Name;

			if ( func.Params == null )
				func.Params = new SteamApiDefinition.Interface.Method.Param[0];

			var args = func.Params.Select( x =>
			{
				var bt = BaseType.Parse( x.ParamType, x.ParamName );
				bt.Func = func.Name;
				return bt;
			} ).ToArray();

			for( int i=0; i<args.Length; i++ )
			{
				if ( args[i] is FetchStringType )
				{
					if ( args[i + 1] is IntType || args[i + 1] is UIntType || args[i + 1] is UIntPtrType )
					{
						if ( string.IsNullOrEmpty(  args[i + 1].Ref ) )
						{
							args[i + 1] = new LiteralType( args[i + 1], "(1024 * 32)" );
						}
					}
					else
					{
						throw new System.Exception( $"String Builder Next Type Is {args[i+1].GetType()}" );
					}
				}
			}

			var argstr = string.Join( ", ", args.Where( x => !x.ShouldSkipAsArgument ).Select( x => x.AsArgument() ) ); ;
			var delegateargstr = string.Join( ", ", args.Select( x => x.AsNativeArgument() ) );

			if ( returnType is SteamApiCallType sap )
			{
				sap.CallResult = func.CallResult;
				argstr = string.Join( ", ", args.Select( x => x.AsArgument().Replace( "ref ", " /* ref */ " )  ) );
			}

			WriteLine( $"#region FunctionMeta" );

			WriteLine( $"[DllImport( Platform.LibraryName, EntryPoint = \"{func.FlatName}\", CallingConvention = Platform.CC)]" );
			
			if ( returnType.ReturnAttribute != null )
				WriteLine( returnType.ReturnAttribute );

			WriteLine( $"private static extern {returnType.TypeNameFrom} _{func.Name}( IntPtr self, {delegateargstr} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );

			WriteLine();
			WriteLine( $"#endregion" );


			if ( !string.IsNullOrEmpty( func.Desc ) )
			{
				WriteLine( "/// <summary>" );
				WriteLine( $"/// {func.Desc}" );
				WriteLine( "/// </summary>" );
			}

			StartBlock( $"internal {returnType.ReturnType} {func.Name}( {argstr} )".Replace( "(  )", "()" ) );
			{
				var callargs = string.Join( ", ", args.Select( x => x.AsCallArgument() ) );

				//
				// Code before any calls
				//
				foreach ( var arg in args )
				{
					if ( arg is FetchStringType sb )
					{
						WriteLine( $"using var mem{sb.VarName} = Helpers.TakeMemory();" );
					}
				}

				if ( returnType.IsVoid )
				{
					WriteLine( $"_{func.Name}( Self, {callargs} );".Replace( "( Self,  )", "( Self )" ) );
				}
				else
				{
					WriteLine( $"var returnValue = _{func.Name}( Self, {callargs} );".Replace( "( Self,  )", "( Self )" ) );
				}

				//
				// Code after the call
				//
				foreach ( var arg in args )
				{
					if ( arg is FetchStringType sb )
					{
						WriteLine( $"{sb.VarName} = Helpers.MemoryToString( mem{sb.VarName} );" );
					}
				}

				//
				// Return
				//
				if ( !returnType.IsVoid )
				{
					WriteLine( returnType.Return( "returnValue" ) );
				}
			}
			EndBlock();
		}
	}
}
