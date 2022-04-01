using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Enemies
{
	[Library]
	public partial class EnemyDemonLarge : EnemyBase
	{
		public override string EnemyName => "Demon Large";
		public override float BaseHealth => 10f;
		public override int MinCash => 2;
		public override int MaxCash => 2;

		public override void Setup()
		{
			base.Setup();
			Movespeed = 1f;
			Rewards.Add( "Candies", 10 );
			Scale = 0.5f;
		}

		public override void Tick()
		{
			base.Tick();
		}
	}
}
