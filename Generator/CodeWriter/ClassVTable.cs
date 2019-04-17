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
			StartBlock( $"public override void InitInternals()" );
			{
				for (int i=0; i< clss.Functions.Count; i++ )
				{
					var func = clss.Functions[i];
					WriteLine( $"_{func.Name} = Marshal.GetDelegateForFunctionPointer<F{func.Name}>( Marshal.ReadIntPtr( VTable, {i*8}) );" ); 
				}
			}
			EndBlock();
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
