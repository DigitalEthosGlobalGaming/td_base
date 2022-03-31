using Sandbox;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class StoreItemBase: Entity
	{
		[Net]
		public float Price { get; set; }
		[Net]
		public string Description { get; set; }

		[Net]
		public int Stock { get; set; } = -1;

		public override void Spawn()
		{
			base.Spawn();
			Transmit = TransmitType.Always;
		}

		public Dictionary<string, float> Cost { get; set; }

		public bool CanAfford(Pawn p, string name, float amount )
		{
			return CanAfford( p.Currencies, name, amount );
		}

		public bool CanAfford( CurrencyManager c, string name, float amount )
		{
			if (c != null)
			{
				return c.CanAfford( name, amount );
			}
			return false;
		}
	}
}
