using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks.ServerList;

internal struct ServerFilterMarshaler : IDisposable
{
	private static readonly int SizeOfPointer = Marshal.SizeOf<IntPtr>();
	private static readonly int SizeOfKeyValuePair = Marshal.SizeOf<MatchMakingKeyValuePair>();

	private IntPtr _arrayPtr;
	private IntPtr _itemsPtr;

	public int Count { get; private set; }
	public IntPtr Pointer => _arrayPtr;

	public ServerFilterMarshaler( MatchMakingKeyValuePair[] filters )
	{
		if ( filters == null || filters.Length == 0 )
		{
			Count = 0;
			_arrayPtr = IntPtr.Zero;
			_itemsPtr = IntPtr.Zero;
			return;
		}

		Count = filters.Length;
		_arrayPtr = Marshal.AllocHGlobal( SizeOfPointer * filters.Length );
		_itemsPtr = Marshal.AllocHGlobal( SizeOfKeyValuePair * filters.Length );

		var arrayDst = _arrayPtr;
		var itemDst = _itemsPtr;
		foreach ( var filter in filters )
		{
			Marshal.WriteIntPtr( arrayDst, itemDst );
			arrayDst += SizeOfPointer;

			Marshal.StructureToPtr( filter, itemDst, false );
			itemDst += SizeOfKeyValuePair;
		}
	}

	public void Dispose()
	{
		if ( _arrayPtr != IntPtr.Zero )
		{
			Marshal.FreeHGlobal( _arrayPtr );
			_arrayPtr = IntPtr.Zero;
		}

		if ( _itemsPtr != IntPtr.Zero )
		{
			Marshal.FreeHGlobal( _itemsPtr );
			_itemsPtr = IntPtr.Zero;
		}
	}
}
