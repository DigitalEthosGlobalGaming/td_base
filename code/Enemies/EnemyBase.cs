using Degg.GridSystem;
using Sandbox;


namespace TDBase.Enemies
{
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
			Movespeed = 10f;
		}

		public void Setup()
		{
			if ( IsSetup )
			{
				return;
			}

			SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );
			Scale = 5f;
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
			if (Map != null)
			{
				Setup();
			}


			Percentage = Percentage + (Movespeed * Time.Delta);
			if (Percentage > 1f)
			{
				MoveToNextSpot();
			}
			if (PreviousSpace != null && NextSpace != null)
			{
				Position = PreviousSpace.GetWorldPosition().LerpTo( NextSpace.GetWorldPosition(), Percentage );
			}


		}
	}

}
