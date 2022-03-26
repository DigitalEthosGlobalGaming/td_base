using Degg.GridSystem;
using Degg.GridSystem.GridSpaces;
using Degg.TDBase.Towers;

namespace Sandbox
{
	public partial class Pawn
	{
		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public void TurretPlacerSimulate( Client cl )
		{
			if ( IsBuildingMap )
			{
				return;
			}

			if ( IsServer )
			{
				if ( CurrentHoveredTile != null )
				{
					if ( Input.Pressed( InputButton.Attack1 ) )
					{
						PlaceTower();
					}
				}
			}
		}

		public void PlaceTower()
		{
			if ( !(CurrentHoveredTile is TDGridSpace) )
			{
				var towers = CurrentHoveredTile.GetItems<TowerBase>();
				if ( towers.Count > 0 )
				{
					CurrentHoveredTile.RemoveItems( towers );
				}

				var newTower = new PelletTower();
				CurrentHoveredTile.AddItem( newTower );
			}
		}


	}
}
