using Sandbox;

namespace Degg.TDBase
{
	public partial class TDBotBase : Bot
	{

		public Pawn Pawn { get; set; }

		public override void BuildInput( InputBuilder builder )
		{
			// Here we can choose / modify the bot's input each tick.
			// We'll make them constantly attack by holding down the Attack1 button.
			// builder.SetButton( InputButton.Attack1, true );
		}

		public override void Tick()
		{
			// Here we can do something with the bot each tick.
			// Here we'll print our bot's name every tick.
			// Log.Info( Client.Name );
		}
	}
}
