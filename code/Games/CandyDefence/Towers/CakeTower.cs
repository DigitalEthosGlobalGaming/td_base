using CandyDefence.Weapons;
using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Towers
{
	[Library]
	public partial class CakeTower: TowerBase
	{
		public override float Cost { get; set; } = 50f;
		public override void OnSetup()
		{
			base.OnSetup();
			SetModel( "models/towers/tower.vmdl" );
			SetupPhysicsFromModel(Sandbox.PhysicsMotionType.Static);
			SetMaterialGroup((int) TowerColors.Orange);
			SetBodyGroup( "bottom", 0 );
			SetBodyGroup( "middle", 0 );
			SetBodyGroup( "top", 0 );
			Equip<CannonWeapon>();
			// This weapon will make a cake fall from the sky and land on the spot doing massive damage.
		}
	}
}
