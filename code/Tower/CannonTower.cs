using Degg.TDBase.Weapons;
using Sandbox;

namespace Degg.TDBase.Towers
{
	public partial class CannonTower: TowerBase
	{		
		public override void OnSetup()
		{
			base.OnSetup();
			SetModel( "models/towers/tower.vmdl" );
			SetupPhysicsFromModel(Sandbox.PhysicsMotionType.Static);
			SetMaterialGroup((int) TowerColors.Red);
			SetBodyGroup( "bottom", 0 );
			SetBodyGroup( "middle", 1 );
			SetBodyGroup( "top", 0 );
			Equip<CannonWeapon>();
		}

	}
}
