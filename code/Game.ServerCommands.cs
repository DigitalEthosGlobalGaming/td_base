
using CandyDefence.Enemies;
using Degg.TDBase;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Sandbox
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class MyGame : Sandbox.Game
	{

		[ServerCmd( "td.client.restart" )]
		public static void Restart( )
		{
			var client = ConsoleSystem.Caller;
			if (client != null)
			{
				TDCurrent.SetupClient( client );
			}
		}

		[ServerCmd( "td.map.tostring" )]
		public static void MapToString()
		{
			var client = ConsoleSystem.Caller;
			if (client?.Pawn is Pawn clientPawn)
			{
				if ( clientPawn?.Map is TDPlayerMap playerMap )
				{
					var str = playerMap.MapToString();
					TDCurrent.LogToClient(To.Single(client), str );
				}
			}
		}


		[AdminCmd( "td.bots.spawn", Help = "Spawn my custom bot." )]
		internal static void SpawnCustomBot()
		{
			Host.AssertServer();

			// Create an instance of your custom bot.
			var bot = new TDBotBase();
		}
	}

}
