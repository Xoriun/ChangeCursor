using LiveSplit.UI.Components;
using System;
using LiveSplit.Model;
using LiveSplit.UI;
using System.Xml;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace LiveSplit.ChangeCursor
{
	public class ChangeCursorComponent : LogicComponent
	{
		private LiveSplitState m_state = null;

		private void OnStart(object sender, EventArgs e)
		{
			Console.WriteLine("start");
		}

		public void OnReset(object sender, TimerPhase value)
		{
			Console.WriteLine("reset");
		}

		#region LogicComponent Implementation
		public override string ComponentName => ChangeCursorFactory.s_componentName;

		private void LaodCursors()
		{
			const int SPI_SETCURSORS = 0x0057;
			const int SPIF_UPDATEINIFILE = 0x01;
			const int SPIF_SENDCHANGE = 0x02;
			SystemParametersInfo(SPI_SETCURSORS, 0, 0, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
		}

		[DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

		private static object standart_appStarting;
		private static object standart_arrow;
		private static object standart_wait;

		private const string factorio_cursor = "%SystemRoot%\\cursors\\customCursor.cur";


		public ChangeCursorComponent(LiveSplitState state)
		{
			m_state = state;
			m_state.OnStart += OnStart;
			m_state.OnReset += OnReset;

			standart_appStarting = (string) Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "AppStarting", "%SystemRoot%\\cursors\\aero_working.ani");
			standart_arrow = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "Arrow", "%SystemRoot%\\cursors\\aero_arrow.cur");
			standart_wait = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "Wait", "%SystemRoot%\\cursors\\aero_busy.ani");

			Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "AppStarting", factorio_cursor);
			Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "Arrow", factorio_cursor);
			Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "Wait", factorio_cursor);
			LaodCursors();
		}

		public override void Dispose()
		{
			m_state.OnStart -= OnStart;
			m_state.OnReset -= OnReset;
			Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "AppStarting", standart_appStarting);
			Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "Arrow", standart_arrow);
			Registry.SetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors\", "Wait", standart_wait);
			LaodCursors();
		}

		public override XmlNode GetSettings(XmlDocument document)
		{
			return document.CreateElement("Settings");
		}

		public override System.Windows.Forms.Control GetSettingsControl(LayoutMode mode)
		{
			return null;
		}

		public override void SetSettings(XmlNode settings)
		{

		}

		public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
		{

		}

		#endregion
	}
}
