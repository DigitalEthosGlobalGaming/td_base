using Degg.GridSystem;
using Degg.TDBase.Weapons;
using Sandbox;
using System;
using System.Collections.Generic;
using TDBase;
using TDBase.Enemies;

namespace Degg.TDBase.Towers
{
	public partial class TowerBase : GridItem
	{
		public WeaponBase Weapon { get; set; }

		public Vector3 TargetPosition { get; set; } = new Vector3( 0, 0, 0 );
		public Entity TargetEntity { get; set; }

		public PlayerMap GetPlayerMap()
		{
			if ( Space?.Map is PlayerMap )
			{
				return (PlayerMap) Space?.Map;
			}
			return null;
		}
		public PriorityQueue<EnemyBase, float> GetEnemiesInRange(float range)
		{
			var enemies = new PriorityQueue<EnemyBase, float>();
			var allEnemies = GetPlayerMap().EnemyEntities;

			foreach ( var enemy in allEnemies )
			{
				var x1 = enemy.Position.x;
				var y1 = enemy.Position.y;
				var x2 = Position.x;
				var y2 = Position.y;

				var y = x2 - x1;
				var x = y2 - y1;

				var distance = Math.Sqrt( x * x + y * y );
				if (distance <= range )
				{
					enemies.Enqueue( enemy, (float) distance );
				}
			}

			return enemies;
		}

		public void LookAt(Vector3 position)
		{
			Rotation = Rotation.LookAt( position - Position, Vector3.Up );
		}
		public override void OnSetup()
		{
			base.OnSetup();
			Scale = 5f;
			Offset = new Vector3( 0, 0, 10f );
		}

		public void Equip<T>() where T: WeaponBase, new()
		{
			Equip( new T());
		}

		public void Equip(WeaponBase w)
		{
			Weapon = w;
			Weapon.Equipped(this);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Weapon?.UnEquipped();
		}

		[Event.Tick.Server]
		public void Tick()
		{
			var target = TargetPosition.WithZ(Position.z);

			if (TargetEntity != null && TargetEntity.IsValid) 
			{
				target = TargetEntity.Position.WithZ( Position.z );
			}

			if ( target.x != 0 && target.y != 0 )
			{
				LookAt( target );
			}
		}


	}
}
