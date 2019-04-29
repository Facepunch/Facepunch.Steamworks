using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
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
					StartBlock( $"internal static class Win64" );
					{
						foreach ( var func in functions )
						{
							WriteMarshalledFunction( func, "steam_api64" );
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
					WriteLine( $"Win64.{func.Name}( ref retVal, {callargs} );" );
					WriteLine( $"{returnType.Return( "retVal" )}" );
				}
				else if ( returnType.IsVoid )
				{
					WriteLine( $"Win64.{func.Name}( {callargs} );" );
				}
				else
				{
					var v = $"Win64.{func.Name}( {callargs} )";

					WriteLine( returnType.Return( v ) );
				}
			}
			EndBlock();

		}

		private void WriteMarshalledFunction( SteamApiDefinition.MethodDef func, string dllName )
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

			WriteLine( $"[DllImport( \"{dllName}\", EntryPoint = \"{func.Name}\", CallingConvention = CallingConvention.Cdecl )]" );

			if ( returnType.ReturnAttribute != null )
				WriteLine( returnType.ReturnAttribute );

			WriteLine( $"public static extern {(returnType.IsReturnedWeird ? "void" : returnType.TypeNameFrom)} {func.Name}( {delegateargstr} );" );
			WriteLine();
		}
			/*

	*/

			/*
			var returnType = BaseType.Parse( func.ReturnType );
			returnType.Func = func.Name;

			var args = func.Arguments.Select( x =>
			{
				var bt = BaseType.Parse( x.Value, x.Key );
				bt.Func = func.Name;
				return bt;
			} ).ToArray();
			var argstr = string.Join( ", ", args.Select( x => x.AsArgument() ) );
			var delegateargstr = string.Join( ", ", args.Select( x => x.AsArgument() ) );

			if ( returnType.IsReturnedWeird  )
			{
				delegateargstr = $"ref {returnType.TypeName} retVal, {delegateargstr}";
				delegateargstr = delegateargstr.Trim( ',', ' ' );
			}

			if ( returnType is SteamApiCallType sap )
			{
				sap.CallResult = func.CallResult;
			}

			WriteLine( $"#region FunctionMeta" );

			WriteLine( $"[UnmanagedFunctionPointer( CallingConvention.ThisCall )]" );

			if ( returnType.ReturnAttribute != null)
				WriteLine( returnType.ReturnAttribute );

			WriteLine( $"private delegate {(returnType.IsReturnedWeird?"void":returnType.TypeNameFrom)} F{func.Name}( IntPtr self, {delegateargstr} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );
			WriteLine( $"private F{func.Name} _{func.Name};" );
			WriteLine();
			WriteLine( $"#endregion" );

			StartBlock( $"internal {returnType.ReturnType} {func.Name}( {argstr} )".Replace( "(  )", "()" ) );
			{
				var callargs = string.Join( ", ", args.Select( x => x.AsCallArgument() ) );

				if ( returnType.IsReturnedWeird )
				{
					WriteLine( $"var retVal = default( {returnType.TypeName} );" );
					WriteLine( $"_{func.Name}( Self, ref retVal, {callargs} );".Replace( ",  );", " );" ) );
					WriteLine( $"{returnType.Return( "retVal" )}" );
				}
				else if ( returnType.IsVoid )
				{
					WriteLine( $"_{func.Name}( Self, {callargs} );".Replace( "( Self,  )", "( Self )" ) );
				}
				else
				{
					var v = $"_{func.Name}( Self, {callargs} )".Replace( "( Self,  )", "( Self )" );

					WriteLine( returnType.Return( v ) );
				}
			}
			EndBlock();
			*/
		
	}
}
