using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public partial class CodeWriter
	{
		public void GenerateVTableClass( SteamApiDefinition.Interface iface, string folder )
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
				StartBlock( $"internal class {iface.Name} : SteamInterface" );
				{
					WriteLine( $"public override IntPtr GetInterfacePointer() => GetApi.{iface.Name.Substring( 1 )}();" );
					WriteLine();
					WriteLine();
					StartBlock( $"internal {iface.Name}()" );
					{
						WriteLine( $"SetupInterface();" );
					}
					EndBlock();
					WriteLine();

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
			var returnType = BaseType.Parse( func.ReturnType, null );
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
				if ( args[i] is StringType )
				{
					if ( args[i + 1] is IntType  || args[i + 1] is UIntType )
					{
						if ( args[i + 1].Ref == string.Empty )
						{
							args[i + 1] = new ConstValueType( args[i + 1], "(1024 * 32)" );
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

			//if ( returnType is SteamApiCallType sap )
		//	{
			//	sap.CallResult = func.CallResult;

		//		argstr = string.Join( ", ", args.Select( x => x.AsArgument().Replace( "ref ", " /* ref */ " )  ) );
		//	}

			WriteLine( $"#region FunctionMeta" );

			WriteLine( $"[DllImport( Platform.LibraryName, EntryPoint = \"{func.FlatName}\")]" );
			
			if ( returnType.ReturnAttribute != null )
				WriteLine( returnType.ReturnAttribute );

			WriteLine( $"private static extern {returnType.TypeNameFrom} _{func.Name}( IntPtr self, {delegateargstr} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );

			WriteLine();
			WriteLine( $"#endregion" );

			StartBlock( $"internal {returnType.ReturnType} {func.Name}( {argstr} )".Replace( "(  )", "()" ) );
			{
				var callargs = string.Join( ", ", args.Select( x => x.AsCallArgument() ) );

				//
				// Code before any calls
				//
				foreach ( var arg in args )
				{
					if ( arg is StringType sb )
					{
						WriteLine( $"IntPtr mem{sb.VarName} = Helpers.TakeMemory();" );
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
					if ( arg is StringType sb )
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
