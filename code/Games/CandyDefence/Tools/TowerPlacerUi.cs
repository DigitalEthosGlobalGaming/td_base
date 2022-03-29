using Sandbox.UI;
using Sandbox;

namespace CandyDefence.Tools
{
	public partial class TowerPlacerUi : Panel
	{
		public Panel Base { get; set; }

		public TowerPlacerUi()
		{
			SetTemplate( "Ui/TowerPlacerUi.html" );
			StyleSheet.Load( "/Ui/TowerPlacerUi.scss" );
		}

		public override void Tick()
		{
			var player = Local.Pawn as Pawn;
		}
	}
}
