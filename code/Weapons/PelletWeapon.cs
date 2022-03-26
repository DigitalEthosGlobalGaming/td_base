
using Degg.TDBase.Towers;

namespace Degg.TDBase.Weapons
{
	public partial class PelletWeapon: WeaponBase
	{
		public PelletWeapon()
		{
			AttackInterval = 1000f;
			Range = 2500f;
			Damage = 1f;
		}

		public override void Fire()
		{
			base.Fire();
			var target = GetTarget();
			if (target != null)
			{
				Tower.TargetEntity = target;
			}
		}
	}
}
