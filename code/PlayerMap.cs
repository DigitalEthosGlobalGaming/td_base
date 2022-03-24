using Degg.GridSystem;
using Degg.GridSystem.GridSpaces;
using Sandbox;
using System.Collections.Generic;
using System.Text.Json;

namespace TDBase
{
	public partial class PlayerMap : GridMap
	{

		[Net]
		public float Score { get; set;}

		public List<GridSpace> EnemyPath { get; set; }

		public void Init(int xAmount, int yAmount)
		{
			// Initialises the map.
			// Default tile scale is 10f because our model size is 10f and we want them to be 100f large.
			TileScale = 10f;
			Init<GridSpace>( this.Position, new Vector2( 101.0f, 101.0f ), xAmount, yAmount );

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
