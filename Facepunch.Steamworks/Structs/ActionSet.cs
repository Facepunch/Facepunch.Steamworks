using Steamworks.Data;
using System.Collections.Generic;

namespace Steamworks
{
	public struct ActionSet
	{
		internal InputActionSetHandle_t Handle;

		internal ActionSet( InputActionSetHandle_t handle )
		{
			this.Handle = handle;
		}
	}
}