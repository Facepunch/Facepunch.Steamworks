using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	/*
	public partial class CodeWriter
	{
		public void GenerateGlobalFunctions( string startingWith, string filename )
		{
			var functions = def.methods.Where( x => x.Name.StartsWith( startingWith ) );

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
				StartBlock( $"internal static class {startingWith}" );
				{
					StartBlock( $"internal static class Native" );
					{
						foreach ( var func in functions )
						{
							WriteMarshalledFunction( func );
						}
					}
					EndBlock();

					foreach ( var func in functions )
					{
						WriteGlobalFunction( startingWith, func );
						WriteLine();
					}

				}
				EndBlock();
			}
			EndBlock();

			System.IO.File.WriteAllText( $"{filename}", sb.ToString().Replace( "(  )", "()" ) );
		}

		private void WriteGlobalFunction( string cname, SteamApiDefinition.MethodDef func )
		{
			var cleanName = func.Name.Substring( cname.Length ).Trim( '_' );

			var returnType = BaseType.Parse( func.ReturnType );
			returnType.Func = func.Name;

			if ( func.Params == null )
				func.Params = new SteamApiDefinition.MethodDef.ParamType[0];

			var args = func.Params.Select( x =>
			{
				var bt = BaseType.Parse( x.Type, x.Name );
				bt.Func = func.Name;
				return bt;
			} ).ToArray();
			var argstr = string.Join( ", ", args.Select( x => x.AsArgument() ) );
			var delegateargstr = string.Join( ", ", args.Select( x => x.AsArgument() ) );

			if ( returnType.IsReturnedWeird )
			{
				throw new System.Exception( "TODO" );
			}

			StartBlock( $"static internal {returnType.ReturnType} {cleanName}( {argstr} )" );
			{
				var callargs = string.Join( ", ", args.Select( x => x.AsCallArgument() ) );

				if ( returnType.IsReturnedWeird )
				{
					WriteLine( $"var retVal = default( {returnType.TypeName} );" );
					WriteLine( $"Native.{func.Name}( ref retVal, {callargs} );" );
					WriteLine( $"{returnType.Return( "retVal" )}" );
				}
				else if ( returnType.IsVoid )
				{
					WriteLine( $"Native.{func.Name}( {callargs} );" );
				}
				else
				{
					var v = $"Native.{func.Name}( {callargs} )";

					WriteLine( returnType.Return( v ) );
				}
			}
			EndBlock();

		}

		private void WriteMarshalledFunction( SteamApiDefinition.MethodDef func )
		{
			var returnType = BaseType.Parse( func.ReturnType );
			returnType.Func = func.Name;

			if ( func.Params == null )
				func.Params = new SteamApiDefinition.MethodDef.ParamType[0];

			var args = func.Params.Select( x =>
			{
				var bt = BaseType.Parse( x.Type, x.Name );
				bt.Func = func.Name;
				return bt;
			} ).ToArray();
			var argstr = string.Join( ", ", args.Select( x => x.AsArgument() ) );
			var delegateargstr = string.Join( ", ", args.Select( x => x.AsArgument() ) );

			WriteLine( $"[DllImport( Platform.LibraryName, EntryPoint = \"{func.Name}\", CallingConvention = CallingConvention.Cdecl )]" );

			if ( returnType.ReturnAttribute != null )
				WriteLine( returnType.ReturnAttribute );

			WriteLine( $"public static extern {(returnType.IsReturnedWeird ? "void" : returnType.TypeNameFrom)} {func.Name}( {delegateargstr} );" );
			WriteLine();
		}

	
	}
	*/
}
