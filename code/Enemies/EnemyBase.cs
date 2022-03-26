using Degg.GridSystem;
using Degg.TDBase.Weapons;
using Sandbox;


namespace TDBase.Enemies
{
	[Library]
	public partial class EnemyBase : ModelEntity
	{
		public PlayerMap Map { get; set; }
		public int CurrentPositionIndex { get; set; }
		public bool IsSetup { get; set; }

		public float Percentage { get; set; }
		public float Movespeed { get; set; }
		public virtual string EnemyName { get; set; }
		public virtual float EnemyHealth {get;set;}
		public virtual float BaseHealth {get;set;}
		public virtual int MinCash => 12;
		public virtual int MaxCash => 20;
		public GridSpace PreviousSpace { get; set; }
		public GridSpace NextSpace { get; set; }
		public override void Spawn()
		{
			base.Spawn();
			Movespeed = 2f;
		}

		public void Setup()
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
		public void Tick()
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



			Percentage = Percentage + (Movespeed * Time.Delta);

			if (Percentage > 1f)
			{
				MoveToNextSpot();
			}



			if (PreviousSpace != null && NextSpace != null)
			{
				var worldPosition = PreviousSpace.GetWorldPosition();
				var nextSpace = NextSpace.GetWorldPosition();

				Position = worldPosition.LerpTo( nextSpace, Percentage );
				Rotation = Rotation.LookAt( nextSpace - Position, Vector3.Up );
			}
		}

		public void TriggerDeath( WeaponBase weapon )
		{
			Delete();
		}

		public void TakeDamage(WeaponBase weapon, float amount)
		{
			EnemyHealth = EnemyHealth - amount;

			if (EnemyHealth <= 0)
			{
				TriggerDeath( weapon );
			}
		}
	}
}
