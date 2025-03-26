using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Steamworks
{
    [TestClass]
    [DeploymentItem( "steam_api64.dll" )]
	[DeploymentItem( "steam_api.dll" )]
	[DeploymentItem( "controller_config/game_actions_252490.vdf" )]
    public class InputTest
	{
		[ClassInitialize]
		public static void ClassInitialize( TestContext context )
		{
			const bool explicitlyCallEveryFrame = true;
			bool initialized = SteamInput.Init( explicitlyCallEveryFrame );
			Assert.IsTrue( initialized );
		}

		[TestInitialize]
		public void Setup()
		{
			SteamInput.RunFrame();
		}

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
		public void ControllerListNoAlloc()
		{
			List<Controller> controllers = new List<Controller>();
			SteamInput.GetControllerNoAlloc( controllers );
			foreach ( var controller in controllers )
			{
				Console.Write( $"Controller: {controller}" );

				var dstate = controller.GetDigitalState( "fire" );
				var astate = controller.GetAnalogState( "Move" );
			}

			CollectionAssert.AreEqual( SteamInput.Controllers.ToList(), controllers );
		}

		[TestMethod]
		public void ControllerAllocates()
		{
			bool allocates = Allocates( () => _ = SteamInput.Controllers );
			Assert.IsTrue( allocates , "Expected default SteamInput.Controllers to allocate");
		}

		[TestMethod]
		public void ControllerListNoAllocDoesNotAllocate()
		{
			// Ensure list is big enough to fit all possible controllers
			// so allocation of list isn't a concern
			List<Controller> controllers = new List<Controller>( 1_000 );
			bool allocates = Allocates( () => SteamInput.GetControllerNoAlloc( controllers ) );
			Assert.IsFalse( allocates , 
				"Expected new allocation-free SteamInput.GetControllerNoAlloc to not allocate");
		}

		// Best guess at allocation test, need NUnit's Is.Not.AllocatingGCMemory() if we want 
		// something better
		private static bool Allocates( Action action )
		{
			// Warm up JIT
			action();

			// Try our best to ensure GC doesn't kick in or around action execution
			GC.Collect();
			long before = GC.GetAllocatedBytesForCurrentThread();
			action();
			long after = GC.GetAllocatedBytesForCurrentThread();

			return before < after;
		}
	}
}
