using Degg.Util;
using Sandbox;
using Sandbox.UI;


namespace Degg.TDBase.Tools
{
	public partial class ToolUi : HudEntity<RootPanel>
	{
		protected Panel HudPanel { get; set; }

		public ToolUi()
		{
			var children = RootPanel.Children;
			RootPanel.AddChild<ChatBox>();
			RootPanel.StyleSheet.Load( "/Degg/Ui/Styles/base.scss" );
		}

		public virtual T SetHudPanel<T>() where T : Panel, new()
		{
			HudPanel?.Delete( true );
			HudPanel = RootPanel.AddChild<T>();
			return (T)HudPanel;
		}
	}
}
