using Degg.GridSystem;
using Sandbox;
using System.Collections.Generic;

namespace TDBase
{
	public partial class PlayerMap : GridMap
	{

		[Net]
		public float Score { get; set;}

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


		public override void ServerTick()
		{
			base.ServerTick();
		}

	}

}
