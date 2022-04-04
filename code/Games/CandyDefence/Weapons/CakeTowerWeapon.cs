
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
			Damage = 10f;
		}



		public override void Fire()
		{
			base.Fire();
			
			var target = GetTarget();
			if (target != null)
			{
				Tower.TargetEntity = target;
				var bullet = CreateBullet<CakeBullet>( target.Position );
				bullet.MovementSpeed = 10f;
				bullet.Position = (target.Position + Vector3.Up * 500f);
			}
		}
	}
}
