using Degg.GridSystem;
using Degg.GridSystem.GridSpaces;
using Degg.Util;
using Sandbox;
using Sandbox.Degg;
using System;
using System.Collections.Generic;
using System.Linq;
using TDBase;

namespace Sandbox
{
	public partial class Pawn : AnimEntity
	{
		[Net]
		public PlayerMap Map { get; set; }

		[Net]
		public bool IsBuildingMap { get; set; }

		[Net]
		public GridSpace CurrentHoveredTile { get; set; }
		/// <summary>
		/// Called when the entity is first created 
		/// </summary>
		public override void Spawn()
		{
			base.Spawn();

			//
			// Use a watermelon model
			//
			SetModel( "models/sbox_props/watermelon/watermelon.vmdl" );

			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			
			if (IsServer)
			{
				MyGame.AddActivePawn( this );
			}

			Log.Info( "Spawn!" );


			SetupMap();
		}

		protected override void OnDestroy()
		{
			if ( IsServer )
			{
				MyGame.RemoveActivePawn( this );
				if ( Map != null )
				{
					Map.Delete();
				}
			}
		}

		public Vector3 GetMapPosition()
		{
			int index = MyGame.ActivePawns.IndexOf( this );
			var cols = 4;
			float y = index % cols;
			float x = (float) Math.Floor( (decimal)(index / cols) );
			return new Vector3( x * Map.GridSize.x  * (Map.XSize + 2), y * Map.GridSize.y * (Map.YSize + 2), 250f );
		}


		public void SetupMap()
		{
			if (Map != null)
			{
				Map.Delete();
				Map = null;
			}

			if ( Map == null )
			{
				var playerMap = new PlayerMap();
				playerMap.Owner = this;
				Map = playerMap;
				playerMap.OnSetupAction = () =>
				{
					Map.LoadFromString( BaseMaps.Map1 );
				};
				Map.Init( 20, 20 );

				playerMap.Position = GetMapPosition();

				var size = playerMap.GetMapSize() / 1.75f;
				Position = playerMap.Position + (Vector3.Up * 500f) + (Vector3.Forward * size.x);
			}
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );


			// Update rotation every frame, to keep things smooth
			Rotation = Rotation.From( 45, 180, 0 ); // Input.Rotation;
			EyeRotation = Rotation;

			// build movement from the input values
			var movement = new Vector3( -Input.Forward, -Input.Left, 0 ).Normal;

			// rotate it to the direction we're facing
			Velocity =  movement;

			// apply some speed to it
			Velocity *= Input.Down( InputButton.Run ) ? 1000 : 500;

			// apply it to our position using MoveHelper, which handles collision
			// detection and sliding across surfaces for us
			MoveHelper helper = new MoveHelper( Position, Velocity );
			helper.Trace = helper.Trace.Size( 16 );
			if ( helper.TryMove( Time.Delta ) > 0 )
			{
				Position = helper.Position;
			}


			var endPos = EyePosition + (EyeRotation.Forward * 4000);
			var mytrace = Trace.Ray( EyePosition, endPos );
			mytrace.WithTag("GridSpace");
			var tr = mytrace.Run();

			if ( IsServer )
			{
				if ( tr.Entity != null && tr.Entity is GridSpace )
				{
					GridSpace ent = (GridSpace)tr.Entity;
					CurrentHoveredTile = ent;
				} else
				{
					CurrentHoveredTile = null;
				}
			}

			MapEditorSimulate( cl );
			TurretPlacerSimulate( cl );

			if ( CurrentHoveredTile  != null)
			{
				DebugOverlay.Sphere( CurrentHoveredTile.Position, 100f, Color.Red );
			}
		}

		/// <summary>
		/// Called every frame on the client
		/// </summary>
		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );
		}
	}
}
