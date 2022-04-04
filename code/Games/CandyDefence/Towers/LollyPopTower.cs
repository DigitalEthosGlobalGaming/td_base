using CandyDefence.Weapons;
using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Towers
{
	[Library]
	public partial class LollyPopTower: TowerBase
	{

		public override float Cost { get; set; } = 30f;
		public override void OnSetup()
		{
			base.OnSetup();
			SetModel( "models/towers/tower.vmdl" );
			SetupPhysicsFromModel(Sandbox.PhysicsMotionType.Static);
			SetMaterialGroup((int) TowerColors.Yellow);
			SetBodyGroup( "bottom", 0 );
			SetBodyGroup( "middle", 1 );
			SetBodyGroup( "top", 1 );
			Equip<SniperWeapon>();
		}
	}
}
