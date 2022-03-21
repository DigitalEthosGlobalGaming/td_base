
using Sandbox;
using System.Collections.Generic;

namespace Degg.GridSystem
{

    public partial class GridSpace: ModelEntity
    {
		[Net]
        public Vector2 GridPosition { get; set; }

		[Net]
		public GridMap Map { get; set; }

		[Net]
		public List<GridItem> Items { get; set; } = new List<GridItem>();



		public Vector3 GetWorldPosition()
		{
			return this.Map.GetWorldSpace( (int)this.GridPosition.x, (int)this.GridPosition.y );
		}

		public virtual float GetMovementWeight(GridSpace a, NavPoint n)
		{
			if (a == null)
			{
				return -1;
			}
			return 10;
		}



		public virtual void ClientTick( float delta, float currentTick )
		{
			DebugOverlay.Sphere( GetWorldPosition(), 10f, Color.Red );
		}
		public virtual void ServerTick( float delta, float currentTick )
		{
			this.Scale = Map.TileScale;
		}


		public virtual void OnAddToMap()
		{
			this.Transmit = TransmitType.Always;
			UpdatePosition();
		}

		public void UpdatePosition()
		{
			Scale = Map.TileScale;
			Position = GetWorldPosition();
		}

		public T GetNeighbour<T>( int x, int y ) where T : GridSpace
		{
			var positionToGet = new Vector2(x,y) + GridPosition;
			if (Map == null)
			{
				return null;
			}
			var tile = Map.GetSpace( positionToGet );
			if (tile is T)
			{
				return (T)tile;
			}

			return null;
		}
		// Grabs immediate neighbours.
		// Note:
		// Up, Right, Down, Left in a clock-wise pattern to grab the neighbours.
		// If a neighbour does not exist, we will place the element as null;
		//	do check if the element in the array is null when using this in a for-loop.
		public T[] GetNeighbours<T>() where T : GridSpace
		{
			var up = GetNeighbour<T>( 0, -1 );
			var down = GetNeighbour<T>( 0, 1 );
			var left = GetNeighbour<T>( -1, 0 );
			var right = GetNeighbour<T>( 1, 0 );
			T[] neighbours = { up, right, down, left  };
			return neighbours;
		}
		public GridSpace GetNeighbour(Vector2 pos)
		{
			var positionToGet = pos + GridPosition;
			return Map.GetSpace( positionToGet );
		}

		public void AddItem(GridItem item, bool triggerEvents = true)
        {
            item.Space = this;
            Items.Add(item);
            if (triggerEvents)
            {
                this.OnItemAdded(item);
                item.OnAdded();
            }
        }

        public void RemoveItem(GridItem item, bool triggerEvents = true)
        {
            item.Space = null;
            Items.Remove(item);
            if (triggerEvents)
            {
                OnItemRemoved(item);
                item.OnRemove();
            }
        }

        public virtual void OnItemAdded(GridItem item) { }
        public virtual void OnItemRemoved(GridItem item) { }

        public override string ToString()
        {
            return $"SPACE [{GridPosition.x},{GridPosition.y}]";
        }
    }
}
