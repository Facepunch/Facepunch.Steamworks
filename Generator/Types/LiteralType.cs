using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Used to replace a variable with a literal.
/// 
/// This is used when we can determine a parameter ourselves. For example
/// if you're passing a buffer and a paramter is the buffer length
/// </summary>

internal class LiteralType : BaseType
{
	private string Value;
	BaseType basetype;

	public LiteralType( BaseType basetype, string value )
	{
		this.basetype = basetype;
		this.Value = value;
	}

	public override bool ShouldSkipAsArgument => true;

	public override string Ref => "";
	public override bool IsVector => false;
	public override string AsArgument() => basetype.AsArgument();
	public override string AsCallArgument() => Value;
}