using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public partial class CodeWriter
	{
		public void GenerateVTableClass( string className, string filename )
		{
			var clss = Parser.Classes.Single( x => x.Name == className );

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
				StartBlock( $"internal class {clss.Name} : SteamInterface" );
				{
					WriteLine( $"public override string InterfaceName => \"{clss.InterfaceString}\";" );
					WriteLine();

					WriteFunctionPointerReader( clss );

					WriteLine();

					foreach ( var func in clss.Functions )
					{
						if ( Cleanup.IsDeprecated( $"{clss.Name}.{func.Name}" ) )
							continue;


						WriteFunction( clss, func );
						WriteLine();
					}

				}
				EndBlock();
			}
			EndBlock();

			System.IO.File.WriteAllText( $"{filename}", sb.ToString() );
		}

		void WriteFunctionPointerReader( CodeParser.Class clss )
		{
			// TODO - we'll probably have to do this PER platform

			int[] locations = new int[clss.Functions.Count];

			for ( int i = 0; i < clss.Functions.Count; i++ )
			{
				locations[i] = i * 8;
			}

			//
			// MSVC switches the order in the vtable of overloaded functions
			// I'm not going to try to try to work out how to order shit
			// so lets just manually fix shit here
			//
			if ( clss.Name == "ISteamUserStats" )
			{
				Swap( clss, "GetStat1", "GetStat2", locations );
				Swap( clss, "SetStat1", "SetStat2", locations );
				Swap( clss, "GetUserStat1", "GetUserStat2", locations );
				Swap( clss, "GetGlobalStat1", "GetGlobalStat2", locations );
				Swap( clss, "GetGlobalStatHistory1", "GetGlobalStatHistory2", locations );
			}

			if ( clss.Name == "ISteamGameServerStats" )
			{
				Swap( clss, "GetUserStat1", "GetUserStat2", locations );
				Swap( clss, "SetUserStat1", "SetUserStat2", locations );
			}

			if ( clss.Name == "ISteamUGC" )
			{
				Swap( clss, "CreateQueryAllUGCRequest1", "CreateQueryAllUGCRequest2", locations );
			}


			StartBlock( $"public override void InitInternals()" );
			{
				for (int i=0; i< clss.Functions.Count; i++ )
				{
					var func = clss.Functions[i];
					var returnType = BaseType.Parse( func.ReturnType );
					var args = func.Arguments.Select( x => BaseType.Parse( x.Value, x.Key ) ).ToArray();
					var regularpos = i * 8;
					var windowsSpecific = NeedsWindowsSpecificFunction( func, returnType, args );

					if ( Cleanup.IsDeprecated( $"{clss.Name}.{func.Name}" ) )
					{
						WriteLine( $" // {func.Name} is deprecated - {locations[i]}" );
					}
					else
					{
						var pos = $"Config.Os == OsType.Windows ? {locations[i]} : {regularpos} ";

						if ( regularpos == locations[i] )
							pos = regularpos.ToString();

						WriteLine( $"_{func.Name} = Marshal.GetDelegateForFunctionPointer<F{func.Name}>( Marshal.ReadIntPtr( VTable, {pos}) );" );

						if ( windowsSpecific )
						{
							WriteLine( $"_{func.Name}_Windows = Marshal.GetDelegateForFunctionPointer<F{func.Name}_Windows>( Marshal.ReadIntPtr( VTable, {pos}) );" );
						}
						
					}
				}
			}
			EndBlock();

			StartBlock( $"internal override void Shutdown()" );
			{
				WriteLine( $"base.Shutdown();" );
				WriteLine( "" );

				for ( int i = 0; i < clss.Functions.Count; i++ )
				{
					var func = clss.Functions[i];
					var returnType = BaseType.Parse( func.ReturnType );
					var args = func.Arguments.Select( x => BaseType.Parse( x.Value, x.Key ) ).ToArray();
					var windowsSpecific = NeedsWindowsSpecificFunction( func, returnType, args );

					if ( Cleanup.IsDeprecated( $"{clss.Name}.{func.Name}" ) )
						continue;

					WriteLine( $"_{func.Name} = null;" );

					if ( windowsSpecific )
					{
						WriteLine( $"_{func.Name}_Windows = null;" );
					}

					
				}
			}
			EndBlock();
		}

		private bool NeedsWindowsSpecificFunction( CodeParser.Class.Function func, BaseType returnType, BaseType[] args )
		{
			if ( returnType.IsReturnedWeird ) return true;
			if ( returnType.WindowsSpecific ) return true;
			if ( args.Any( x => x.WindowsSpecific ) ) return true;

			return false;
		}


		private void Swap( CodeParser.Class clss, string v1, string v2, int[] locations )
		{
			var a = clss.Functions.IndexOf( clss.Functions.Single( x => x.Name == v1 ) );
			var b = clss.Functions.IndexOf( clss.Functions.Single( x => x.Name == v2 ) );

			var s = locations[a];
			locations[a] = locations[b];
			locations[b] = s;
		}

		private void WriteFunction( CodeParser.Class clss, CodeParser.Class.Function func )
		{
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

			var windowsSpecific = NeedsWindowsSpecificFunction( func, returnType, args );

			if ( returnType is SteamApiCallType sap )
			{
				sap.CallResult = func.CallResult;

				argstr = string.Join( ", ", args.Select( x => x.AsArgument().Replace( "ref ", " /* ref */ " )  ) );
			}

			WriteLine( $"#region FunctionMeta" );

			WriteLine( $"[UnmanagedFunctionPointer( CallingConvention.ThisCall )]" );

			if ( returnType.ReturnAttribute != null)
				WriteLine( returnType.ReturnAttribute );

			WriteLine( $"private delegate {returnType.TypeNameFrom} F{func.Name}( IntPtr self, {delegateargstr} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );
			WriteLine( $"private F{func.Name} _{func.Name};" );

			if ( windowsSpecific )
			{
				var delegateargstrw = string.Join( ", ", args.Select( x => x.AsWinArgument() ) );
				WriteLine( $"[UnmanagedFunctionPointer( CallingConvention.ThisCall )]" );

				if ( returnType.IsReturnedWeird )
				{
					var windelargs = $"ref {returnType.TypeName} retVal, {delegateargstrw}".Trim( ',', ' ' );
					WriteLine( $"private delegate void F{func.Name}_Windows( IntPtr self, {windelargs} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );
				}
				else
				{
					if ( returnType.ReturnAttribute != null )
						WriteLine( returnType.ReturnAttribute );

					
					WriteLine( $"private delegate {returnType.TypeNameFrom} F{func.Name}_Windows( IntPtr self, {delegateargstrw} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );
				}

				WriteLine( $"private F{func.Name}_Windows _{func.Name}_Windows;" );
			}

			WriteLine();
			WriteLine( $"#endregion" );

			StartBlock( $"internal {returnType.ReturnType} {func.Name}( {argstr} )".Replace( "(  )", "()" ) );
			{
				var callargs = string.Join( ", ", args.Select( x => x.AsCallArgument() ) );

				if ( returnType.IsReturnedWeird )
				{
					StartBlock( "if ( Config.Os == OsType.Windows )" );
					{
						WriteLine( $"var retVal = default( {returnType.TypeName} );" );
						WriteLine( $"_{func.Name}_Windows( Self, ref retVal, {callargs} );".Replace( ",  );", " );" ) );
						WriteLine( $"{returnType.Return( "retVal" )}" );
					}
					EndBlock();
					WriteLine();
				}
				else if ( windowsSpecific )
				{
					StartBlock( "if ( Config.Os == OsType.Windows )" );
					{
						var wincallargs = callargs;

						foreach ( var arg in args )
						{
							if ( !arg.WindowsSpecific ) continue;

							if ( arg.IsVector )
							{
								WriteLine( $"{arg.TypeName}.Pack8[] {arg.VarName}_windows = {arg.VarName};" );
							}
							else
							{
								WriteLine( $"{arg.TypeName}.Pack8 {arg.VarName}_windows = {arg.VarName};" );
							}

							wincallargs = wincallargs.Replace( $" {arg.VarName}", $" {arg.VarName}_windows" );
						}

						if ( !returnType.IsVoid )
							Write( "var retVal = " );

						WriteLine( $"_{func.Name}_Windows( Self, {wincallargs} );".Replace( "( Self,  )", "( Self )" ) );

						foreach ( var arg in args )
						{
							if ( !arg.WindowsSpecific ) continue;

							WriteLine( $"{arg.VarName} = {arg.VarName}_windows;" );
						}

						if ( !returnType.IsVoid )
						{
							WriteLine( returnType.Return( "retVal" ) );
						}
					}
					EndBlock();
					WriteLine();
				}

				if ( returnType.IsVoid )
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
		}
	}
}
