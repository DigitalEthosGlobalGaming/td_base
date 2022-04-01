using Degg.TDBase;
using Degg.TDBase.Tools;
using Degg.Util;
using Sandbox;

namespace CandyDefence.Tools
{
	public partial class PlayerTool : TowerPlacerTool
	{

		public override void CreateHudElements()
		{
			base.CreateHudElements();
			if (IsServer)
			{
				return;
			}
			var pawn = Owner as Pawn;
			var map = (pawn?.Map as CandyDefenceMap);
			var store = map?.Store;

			var element = SetHudPanel<TowerPlacerUi>();

			if ( store != null && store.IsValid() )
			{
				element.SetStoreItems( store?.GetItems<TowerStoreItem>() );
			}

			element.Tool = this;
			element.SelectTower( 4 );
		}

		public override void PlaceTower()
		{
			if (IsClient)
			{
				return;
			}

			var towerStoreItem = GetStoreItemForTower( CurrentTower );

			var pawn = OwnerPawn();
			var map = pawn?.GetMap<CandyDefenceMap>();

			var tile = GetHoveredTile();
			if ( CanPlaceOnTile( tile ) )
			{

				if ( towerStoreItem?.Buy( pawn ) ?? false )
				{
					var store = map.Store;
					if ( store != null )
					{
						base.PlaceTower();
						return;
					}
				}
			}

			pawn.OwnerPlaySound( "ui.navigate.deny" );

		}
	}
}
