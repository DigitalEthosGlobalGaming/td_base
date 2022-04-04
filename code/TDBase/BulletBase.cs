
using Sandbox;
using System.Collections.Generic;

namespace Degg.TDBase
{
	public partial class BulletBase: ModelEntity
	{
		public Vector3 TargetPosition { get; set; }
		public Vector3 StartPosition { get; set; }
		public EnemyBase TargetEntity { get; set; }
		public WeaponBase Weapon { get; set; }
		public float MovementSpeed { get; set; }
		public float Percentage { get; set; }
		public bool IsExploded { get; set; }

		public override void Spawn()
		{
			base.Spawn();
			MovementSpeed = 2.5f;
			if (IsServer)
			{
				Fire();
			}
		}
		public virtual void Fire()
		{
			
		}
		public virtual void Explode()
		{
			IsExploded = true;
			Delete();
		}

		public Pawn GetPawn()
		{
			return Weapon?.GetPawn();
		}

		public virtual Vector3 GetTargetPosition()
		{
			var target = TargetPosition;
			if (TargetEntity != null)
			{
				if ( TargetEntity.IsValid() )
				{
					target = TargetEntity.WorldSpaceBounds.Center;
					TargetPosition = target;
				}
			}

			return target;
		}

		public virtual Rotation GetRotation(Vector3 newPosition)
		{
			return Rotation.LookAt( newPosition, Vector3.Up );
		}

		public List<EnemyBase> GetEnemies()
		{
			var currentRound = Weapon?.Tower?.GetPlayerMap()?.CurrentRound;
			return currentRound?.EnemyEntities ?? new List<EnemyBase>();
		}

		public List<EnemyBase> GetTargetsInRange(Vector3 position, float radius )
		{
			return GetEnemies().FindAll( ( enemy ) => {
				if ( enemy.IsValid )
				{
					var distance = enemy.Position.Distance( position );
					return distance <= radius;
				}

				return false;
			} );
		}

		public List<EnemyBase> GetTargetsInRange(float size )
		{
			return GetTargetsInRange( Position, size );
		}

		[Event.Tick.Server]
		public void Tick()
		{
			if ( !IsExploded )
			{
				var deltaSpeed = (MovementSpeed * 100f * Time.Delta);
				Percentage = Percentage + deltaSpeed;

				var target = GetTargetPosition();
				var newPositions = target - Position;

				Rotation = GetRotation( newPositions );
				var moveRotation = Rotation.LookAt( newPositions, Vector3.Up );

				var dirVector = moveRotation.Forward * deltaSpeed;
				Position = Position + dirVector;

				var distance = Position.Distance( target );

				if ( distance <= deltaSpeed)
				{
					Explode();
				}
			}
		}

	}
}
