using Sandbox;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class CurrencyManager : Entity
	{
		[Net]
		public Dictionary<string, float> Currencies { get; set; }

		public override void Spawn()
		{
			base.Spawn();
			Transmit = TransmitType.Always;
			Currencies = new Dictionary<string, float>();
		}

		public void SetMoney(string name, float amount)
		{
			if (IsClient)
			{
				return;
			}

			if ( Currencies == null )
			{
				Currencies = new Dictionary<string, float>();
			}

			Currencies[name] = amount;			
		}
		public void AddMoney(string name, float amount)
		{
			SetMoney(name, GetMoney(name) + amount);
		}
		public float SubtractMoney(string name, float amount)
		{
			SetMoney( name, GetMoney( name ) - amount);
			return GetMoney( name );
		}

		public float GetMoney(string name)
		{
			if (Currencies == null)
			{
				return 0;
			}

			if (Currencies.TryGetValue(name, out var amount))
			{
				return amount;
			}
			return 0;
		}

		public bool CanAfford(string name, float amount)
		{
			return GetMoney( name ) >= amount;
		}

	}
}
