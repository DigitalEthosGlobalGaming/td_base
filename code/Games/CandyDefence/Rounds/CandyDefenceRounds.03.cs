using CandyDefence.Enemies;
using Degg.TDBase;

namespace CandyDefence.Rounds
{
	public partial class CandyDefenceRounds
	{
		public static RoundBase Round3()
		{
			var round = new RoundBase();
			round.SpawnInterval = 500;
			for ( int i = 0; i < 20; i++ )
			{
				round.AddToQueue<EnemyDemon>();
			}

			return round;
		}
	}

}
