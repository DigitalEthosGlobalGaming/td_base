
using Sandbox;

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

		[Event.Tick.Server]
		public void Tick()
		{
			if ( !IsExploded )
			{
				var deltaSpeed = (MovementSpeed * 100f * Time.Delta);
				Percentage = Percentage + deltaSpeed;

				var target = GetTargetPosition();
				Rotation = Rotation.LookAt( target - Position, Vector3.Up );
				var dirVector = Rotation.Forward * deltaSpeed;
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
