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
	private BaseType baseType;

	public LiteralType( BaseType baseType, string value )
	{
		this.baseType = baseType;
		this.Value = value;

		VarName = baseType.VarName;
	}

	public bool IsOutValue => !string.IsNullOrEmpty( Ref );
	public string OutVarDeclaration => IsOutValue ? $"{baseType.TypeName} sz{VarName} = {Value};" : null;

	public override bool ShouldSkipAsArgument => true;

	public override string Ref => baseType.Ref;
	public override bool IsVector => false;
	public override string AsArgument() => baseType.AsArgument();
	public override string AsCallArgument() => string.IsNullOrEmpty( Ref ) ? Value : $"{Ref}sz{VarName}";
}
