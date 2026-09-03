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

		[TestMethod]
		public void Haptics()
		{
			foreach ( var controller in SteamInput.Controllers )
			{
				Console.WriteLine( $"Controller: {controller}" );

				// Body motors, then the Xbox trigger motors, with a stop in between so the two are distinguishable by hand
				controller.TriggerVibration( 30000, 30000 );
				Thread.Sleep( 300 );
				controller.StopVibration();
				Thread.Sleep( 200 );

				controller.TriggerVibrationExtended( 0, 0, 40000, 40000 );
				Thread.Sleep( 300 );
				controller.StopVibration();
				Thread.Sleep( 200 );

				// Voice-coil tick, a no-op on Xbox pads
				controller.TriggerSimpleHapticEvent( ControllerHapticLocation.Both, 200, 0, 200, 0 );

				var motion = controller.GetMotionData();
				Console.WriteLine( $"Motion accel: {motion.PosAccelX}, {motion.PosAccelY}, {motion.PosAccelZ}" );
			}
		}
	}

}
 