using Degg.TDBase;
namespace CandyDefence.Bullets
{
	public partial class CakeBullet: BulletBase
	{
		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/food/cake.vmdl" );
			MovementSpeed = 4f;
		}

		public override Rotation GetRotation( Vector3 newPosition )
		{
			return Rotation;
		}

		public override void Explode()
		{
			base.Explode();

			var targets = GetTargetsInRange( 250f );
			foreach(var enemy in targets)
			{
				if (enemy.IsValid)
				{
					enemy.TakeDamage( Weapon, Weapon.Damage );
				}
			}
		}
	}
}
