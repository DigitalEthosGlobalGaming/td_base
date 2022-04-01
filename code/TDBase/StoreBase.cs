using Sandbox;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class StoreBase: Entity
	{
		[Net]
		public List<StoreItemBase> Items { get; set; }

		public override void Spawn()
		{
			base.Spawn();
			Transmit = TransmitType.Always;
			Items = new List<StoreItemBase>();
		}

		public void Buy(StoreItemBase item, Pawn p)
		{
			Buy( item, p.Currencies );
		}
		public void Buy(StoreItemBase item, CurrencyManager c)
		{

		}


		public List<T> GetItems<T>() where T : StoreItemBase
		{
			var results = new List<T>();
			if (Items == null)
			{
				return new List<T>();
			}
			foreach ( var item in Items )
			{
				results.Add( (T) item );
			}

			return results;

		}

		public StoreItemBase AddStock<T>() where T: StoreItemBase, new()
		{
			return AddStock(new T());
		}
		public StoreItemBase AddStock(StoreItemBase newItem )
		{
			Items.Add( newItem );
			return newItem;
		}
	}
}
