
using Degg.TDBase.Bullets;

namespace Degg.TDBase.Weapons
{
	public partial class CannonWeapon : WeaponBase
	{
		public CannonWeapon()
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
				CreateBullet<CannonBullet>( target );
				Tower.TargetPosition = target.Position;
			}
		}
	}
}
