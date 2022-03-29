
using Degg.GridSystem;
using Sandbox;

namespace Degg.TDBase.Tools
{
	public partial class ToolBase : Entity
	{

		[Net, Predicted]
		public TimeSince TimeSincePrimaryAttack { get; set; }

		[Net, Predicted]
		public TimeSince TimeSinceSecondaryAttack { get; set; }

		public virtual float PrimaryRate => 10.0f;
		public virtual float SecondaryRate => 10.0f;

		public override void Spawn()
		{
			base.Spawn();
			Transmit = TransmitType.Owner;
		}

		public virtual void OnTileHovered( GridSpace space)
		{

		}

		public virtual void OnTileHoveredOff( GridSpace space )
		{

		}

		public T GetHoveredTile<T>() where T: GridSpace
		{
			var t = GetHoveredTile();
			if (t is T)
			{
				return (T)t;
			}
			return null;
		}

		public GridSpace GetHoveredTile()
		{
			if (Owner != null && Owner.IsValid && Owner is Pawn)
			{
				return ((Pawn)Owner).CurrentHoveredTile;
			}
			return null;
		}

		public override void Simulate( Client player )
		{

			if ( !Owner.IsValid() )
			{
				return;
			}

			if ( CanPrimaryAttack() )
			{
				using ( LagCompensation() )
				{
					TimeSincePrimaryAttack = 0;
					AttackPrimary();
				}
			}

			if ( CanSecondaryAttack() )
			{
				using ( LagCompensation() )
				{
					TimeSinceSecondaryAttack = 0;
					AttackSecondary();
				}
			}
		}

		public override void FrameSimulate( Client player )
		{
		}

		public virtual bool CanPrimaryAttack()
		{
			if ( !Owner.IsValid() || !Input.Down( InputButton.Attack1 ) ) return false;

			var rate = PrimaryRate;
			if ( rate <= 0 ) return true;

			return TimeSincePrimaryAttack > (1 / rate);
		}

		public virtual void AttackPrimary()
		{

		}

		public virtual bool CanSecondaryAttack()
		{
			if ( !Owner.IsValid() || !Input.Down( InputButton.Attack2 ) ) return false;

			var rate = SecondaryRate;
			if ( rate <= 0 ) return true;

			return TimeSinceSecondaryAttack > (1 / rate);
		}

		public virtual void AttackSecondary()
		{

		}
	}
}
