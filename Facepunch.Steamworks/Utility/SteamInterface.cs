using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	internal abstract class SteamInterface
	{
		public virtual IntPtr GetUserInterfacePointer() => IntPtr.Zero;
		public virtual IntPtr GetServerInterfacePointer() => IntPtr.Zero;
		public virtual IntPtr GetGlobalInterfacePointer() => IntPtr.Zero;

		public IntPtr Self;

		public bool IsValid => Self != IntPtr.Zero;

		internal void SetupInterface( bool gameServer )
		{
			Self = GetGlobalInterfacePointer();

			if ( Self != IntPtr.Zero )
				return;

			if ( gameServer )
				Self = GetServerInterfacePointer();
			else
				Self = GetUserInterfacePointer();
		}

		internal void ShutdownInterface()
		{
			Self = IntPtr.Zero;
		}
	}

	public abstract class SteamClass
	{
		internal abstract void InitializeInterface( bool server );
		internal virtual void DestroyInterface()
		{
			Interface.ShutdownInterface();
		}

		internal abstract SteamInterface Interface { get; }
	}

}