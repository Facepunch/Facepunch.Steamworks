using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Inventory
    {
        /// <summary>
        /// An item in your inventory.
        /// </summary>
        public class Item
        {
            public ulong Id;
            public int Quantity;

            public int DefinitionId;

            /// <summary>
            /// Careful, this might not be available. Especially on a game server.
            /// </summary>
            public Definition Definition;

            public bool TradeLocked;
        }
    }
}
