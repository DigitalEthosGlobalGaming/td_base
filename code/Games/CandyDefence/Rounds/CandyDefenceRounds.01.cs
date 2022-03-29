using CandyDefence.Enemies;
using Degg.TDBase;

namespace CandyDefence.Rounds
{
	public partial class CandyDefenceRounds
	{
		public static RoundBase Round1()
		{
			var round = new RoundBase();
			round.SpawnInterval = 1000;
			for ( int i = 0; i < 10; i++ )
			{
				round.AddToQueue<EnemyChicken>();
			}

			return round;
		}
	}

}
