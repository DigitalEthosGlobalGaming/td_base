using CandyDefence.Weapons;
using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Towers
{
	[Library]
	public partial class MilkshakeTower: TowerBase
	{

		public override float Cost { get; set; } = 5f;
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
			// Launches a homing milkshake that hits the enemy doing damage and then leaving a puddle of milk slowing any enemies that walk through.
		}
	}
}
