using System;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Facepunch.Steamworks.Test
{
    public partial class Client
    {
        [TestMethod]
        public void UpdateStats()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                client.Stats.UpdateStats();
            }
        }

        [TestMethod]
        public void UpdateSUpdateGlobalStatstats()
        {
            using ( var client = new Facepunch.Steamworks.Client( 252490 ) )
            {
                client.Stats.UpdateGlobalStats( 1 );
                client.Stats.UpdateGlobalStats( 3 );
                client.Stats.UpdateGlobalStats( 7 );
            }
        }
    }
}
