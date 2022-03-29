using Degg.TDBase.Tools;
using Degg.Util;

namespace CandyDefence.Tools
{
	public partial class PlayerTool: TowerPlacerTool
	{
		public override void CreateHudElements()
		{
			base.CreateHudElements();
			SetHudPanel<TowerPlacerUi>();
		}
	}
}
