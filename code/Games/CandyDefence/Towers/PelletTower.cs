using Degg.TDBase.Weapons;
using Sandbox;

namespace Degg.TDBase.Towers
{
	public partial class PelletTower: TowerBase
	{		
		public override void OnSetup()
		{
			base.OnSetup();
			SetModel( "models/towers/tower.vmdl" );
			SetupPhysicsFromModel(Sandbox.PhysicsMotionType.Static);
			SetMaterialGroup( Rand.Int(1,6 ));
			SetBodyGroup( "bottom", Rand.Int( 1, 3 ) );
			SetBodyGroup( "middle", Rand.Int( 1, 3 ) );
			SetBodyGroup( "top", Rand.Int( 1, 3 ) );
			Equip<PelletWeapon>();
		}

	}
}
