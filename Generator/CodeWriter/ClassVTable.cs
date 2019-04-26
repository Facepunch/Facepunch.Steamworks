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
					StartBlock( $"public {clss.Name}( bool server = false ) : base( server )" );
					{

					}
					EndBlock();
					WriteLine();

					WriteLine( $"public override string InterfaceName => \"{clss.InterfaceString}\";" );
					WriteLine();

					WriteFunctionPointerReader( clss );

					WriteLine();

					foreach ( var func in clss.Functions )
					{
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

			if ( clss.Name == "ISteamUGC" )
			{
				Swap( clss, "CreateQueryAllUGCRequest1", "CreateQueryAllUGCRequest2", locations );
			}


			StartBlock( $"public override void InitInternals()" );
			{
				for (int i=0; i< clss.Functions.Count; i++ )
				{
					var func = clss.Functions[i];
					WriteLine( $"_{func.Name} = Marshal.GetDelegateForFunctionPointer<F{func.Name}>( Marshal.ReadIntPtr( VTable, {locations[i]}) );" ); 
				}
			}
			EndBlock();
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
		}
	}
}
