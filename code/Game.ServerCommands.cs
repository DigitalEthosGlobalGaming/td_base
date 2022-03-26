
using Degg.GridSystem;
using Degg.Util;
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TDBase;
using TDBase.Enemies;

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
				if ( clientPawn?.Map is PlayerMap playerMap)
				{
					var str = playerMap.MapToString();
					TDCurrent.LogToClient(To.Single(client), str );
				}
			}
		}

		[ServerCmd( "td.enemy.spawn" )]
		public static void SpawnEnemy(int amount = 1)
		{
			var client = ConsoleSystem.Caller;
			if ( client?.Pawn is Pawn clientPawn )
			{
				if ( clientPawn?.Map is PlayerMap playerMap )
				{
					for ( int i = 0; i < amount; i++ )
					{
						playerMap.AddToQueue<EnemyDemon>();
					}					
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
