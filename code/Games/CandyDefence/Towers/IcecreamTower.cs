using CandyDefence.Weapons;
using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Towers
{
	[Library]
	public partial class IcecreamTower: TowerBase
	{

		public override float Cost { get; set; } = 20f;
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
