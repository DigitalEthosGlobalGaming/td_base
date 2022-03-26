using Degg.GridSystem;
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
			Scale = 0.5f;
			IsSetup = true;

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
			if ( !IsSetup || Map == null )
			{
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
	}
}
