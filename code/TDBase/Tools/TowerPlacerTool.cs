using Degg.GridSystem;
using Degg.Util;
using Sandbox;

namespace Degg.TDBase.Tools
{
	public partial class TowerPlacerTool : ToolBase
	{

		public const float GhostFloatHeight = 50f;

		public string CurrentTower { get; set; } = "SniperTower";

		public TowerBase Ghost {get;set;}

		public void SpawnGhost()
		{
			if (IsServer)
			{
				return;
			}
			DestroyGhost();
			var tile = GetHoveredTile();

			if ( tile == null) {
				return;
			}

			Ghost = Library.Create<TowerBase>( CurrentTower );

			Ghost.IsGhost = true;
			Ghost.Space = tile;
			Ghost.Setup();
			Ghost.Position = Ghost.Space.Position + (Vector3.Up * GhostFloatHeight);
			Ghost.TargetPosition = Ghost.Position;

			if ( CanPlaceOnTile( tile ) )
			{
				Ghost.RenderColor = Color.White.WithAlpha( 0.5f );
			} else
			{
				Ghost.RenderColor = Color.Red.WithAlpha( 0.5f );
			}
		}

		public void DestroyGhost()
		{
			if ( Ghost != null && Ghost.IsValid )
			{
				Ghost.Delete();
			}
		}


		protected override void OnDestroy()
		{
			base.OnDestroy();
			DestroyGhost();
		}

		public override void OnTileHovered(GridSpace tile)
		{
			SpawnGhost();
		}

		public bool CanPlaceOnTile()
		{
			return CanPlaceOnTile( GetHoveredTile() );
		}

		public bool CanPlaceOnTile( GridSpace g )
		{
			var tile = GetHoveredTile();
			if ( tile != null && !(tile is TDGridSpace) )
			{
				if ( tile.GetItems<TowerBase>().Count > 0 )
				{
					return false;
				}
				return true;
			}
			return false;
		}

		public void RemoveTower()
		{
			if ( IsClient )
			{
				return;
			}
			
			var tile = GetHoveredTile();
			if ( tile != null && !(tile is TDGridSpace) )
			{
				var towers = tile.GetItems<TowerBase>();
				if ( towers.Count > 0 )
				{
					tile.RemoveItems( towers );
				}

			}
		}

		public void PlaceTower()
		{
			if ( IsClient )
			{
				return;
			}
			var tile = GetHoveredTile();
			if ( CanPlaceOnTile(tile)) { 
				RemoveTower();

				var tower = Library.Create<TowerBase>( CurrentTower );
				tile.AddItem( tower );
			}
		}

		public override void AttackSecondary()
		{
			RemoveTower();
			DestroyGhost();
		}

		public override void AttackPrimary()
		{
			PlaceTower();
			DestroyGhost();
		}
	}
}
