using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Enemies
{
	[Library]
	public partial class EnemyDemon : EnemyBase
	{
		public override string EnemyName => "Demon";
		public override float BaseHealth => 8f;
		public override int MinCash => 1;
		public override int MaxCash => 1;

		public override void Setup()
		{
			base.Setup();
			Rewards.Add( "Candies", 2 );
		}
	}
}
