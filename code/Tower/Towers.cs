using Degg.GridSystem;
namespace Degg.TDBase.Towers
{
	public partial class TowerBase : GridItem
	{
		public override void OnSetup()
		{
			base.OnSetup();
			Scale = 5f;
			Offset = new Vector3( 0, 0, 10f );
		}
	}
}
