
using Degg.Utils;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class WeaponBase
	{
		public float Range { get; set; }
		public float Damage { get; set; }
		public float AttackInterval { get; set; }
		public TowerBase Tower { get; set; }
		public Timer AttackTimer { get; set; }
		public Vector3 Position { get; set; }
		public virtual void Equipped(TowerBase tower)
		{
			Tower = tower;

			if (AttackTimer != null)
			{
				AttackTimer.Delete();
				AttackTimer = null;
			}

			AttackTimer = new Timer( Fire, AttackInterval );
			AttackTimer.Start();
		}

		public T CreateBullet<T>(EnemyBase target ) where T : BulletBase, new()
		{
			var b = new T();
			b.TargetEntity = target;
			return (T) CreateBullet( b );
		}
		public T CreateBullet<T>(Vector3 target) where T: BulletBase, new()
		{
			var b = new T();
			b.TargetPosition = target;
			return (T)CreateBullet( b );
		}
		public BulletBase CreateBullet(BulletBase b)
		{
			b.StartPosition = Position;
			b.Position = Position;
			b.Weapon = this;
			return b;
		}
		public void UnEquipped()
		{
			if ( AttackTimer != null )
			{
				AttackTimer.Delete();
			}
			Tower = null;
		}
		private void Fire(Timer t)
		{
			Fire();
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
