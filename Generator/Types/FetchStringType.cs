using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Passes a pointer to a buffer as an argument, then converts
/// it into a string which is returned via an out param. 
/// 
/// This is used of "char *" parameters which expect you to pass in
/// a buffer to retrieve the text. Usually \0 terminated.
/// </summary>

internal class FetchStringType : BaseType
{
	public override string TypeName => $"string";
	public override string AsArgument() => $"out string {VarName}";

	public override string AsNativeArgument() => $"IntPtr {VarName}";

	public override string AsCallArgument() => $"mem{VarName}";
	public override string Ref => "";
}