using Degg.TDBase;
using System.Collections.Generic;

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
		public static List<Pawn> ActivePawns { get; set; }
		public static MyGame TDCurrent { get; protected set; }

		public MyGame(): base()
		{
			TDCurrent = this;
			ActivePawns = new List<Pawn>();
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );
			SetupClient( client );
		}


		public static void cl_restart()
		{

		}

		public static void AddActivePawn(Pawn p)
		{
			if (ActivePawns.IndexOf(p) == -1)
			{
				ActivePawns.Add( p );
			}
		}

		public static void RemoveActivePawn( Pawn p )
		{
			if ( ActivePawns.IndexOf( p ) >= 0 )
			{
				ActivePawns.Remove( p );
			}
		}

		public void SetupBot( TDBotBase bot )
		{
			if ( IsServer )
			{
				if ( bot.Pawn != null )
				{

					Pawn botPawn = bot.Pawn;
					bot.Pawn = null;
					botPawn.Delete();
				}
			

				// Create a pawn for this client to play with
				var pawn = new Pawn();
				bot.Pawn = pawn;
			}
		}

		public void SetupClient(Client client)
		{
			if ( client.Pawn != null )
			{
				if ( IsServer )
				{
					Pawn clientPawn = (Pawn) client.Pawn;
					client.Pawn = null;
					clientPawn.Delete();
				}
			}

			// Create a pawn for this client to play with
			var pawn = new Pawn();
			client.Pawn = pawn;
		}
	}

}
