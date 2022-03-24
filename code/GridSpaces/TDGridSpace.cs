
namespace Degg.GridSystem.GridSpaces
{
	public partial class TDGridSpace: RoadGridSpace
	{
		public int Type { get; set; }

		public bool IsSpawner()
		{
			return Type == 1;
		}
		public bool IsEnd()
		{
			return Type == 2;
		}

		public override void UpdateModel()
		{
			base.UpdateModel();
			if ( Type == 1 )
			{
				RenderColor = Color.Red;
			} else if ( Type == 2) {
				RenderColor = Color.Green;
			} else
			{
				RenderColor = Color.White;
			}
		}
	}
}
