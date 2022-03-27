using Sandbox;
using TDBase.Enemies;

namespace Degg.TDBase.Bullets
{
	public partial class CannonBullet: BulletBase
	{
		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/bullets/cannon.vmdl" );
			MovementSpeed = 4f;
		}

		public override void Explode()
		{
			base.Explode();
			if (TargetEntity != null)
			{
				TargetEntity.TakeDamage( Weapon, Weapon.Damage );
			}
		}
	}
}
