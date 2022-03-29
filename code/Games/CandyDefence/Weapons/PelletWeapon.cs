
using Degg.TDBase.Towers;

namespace Degg.TDBase.Weapons
{
	public partial class PelletWeapon: WeaponBase
	{
		public PelletWeapon()
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
				Tower.TargetEntity = target;
				target.TakeDamage( this, Damage );
			}
			// On fire make particles of some sort of lollies.
		}
	}
}
