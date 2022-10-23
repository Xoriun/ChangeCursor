using LiveSplit.ChangeCursor;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveSplit.Model;
using System.ComponentModel;
using IComponent = LiveSplit.UI.Components.IComponent;

[assembly: ComponentFactory(typeof(ChangeCursorFactory))]

namespace LiveSplit.ChangeCursor
{
	class ChangeCursorFactory : IComponentFactory
	{

		public const string s_componentName = "ChangeCursor";
		private const string s_componentDesc = "A small tool to change the System Cursor when starting LiveSplit and setting when closing LiveSplit.";

		private const string s_updateURL = "https://github.com/iNightfaller/SpeedGuidesLive";

		#region IComponentFactory Interface
		public string ComponentName { get { return s_componentName; } }
		public string Description { get { return s_componentDesc; } }
		public ComponentCategory Category { get { return ComponentCategory.Other; } }
		public string UpdateName { get { return "TODO: UpdateName"; } }
		public string XMLURL { get { return "TODO: XmlURL"; } }
		public string UpdateURL { get { return "TODO: s_updateURL"; } }
		public Version Version { get { return Version.Parse("1.0.0"); } }
		public IComponent Create(LiveSplitState state) { return new ChangeCursorComponent(state); }
		#endregion
	}
}
