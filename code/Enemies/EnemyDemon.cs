

using Sandbox;

namespace TDBase.Enemies
{
	[Library]
	public partial class EnemyDemon : EnemyBase
	{
		public override string EnemyName => "Demon";
		public override float BaseHealth => 50f;
		public override int MinCash => 1;
		public override int MaxCash => 1;

		public override void Spawn()
		{
			base.Spawn();
			
		}
	}
}
