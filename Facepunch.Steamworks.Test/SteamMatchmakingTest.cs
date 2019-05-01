using System;
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
    public class SteamMatchmakingTest
	{
		[TestMethod]
		public async Task LobbyList()
		{
			await CreateLobby();

			var list = await SteamMatchmaking.LobbyList
									.RequestAsync();

			if ( list == null )
			{
				Console.WriteLine( "No Lobbies Found!" );
				return;
			}

			foreach ( var lobby in list )
			{
				Console.WriteLine( $"[{lobby.Id}] owned by {lobby.Owner} ({lobby.MemberCount}/{lobby.MaxMembers})" );
			}
		}

		[TestMethod]
		public async Task LobbyListWithAtLeastOne()
		{
			await CreateLobby();
			await LobbyList();
		}

		[TestMethod]
		public async Task CreateLobby()
		{
			var lobbyr = await SteamMatchmaking.CreateLobbyAsync( 32 );
			if ( !lobbyr.HasValue )
			{
				Assert.Fail();
			}

			var lobby = lobbyr.Value;
			lobby.SetPublic();
			lobby.SetData( "gametype", "sausage" );
			lobby.SetData( "dicks", "unlicked" );

			Console.WriteLine( $"lobby: {lobby.Id}" );

			foreach ( var entry in lobby.Data )
			{
				Console.WriteLine( $" - {entry.Key} {entry.Value}" );
			}

			Console.WriteLine( $"members: {lobby.MemberCount}/{lobby.MaxMembers}" );

			Console.WriteLine( $"Owner: {lobby.Owner}" );
			Console.WriteLine( $"Owner Is Local Player: {lobby.Owner.IsMe}" );

			lobby.SendChatString( "Hello I Love Lobbies" );
		}
	}

}
