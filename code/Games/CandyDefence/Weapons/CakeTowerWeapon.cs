
using CandyDefence.Bullets;
using Degg.TDBase;

namespace CandyDefence.Weapons
{
	public partial class CakeTowerWeapon : WeaponBase
	{
		public CakeTowerWeapon()
		{
			AttackInterval = 1000f;
			Range = 250f;
			Damage = 1f;
		}



		public override void Fire()
		{
			base.Fire();
			
			var target = GetTarget();
			if (target != null)
			{
				var bullet = CreateBullet<CakeBullet>( target.Position );
				bullet.Position = (target.Position + Vector3.Up * 200f);
			}
		}
	}
}
