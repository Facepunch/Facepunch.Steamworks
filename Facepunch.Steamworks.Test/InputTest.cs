using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Steamworks.Data;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
    public class InputTest
	{

		[TestMethod]
        public void ControllerList()
        {
			foreach ( var c in SteamInput.Controllers )
			{
				Console.WriteLine( "Got Contorller!" );
			}
		}
	}

}
