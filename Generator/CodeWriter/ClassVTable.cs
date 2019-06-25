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

			int[] standardLocations = new int[clss.Functions.Count];
			int[] windowsLocations = new int[clss.Functions.Count];

			for ( int i = 0; i < clss.Functions.Count; i++ )
			{
				windowsLocations[i] = i * 8;
				standardLocations[i] = i * 8;
			}

			//
			// MSVC switches the order in the vtable of overloaded functions
			// I'm not going to try to try to work out how to order shit
			// so lets just manually fix shit here
			//
			if ( clss.Name == "ISteamUserStats" )
			{
				Swap( clss, "GetStat1", "GetStat2", windowsLocations );
				Swap( clss, "SetStat1", "SetStat2", windowsLocations );
				Swap( clss, "GetUserStat1", "GetUserStat2", windowsLocations );
				Swap( clss, "GetGlobalStat1", "GetGlobalStat2", windowsLocations );
				Swap( clss, "GetGlobalStatHistory1", "GetGlobalStatHistory2", windowsLocations );
			}

			if ( clss.Name == "ISteamGameServerStats" )
			{
				Swap( clss, "GetUserStat1", "GetUserStat2", windowsLocations );
				Swap( clss, "SetUserStat1", "SetUserStat2", windowsLocations );
			}

			if ( clss.Name == "ISteamUGC" )
			{
				Swap( clss, "CreateQueryAllUGCRequest1", "CreateQueryAllUGCRequest2", windowsLocations );
			}


			StartBlock( $"public override void InitInternals()" );
			{
				var different = new List<int>();

				for ( int i = 0; i < clss.Functions.Count; i++ )
				{
					var func = clss.Functions[i];

					if ( Cleanup.IsDeprecated( $"{clss.Name}.{func.Name}" ) )
					{
						WriteLine( $" // {func.Name} is deprecated" );
					}
					else
					{
						if ( standardLocations[i] != windowsLocations[i] )
						{
							different.Add( i );
							continue;
						}

						WriteLine( $"_{func.Name} = Marshal.GetDelegateForFunctionPointer<F{func.Name}>( Marshal.ReadIntPtr( VTable, {standardLocations[i]} ) );" );
					}
				}

				if ( different.Count > 0 )
				{
					WriteLine( "" );
					WriteLine( "#if PLATFORM_WIN64" );
					foreach ( var i in different )
					{
						var func = clss.Functions[i];
						WriteLine( $"_{func.Name} = Marshal.GetDelegateForFunctionPointer<F{func.Name}>( Marshal.ReadIntPtr( VTable, {windowsLocations[i]} ) );" );
					}
					WriteLine( "#else" );
					foreach ( var i in different )
					{
						var func = clss.Functions[i];
						WriteLine( $"_{func.Name} = Marshal.GetDelegateForFunctionPointer<F{func.Name}>( Marshal.ReadIntPtr( VTable, {standardLocations[i]} ) );" );
					}
					WriteLine( "#endif" );
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

			if ( returnType.IsReturnedWeird )
			{
				WriteLine( "#if PLATFORM_WIN64" );
				WriteLine( $"private delegate void F{func.Name}( IntPtr self, ref {returnType.TypeName} retVal, {delegateargstr} );".Replace( " retVal,  )", " retVal )" ) );
				WriteLine( "#else" );
			}

			WriteLine( $"private delegate {returnType.TypeNameFrom} F{func.Name}( IntPtr self, {delegateargstr} );".Replace( "( IntPtr self,  )", "( IntPtr self )" ) );

			if ( returnType.IsReturnedWeird )
			{
				WriteLine( "#endif" );
			}

			WriteLine( $"private F{func.Name} _{func.Name};" );

			WriteLine();
			WriteLine( $"#endregion" );

			StartBlock( $"internal {returnType.ReturnType} {func.Name}( {argstr} )".Replace( "(  )", "()" ) );
			{
				var callargs = string.Join( ", ", args.Select( x => x.AsCallArgument() ) );

				if ( returnType.IsReturnedWeird )
				{
					WriteLine( "#if PLATFORM_WIN64" );
					{
						WriteLine( $"var retVal = default( {returnType.TypeName} );" );
						WriteLine( $"_{func.Name}( Self, ref retVal, {callargs} );".Replace( ",  );", " );" ) );
						WriteLine( $"{returnType.Return( "retVal" )}" );
					}
					WriteLine( "#else" );
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

				if ( returnType.IsReturnedWeird )
				{
					WriteLine( "#endif" );
				}
			}
			EndBlock();
		}
	}
}
