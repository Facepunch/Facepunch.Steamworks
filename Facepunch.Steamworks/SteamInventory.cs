using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	/// <summary>
	/// Undocumented Parental Settings
	/// </summary>
	public static class SteamInventory
	{
		static ISteamInventory _internal;
		internal static ISteamInventory Internal
		{
			get
			{
				if ( _internal == null )
					_internal = new ISteamInventory();

				return _internal;
			}
		}

		internal static void InstallEvents()
		{
			new Event<SteamInventoryFullUpdate_t>( x => OnInventoryUpdated?.Invoke() );
			new Event<SteamInventoryDefinitionUpdate_t>( x => OnDefinitionsUpdated?.Invoke() );
		}

		public static event Action OnInventoryUpdated;
		public static event Action OnDefinitionsUpdated;

	}
}