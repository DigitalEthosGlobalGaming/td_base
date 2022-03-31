using Sandbox.UI;
using Sandbox;
using Degg.Util;

namespace CandyDefence.Tools
{
	public partial class TowerHudItem : Panel
	{
		public TowerStoreItem Tower { get; protected set; }
		public Panel Container { get; set; }
		public Label Name { get; set; }
		public Label Price { get; set; }
		public Label Description { get; set; }
		public bool Selected { get; set; } = true;

		public TowerHudItem()
		{
			SetTemplate( "/Games/CandyDefence/Ui/TowerHudItem.html" );
			StyleSheet.Load( "/Games/CandyDefence/Ui/TowerHudItem.scss" );
			Selected = false;
		}

		public void SetTower( TowerStoreItem t )
		{
			if ( t != null )
			{
				Tower = t;
				if ( Name != null )
				{
					Name.Text = Tower.Name;
				}
				if ( Price != null )
				{
					Price.Text = "$" + Tower.Price.ToString();
				}
			} else
			{
				AdvLog.Warning( "Trying to set tower of null" );
			}
		}

		public override void Tick()
		{
			if ( Container != null )
			{
				Container.SetClass( "selected", Selected );
			}
		}
	}
}
