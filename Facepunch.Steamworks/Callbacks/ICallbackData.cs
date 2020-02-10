using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Steamworks
{
	//
	// Created on registration of a callback
	//
	internal interface ICallbackData
	{
		int CallbackId { get; }
		int DataSize { get; }
	}
}