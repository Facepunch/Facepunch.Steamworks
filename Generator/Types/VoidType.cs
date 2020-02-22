using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Nothing - just priny void
/// </summary>
internal class VoidType : BaseType
{
	public override string TypeName => $"void";
	public override string TypeNameFrom => $"void";
	public override string Return( string varname ) => "";
	public override bool IsVoid => true;
}