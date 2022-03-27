using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Enemies
{
	[Library]
	public partial class EnemyDemonLarge : EnemyBase
	{
		public override string EnemyName => "Demon Large";
		public override float BaseHealth => 10f;
		public override int MinCash => 1;
		public override int MaxCash => 1;

		public override void Spawn()
		{
			base.Spawn();
			Movespeed = 1f;
		}
	}
}
