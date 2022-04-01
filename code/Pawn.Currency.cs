using Degg.GridSystem;
using Degg.TDBase;

namespace Sandbox
{
	public partial class Pawn
	{
		[Net]
		public CurrencyManager Currencies { get; set; }
	}
}
