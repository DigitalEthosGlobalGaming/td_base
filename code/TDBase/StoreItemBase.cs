using Sandbox;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class StoreItemBase: Entity
	{
		[Net]
		public float Price { get; set; }
		[Net]
		public string Currency { get; set; }
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

		public bool CanAfford(Pawn p)
		{
			return CanAfford( p, Currency, Price );
		}
		public bool CanAfford(Pawn p, string name, float amount )
		{
			return CanAfford( p.Currencies, name, amount );
		}

		public bool CanAfford( CurrencyManager c )
		{
			return CanAfford( c, Currency, Price );
		}

		public bool CanAfford( CurrencyManager c, string name, float amount )
		{
			if (c != null)
			{
				return c.CanAfford( name, amount );
			}
			return false;
		}
		public virtual bool Buy(CurrencyManager c)
		{
			if ( CanAfford( c ) )
			{
				c.SubtractMoney( Currency, Price );
				return true;
			}

			return false;
		}

		public virtual bool Buy( Pawn p )
		{
			return Buy( p.Currencies );
		}
	}
}
