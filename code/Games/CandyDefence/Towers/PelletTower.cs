using Degg.TDBase.Weapons;
using Sandbox;

namespace Degg.TDBase.Towers
{
	public partial class PelletTower: TowerBase
	{

		public override float Cost { get; set; } = 15f;
		public override void OnSetup()
		{
			base.OnSetup();
			SetModel( "models/towers/tower.vmdl" );
			SetupPhysicsFromModel(Sandbox.PhysicsMotionType.Static);
			SetMaterialGroup( (int)TowerColors.LightBlue);
			SetBodyGroup( "bottom", 0 );
			SetBodyGroup( "middle", 1 );
			SetBodyGroup( "top", 2 );
			Equip<PelletWeapon>();
		}

	}
}
