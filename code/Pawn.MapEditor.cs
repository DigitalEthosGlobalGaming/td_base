using Degg.GridSystem;
using Degg.GridSystem.GridSpaces;


namespace Sandbox
{
	public partial class Pawn
	{
		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public void MapEditorSimulate( Client cl )
		{
			if ( !IsBuildingMap )
			{
				return;
			}

			if ( IsServer )
			{
				if ( CurrentHoveredTile != null )
				{
					if ( Input.Pressed( InputButton.Attack1 ) )
					{
						var position = CurrentHoveredTile.GridPosition;
						if ( CurrentHoveredTile is TDGridSpace gridSpace )
						{
							gridSpace.Type = gridSpace.Type + 1;
							if ( gridSpace.Type > 2 )
							{
								gridSpace.Type = 0;
							}
							gridSpace.UpdateModel();
						}
						else
						{
							Map.AddTile<TDGridSpace>( (int)position.x, (int)position.y );
						}
					}
					else if ( Input.Pressed( InputButton.Attack2 ) )
					{
						var position = CurrentHoveredTile.GridPosition;
						Map.AddTile<GridSpace>( (int)position.x, (int)position.y );
					}
				}
			}
		}
	}
}
