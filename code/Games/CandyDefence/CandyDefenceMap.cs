
using CandyDefence.Rounds;
using CandyDefence.Tools;
using Degg.TDBase;
using Degg.Util;
using Sandbox;

namespace CandyDefence
{
	public partial class CandyDefenceMap: TDPlayerMap
	{
		[Net, Change]
		public StoreBase Store { get; set; }

		public void OnStoreChanged(StoreBase previous, StoreBase next)
		{
			AdvLog.Info("here", next );
			OwnerPawn?.Tool?.CreateHudElements();
		}
		public override void Init()
		{
			Init( 20, 20 );
			AddRounds(CandyDefenceRounds.Rounds());
			OwnerPawn?.SetTool<PlayerTool>();
			OwnerPawn?.Currencies?.AddMoney( "Candies", 100 );

			Store = new StoreBase();
			var items = TowerStoreItem.GetStoreItems();
			foreach ( var item in items )
			{
				Store.AddStock( item );
			}
		}
	}

}
