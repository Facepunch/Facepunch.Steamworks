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
	[DeploymentItem( "steam_api.dll" )]
	[DeploymentItem( "controller_config/game_actions_252490.vdf" )]
    public class InputTest
	{
		[TestMethod]
        public void ControllerList()
        {
			foreach ( var controller in SteamInput.Controllers )
			{
				Console.Write( $"Controller: {controller}" );

				var dstate = controller.GetDigitalState( "fire" );
				var astate = controller.GetAnalogState( "Move" );
			}
		}
	}

}
 