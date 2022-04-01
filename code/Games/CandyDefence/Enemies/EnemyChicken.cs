using Degg.TDBase;
using Sandbox;
using System;

namespace CandyDefence.Enemies
{
	[Library]
	public partial class EnemyChicken : EnemyBase
	{
		public override string EnemyName => "Chicken";
		public override float BaseHealth => 5f;
		
		public override void Setup()
		{
			base.Setup();
			Rewards.Add( "Candies", 1 );
			SetModel( "models/enemies/chicken.vmdl" );
			Movespeed = 2f;
		}


		public override Vector3 GetMovementPosition( Vector3 previousPosition, Vector3 nextPosition, float percentage )
		{
			float height = (float) (Math.Sin( Time.Now * 20f ) * 2.5f);
			var position = base.GetMovementPosition( previousPosition, nextPosition, percentage );
			return position + (Vector3.Up * height);
		}
	}
}
