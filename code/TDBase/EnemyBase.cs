using Degg.GridSystem;
using Degg.Util;
using Sandbox;
using System.Collections.Generic;

namespace Degg.TDBase
{
	[Library]
	public partial class EnemyBase : AnimEntity
	{
		public TDPlayerMap Map { get; set; }
		public RoundBase Round { get; set; }
		public int CurrentPositionIndex { get; set; }
		public bool IsSetup { get; set; }

		public Dictionary<string, float> Rewards { get; set; }

		public Rotation TargetRotation { get; set; }

		public float Percentage { get; set; }
		public float Movespeed { get; set; }
		public virtual string EnemyName { get; set; }
		public bool IsAlive { get; set; }
		public virtual float EnemyHealth {get;set;}
		public virtual float BaseHealth {get;set;}
		public virtual int MinCash => 12;
		public virtual int MaxCash => 20;
		public GridSpace PreviousSpace { get; set; }
		public GridSpace NextSpace { get; set; }
		public override void Spawn()
		{
			base.Spawn();
			Rewards = new Dictionary<string, float>();
			Movespeed = 2f;
			IsAlive = true;
		}

		public virtual void Setup()
		{
			if ( IsSetup )
			{
				return;
			}

			SetModel( "models/enemies/demon.vmdl" );
			Position = Position.WithZ( Position.z + 10f );
			Scale = 0.25f;
			IsSetup = true;
			EnemyHealth = BaseHealth;

		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Map?.EnemyEntities?.Remove( this );
		}
		
		public void MoveToNextSpot()
		{
			Percentage = 0;
			CurrentPositionIndex = CurrentPositionIndex + 1;
			var path = Map?.EnemyPath;
			if ( path != null )
			{
				if ( CurrentPositionIndex >= path.Count )
				{
					Delete();
				}
				else
				{
					PreviousSpace = NextSpace;
					var nextSpot = path[CurrentPositionIndex];
					NextSpace = nextSpot;
				}
			}
		}

		[Event.Tick]
		public virtual void Tick()
		{
			if ( !IsSetup)
			{
				return;
			}

			if (Map == null)
			{
				Delete();
				return;
			}

			if ( EnemyHealth > 0 )
			{
				Percentage = Percentage + (Movespeed * Time.Delta);

				if ( Percentage > 1f )
				{
					MoveToNextSpot();
				}

				if ( PreviousSpace != null && NextSpace != null )
				{
					var worldPosition = PreviousSpace.GetTopWorldPosition();
					var nextSpace = NextSpace.GetTopWorldPosition();

					Position = GetMovementPosition( worldPosition, nextSpace, Percentage );
					Rotation = GetMovementRotation( worldPosition, nextSpace, Percentage );
				}
			} else
			{
				if ( IsValid )
				{
					Percentage = Percentage + (Movespeed * Time.Delta);
					Rotation = Rotation.Lerp( Rotation, TargetRotation, Percentage );
				}
			}
		}

		public virtual Vector3 GetMovementPosition(Vector3 previousPosition, Vector3 nextPosition, float percentage)
		{
			return previousPosition.LerpTo( nextPosition, Percentage );
		}
		public virtual Rotation GetMovementRotation( Vector3 previousPosition, Vector3 nextPosition, float percentage )
		{			
			return Rotation.LookAt( nextPosition - previousPosition, Vector3.Up );
		}

		public void TriggerDeath( WeaponBase weapon, BulletBase bullet )
		{
			if ( !IsValid )
			{
				return;
			} 
			if ( !IsAlive )
			{
				return;
			}

			OnDeath( weapon, bullet );
			Percentage = 0f;
			IsAlive = false;
			TargetRotation = Rotation.LookAt( Vector3.Random, Vector3.Down );
			DeleteAsync( Rand.Float( 0.5f, 1.5f ) );
			AdvLog.Info( weapon, bullet );
			GiveRewards( weapon?.GetPawn() ?? bullet?.GetPawn() );
		}

		public virtual void GiveRewards(Pawn p)
		{
			if (p == null)
			{
				AdvLog.Warning( "Trying to give rewards to null Pawn" );
				return;
			}

			var currencies = p.Currencies;
			if (currencies != null)
			{
				foreach ( var item in Rewards )
				{
					currencies.AddMoney( item.Key, item.Value );
				}
			}
		}

		public virtual void OnDeath(WeaponBase weapon, BulletBase bullet)
		{

		}


		public void TakeDamage( BulletBase bullet, float amount )
		{
			EnemyHealth = EnemyHealth - amount;

			if ( EnemyHealth <= 0 )
			{
				TriggerDeath( bullet.Weapon, bullet );
			}
		}

		public void TakeDamage(WeaponBase weapon, float amount)
		{
			EnemyHealth = EnemyHealth - amount;

			if (EnemyHealth <= 0)
			{
				TriggerDeath( weapon, null );
			}
		}
	}
}
