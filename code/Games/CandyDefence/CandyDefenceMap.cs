
using CandyDefence.Rounds;
using CandyDefence.Tools;
using Degg.TDBase;
using Degg.Util;

namespace CandyDefence
{
	public partial class CandyDefenceMap: TDPlayerMap
	{

		public override void Init()
		{
			Init( 20, 20 );
			AddRounds(CandyDefenceRounds.Rounds());
			OwnerPawn?.SetTool<PlayerTool>();
		}
	}

}
