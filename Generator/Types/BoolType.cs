using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Special care needs to be taken with bool types. Apparently BOOL in WINDOWS.H are 4 bytes.
/// But in reality and in native c++ they're 1 byte.
/// Of course marshalling by default expects them to be 4 bytes - because why not eh. So we have
/// to add a few attributes to make sure we don't get fucked over by bill gates again.
/// </summary>
internal class BoolType : BaseType
{
	public override string TypeName => $"bool";
	public override string AsArgument() => $"[MarshalAs( UnmanagedType.U1 )] {Ref}{TypeName} {VarName}";
	public override string ReturnAttribute => "[return: MarshalAs( UnmanagedType.I1 )]";

}