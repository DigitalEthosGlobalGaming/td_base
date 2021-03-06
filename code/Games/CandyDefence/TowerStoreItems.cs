using Degg.TDBase;
using Sandbox;
using System.Collections.Generic;

namespace CandyDefence
{
	public partial class TowerStoreItem: StoreItemBase
	{
		public static List<TowerStoreItem> GetStoreItems()
		{
			var list = new List<TowerStoreItem>();

			list.Add(new TowerStoreItem()
			{
				Name = "Pellet Tower",
				ClassName = "PelletTower",
				Currency="Candies",
				Price=10,
				Description= "Basic tower that shoots candy at enemies."
			} ) ;

			list.Add( new TowerStoreItem()
			{
				Name = "Icecream Tower",
				ClassName = "IcecreamTower",
				Currency = "Candies",
				Price = 25,
				Description = "Shoots a strong icecream at enemies doing damage."
			} );

			list.Add( new TowerStoreItem()
			{
				Name = "LollyPop Tower",
				ClassName = "LollyPopTower",
				Currency = "Candies",
				Price = 50,
				Description = "Long range and accurate tower perfect for getting those hard to reach places."
			} );

			list.Add( new TowerStoreItem()
			{
				Name = "Milkshake Tower",
				ClassName = "MilkshakeTower",
				Currency = "Candies",
				Price = 75,
				Description = "Launches and spills a milkshake on the ground on the enemy. Slowing and damaging enemies that walk over it."
			} );

			list.Add( new TowerStoreItem()
			{
				Name = "Cake Tower",
				ClassName = "CakeTower",
				Currency = "Candies",
				Price = 100,
				Description = "Large cake falls from the sky damaging the enemy and stunning them."
			} );

			return list;
		}

		[Net]
		public string ClassName { get; set; }

	}
}
