using Degg.TDBase;
using System.Collections.Generic;

namespace CandyDefence.Rounds
{
	public partial class CandyDefenceRounds
	{
		public static List<RoundBase> Rounds()
		{
			var list = new List<RoundBase>();

			list.Add( Round1() );
			list.Add( Round2() );
			list.Add( Round3() );
			list.Add( Round4() );
			list.Add( Round5() );
			list.Add( Round6() );
			list.Add( Round7() );
			list.Add( Round8() );
			list.Add( Round9() );
			list.Add( Round10() );

			return list;
		}
	}

}
