using Degg.GridSystem;
using Degg.TDBase.Tools;
using Degg.Util;
using Degg.Utils;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Degg.TDBase
{
	public partial class TDPlayerMap : GridMap
	{

		[Net]
		public float Score { get; set;}
		public List<EnemyBase> EnemyEntities { get; set; }
		public List<GridSpace> EnemyPath { get; set; }
		public List<RoundBase> Rounds { get; set; }
		public RoundBase CurrentRound { get; set; }

		public Timer RoundChecker { get; set; }
		public int CurrentRoundNumber { get; set; }

		public virtual void Init(int xAmount, int yAmount)
		{
			// Initialises the map.
			// Default tile scale is 10f because our model size is 10f and we want them to be 100f large.
			TileScale = 10f;
			Init<GridSpace>( this.Position, new Vector2( 101.0f, 101.0f ), xAmount, yAmount );
			CurrentRoundNumber = -1;
		}

		public void StartNextRound()
		{
			CurrentRoundNumber = CurrentRoundNumber + 1;
			if ( CurrentRoundNumber <= Rounds.Count)
			{
				var previousRound = CurrentRound;
				if (previousRound != null)
				{
					previousRound.Stop();
				}

				var round = Rounds[CurrentRoundNumber];
				CurrentRound = round;
				round.Start();
			}
		}

		public RoundBase AddRound<T>() where T: RoundBase, new()
		{
			return AddRound( new T() );
		}

		public List<RoundBase> AddRounds( List<RoundBase> rounds )
		{
			foreach ( var round in rounds )
			{
				AddRound( round );
			}

			return rounds;
		}

		public RoundBase AddRound( RoundBase round)
		{
			if ( Rounds  == null)
			{
				Rounds = new List<RoundBase>();
			}
			round.Map = this;
			Rounds.Add(round);
			return round;
		}


		public override void OnSpaceSetup(GridSpace space)
		{
			space.SetModel( "models/tiles/tile.vmdl" );
			space.SetupPhysicsFromModel( PhysicsMotionType.Static );
		}

		public override void OnSetup()
		{
			base.OnSetup();
			CreateEnemyPath();
			if ( Rounds  == null)
			{
				Rounds = new List<RoundBase>();
			}
			if ( EnemyEntities == null)
			{
				EnemyEntities = new List<EnemyBase>();
			}

			if ( RoundChecker == null)
			{
				RoundChecker = new Timer(CheckRound, 1000 );
				RoundChecker.Start();
			}


			if ( CurrentRoundNumber == -1 )
			{
				StartNextRound();
			}
		}

		public void CheckRound(Timer t)
		{
			if ( CurrentRound != null )
			{
				if (CurrentRound.HasRoundEnded())
				{
					StartNextRound();
				}
			}
		}

		public void CreateEnemyPath()
		{
			EnemyPath = new List<GridSpace>();
			var startSpace = GetGridAsList().Find( (item) =>
			 {
				 if (item is TDGridSpace space)
				 {
					 if ( space.IsSpawner())
					 {
						 return true;
					 }
				 }
				 return false;
			 });

			var endSpace = GetGridAsList().Find( ( item ) =>
			{
				if ( item is TDGridSpace space )
				{
					if ( space.IsEnd() )
					{
						return true;
					}
				}
				return false;
			});

			if (startSpace != null && endSpace != null)
			{
				var path = CreatePath( startSpace, endSpace );
				if (path.Count > 0)
				{
					EnemyPath = path;
				}
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			if (EnemyEntities != null)
			{
				foreach ( var item in EnemyEntities )
				{
					item.Delete();
				}
			}
			if ( Rounds != null )
			{
				foreach ( var round in Rounds )
				{
					round.Delete();
				}
			}

			if ( RoundChecker != null)
			{
				RoundChecker.Delete();
			}
		}

		public override void ServerTick()
		{
			base.ServerTick();
		}

		public virtual void LoadFromString(string str)
		{
			byte[] parts = JsonSerializer.Deserialize<byte[]>( str );

			for ( int i = 0; i < Grid.Count; i++ )
			{
				var part = parts[i];
				var existing = Grid[i];
				if (part > 0)
				{


					TDGridSpace tile = AddTile<TDGridSpace>( (int)existing.GridPosition.x, (int)existing.GridPosition.y );
					tile.Type = part - 1;
					tile.UpdateModel();
				}
			}

			OnSetup();
		}

		public void AddRound()
		{

		}

		public virtual string MapToString()
		{
			byte[] parts = new byte[Grid.Count];
			for ( int i = 0; i < Grid.Count; i++ )
			{
				
				if (Grid[i] is TDGridSpace gs )
				{
					parts[i] = (byte) (gs.Type + 1);
				} else
				{
					parts[i] = 0;
				}

			}
			var data = JsonSerializer.Serialize( parts );

			return data;
		}

	}

}
