using Degg.TDBase.Towers;
using Degg.Util;
using Degg.Utils;
using Sandbox;
using System.Collections.Generic;
using TDBase.Enemies;

namespace Degg.TDBase.Weapons
{
	public partial class WeaponBase
	{
		public float Range { get; set; }
		public float Damage { get; set; }
		public float AttackInterval { get; set; }
		public TowerBase Tower { get; set; }
		public Timer AttackTimer { get; set; }

		public virtual void Equipped(TowerBase tower)
		{
			Tower = tower;

			if (AttackTimer != null)
			{
				AttackTimer.Delete();
				AttackTimer = null;
			}


			AttackTimer = new Timer( (a,b,c) =>
			{
				Fire();
			}, AttackInterval );
			AttackTimer.Start();
		}
		public void UnEquipped()
		{
			if ( AttackTimer != null )
			{
				AttackTimer.Delete();
			}
			Tower = null;
		}

		public virtual void Fire()
		{

		}

		public EnemyBase GetTarget()
		{
			var enemies = GetEnemiesInRange();
			if (enemies.Count > 0)
			{
				return GetEnemiesInRange()?.Dequeue();
			}
			return null;
		}

		public PriorityQueue<EnemyBase, float> GetEnemiesInRange()
		{
			return Tower.GetEnemiesInRange( Range );

		}

	}
}
