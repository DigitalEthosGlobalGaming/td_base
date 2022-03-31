using Sandbox.UI;
using Sandbox;
using System.Collections.Generic;
using Degg.TDBase.Tools;
using Degg.TDBase;
using Degg.Util;

namespace CandyDefence.Tools
{
	public partial class TowerPlacerUi : Panel
	{
		public TowerPlacerTool Tool { get; set; }
		public Panel Container { get; set; }
		public List<TowerStoreItem> StoreItems { get; set; } = new List<TowerStoreItem>();

		public float LastTick { get; set; }
		public int ItemIndex { get; set; }

		public TowerPlacerUi()
		{
			SetTemplate( "/Games/CandyDefence/Ui/TowerPlacerUi.html" );
			StyleSheet.Load( "/Games/CandyDefence/Ui/TowerPlacerUi.scss" );

			CreateItemsElements();
		}

		public void SetStoreItems( List<TowerStoreItem> items)
		{
			StoreItems = items;
			DeleteItemsElements();
			CreateItemsElements();
		}

		public void DeleteItemsElements()
		{
			if (Container != null)
			{
				Container.DeleteChildren();
			}
		}
		public void CreateItemsElements()
		{
			if ( Container  == null)
			{
				Container = this.AddChild<Panel>();
			}

			foreach ( var item in StoreItems )
			{
				var child = Container.AddChild<TowerHudItem>();
				child.SetTower(item);
			}
		}


		public void SelectTower( int index )
		{

			var childen = new List<TowerHudItem>(Container.ChildrenOfType<TowerHudItem>());
			if ( StoreItems.Count ==0)
			{
				return;
			}
			if (index >= StoreItems.Count)
			{
				index = 0;
			} else if (index < 0)
			{
				index = StoreItems.Count - 1;
			}
			var previous = childen[ItemIndex];
			ItemIndex = index;
			var next = childen[ItemIndex];
			previous.Selected = false;
			next.Selected = true;

			if ( Tool != null)
			{
				var tower = next?.Tower?.ClassName;
				Log.Info(next);
				if ( tower != null )
				{
					TowerPlacerTool.SetCurrentTowerCmd( tower );
				}
			}
		}

		public void SelectNextTower()
		{
			SelectTower( ItemIndex + 1 );
		}
		public void SelectPreviousTower()
		{
			SelectTower( ItemIndex - 1 );
		}

		public override void Tick()
		{
			if ( Time.Tick != LastTick )
			{
				LastTick = Time.Tick;
				if ( Input.Pressed( InputButton.Jump ) )
				{
					SelectNextTower();
				}
			}
		}
	}
}
