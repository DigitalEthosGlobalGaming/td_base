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
			Init<GridSpace>( this.Position, new Vector2( 10.0f, 10.0f ), xAmount, yAmount );
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
