using Degg.GridSystem;
using Degg.GridSystem.GridSpaces;
using Degg.Util;
using Degg.Utils;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Text.Json;
using TDBase.Enemies;

namespace TDBase
{
	public partial class PlayerMap : GridMap
	{

		[Net]
		public float Score { get; set;}

		public Queue<Type> Enemies { get; set; }
		public List<EnemyBase> EnemyEntities { get; set; }

		public Timer EnemySpawner { get; set; }
		public Timer RoundSystem { get; set; }
		public List<GridSpace> EnemyPath { get; set; }

		public void Init(int xAmount, int yAmount)
		{
			// Initialises the map.
			// Default tile scale is 10f because our model size is 10f and we want them to be 100f large.
			TileScale = 10f;
			Init<GridSpace>( this.Position, new Vector2( 101.0f, 101.0f ), xAmount, yAmount );
			Enemies = new Queue<Type>();

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
			if ( RoundSystem == null )
			{
				RoundSystem = new Timer( ( a, b, c ) =>
				 {
					 AdvLog.Info( a, b, c );
					 RoundStart();
				 }, 1500 );
			}

			if ( EnemySpawner == null)
			{
				EnemySpawner = new Timer((a,b,c) =>
					{
						SpawnEnemy();
					}, 1000);
				EnemySpawner.Start();
			}

			if ( EnemyEntities == null)
			{
				EnemyEntities = new List<EnemyBase>();
			}
			
		}

		public void AddToQueue<T>() where T: EnemyBase, new()
		{
			if ( Enemies != null )
			{
				Log.Info( "Added to queue" );
				Enemies.Enqueue( typeof( T ) );
			}
		}

		public void SpawnEnemy()
		{

			if ( Enemies != null )
			{
				
				if ( Enemies.TryDequeue( out var myEnemy ) )
				{
					var enemy = Library.Create<EnemyBase>( myEnemy );
					enemy.Map = this;
					enemy.Setup();
					EnemyEntities.Add( enemy );
				}
			}
			
		}

		public void RoundStart()
		{
			Log.Info( "Round Start" );
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
			if (RoundSystem != null)
			{
				RoundSystem.Delete();
			}
			if (EnemyEntities != null)
			{
				foreach ( var item in EnemyEntities )
				{
					item.Delete();
				}
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
