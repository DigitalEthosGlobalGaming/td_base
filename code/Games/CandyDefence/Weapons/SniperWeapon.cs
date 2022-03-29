
using CandyDefence.Bullets;
using Degg.TDBase;

namespace CandyDefence.Weapons
{
	public partial class SniperWeapon : WeaponBase
	{
		public SniperWeapon()
		{
			AttackInterval = 5000f;
			Range = 1000f;
			Damage = 1f;
		}

		public override void Fire()
		{
			base.Fire();
			var target = GetTarget();
			if ( target != null )
			{
				Tower.TargetEntity = target;
				target.TakeDamage( this, Damage );
			}
		}
	}
}
