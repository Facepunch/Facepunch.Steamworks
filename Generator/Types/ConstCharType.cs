using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// To pass a string we use a custom marshaller (Utf8StringToNative) to convert it
/// from a utf8 string to a pointer. If we just pass it as a string it won't be utf8 encoded.
/// 
/// To receive we have a special struct called Utf8StringPointer which can implicitly change
/// the pointer to a utf8 string.
/// </summary>
internal class ConstCharType : BaseType
{
	public override string TypeName => $"string";
	public override string TypeNameFrom => $"Utf8StringPointer";
	public override string AsArgument() => $"[MarshalAs( UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof( Utf8StringToNative ) )] {Ref}{TypeName} {VarName}";
	public override string Ref => "";
}