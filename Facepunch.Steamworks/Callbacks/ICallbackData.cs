using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Steamworks
{
	/// <summary>
	/// Gives us a generic way to get the CallbackId of structs
	/// </summary>
	internal interface ICallbackData
	{
		CallbackType CallbackType { get; }
		int DataSize { get; }
	}
}