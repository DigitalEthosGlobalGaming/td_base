using CandyDefence.Enemies;
using Degg.TDBase;
using Sandbox;

namespace CandyDefence.Rounds
{
	public partial class CandyDefenceRounds
	{

		public class TimedRound: RoundBase
		{
			public float EndTime = -1;
			public float Duration = 0f;
			public override bool HasRoundEnded()
			{
				if ( EndTime > 0)
				{
					return EndTime <= Time.Now;
				} else
				{
					EndTime = Time.Now + Duration;
				}
				return false;
			}
		}
		public static RoundBase Round0()
		{
			var round = new TimedRound();
			round.Duration = 2.5f;

			return round;
		}
	}

}
