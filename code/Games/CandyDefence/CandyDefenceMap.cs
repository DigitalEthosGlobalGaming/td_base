
using CandyDefence.Rounds;

namespace Degg.TDBase
{
	public partial class CandyDefenceMap: TDPlayerMap
	{

		public void Init()
		{
			Init( 20, 20 );
		}
		public override void Init( int xAmount, int yAmount )
		{
			base.Init( xAmount, yAmount );
			AddRounds( CandyDefenceRounds.Rounds() );
		}

	}

}
