using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Interop;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Threading;

using System.Xml.Linq;

using System.ComponentModel;


namespace ScreenKeyboardEscFn
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		private const int GWL_EXSTYLE = -20;

		private const int WS_EX_NOACTIVATE = 0x08000000;

		private const int KEYEVENTF_EXTENDEDKEY = 0x1;
		private const int KEYEVENTF_KEYUP = 0x2;
		private const int VK_ESCAPE = 27;
		private const int VK_F1 = 0x70;
		private const int VK_F2 = 0x71;
		private const int VK_F3 = 0x72;
		private const int VK_F4 = 0x73;
		private const int VK_F5 = 0x74;
		private const int VK_F6 = 0x75;
		private const int VK_F7 = 0x76;
		private const int VK_F8 = 0x77;
		private const int VK_F9 = 0x78;
		private const int VK_F10 = 0x79;
		private const int VK_F11 = 0x7a;
		private const int VK_F12 = 0x7b;
		private const int VK_CANCEL = 0x03;
		private const int VK_PAUSE = 0x13;
		private const int VK_SNAPSHOT = 0x2c;
		private const int VK_SCROLL = 0x91;
		private const int VK_INSERT = 0x2d;
		private const int VK_DELETE = 0x2e;

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			var helper = new WindowInteropHelper(this);
			SetWindowLong(helper.Handle, GWL_EXSTYLE, GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
		}

		//Keybd_eventを読み込み-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		[DllImport("user32.dll")]
		static extern uint keybd_event(short bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		public MainWindow()
		{
			InitializeComponent();

			SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingChanged);
		}

		private void WindowMain_Loaded(object sender, RoutedEventArgs e)
		{
			SystemEvents_DisplaySettingChanged(null, null);
		}

		private void SystemEvents_DisplaySettingChanged(object sender, EventArgs e)
		{
			int widthScreen = (int)SystemParameters.PrimaryScreenWidth;
			int heightScreen = (int)SystemParameters.PrimaryScreenHeight;
			int heightTaskBar = heightScreen - (int)SystemParameters.WorkArea.Height;

			int numButtons = this.LayoutRoot.Children.OfType<UIElement>().Count(elem => elem is Button);
			this.Width = widthScreen;
			this.Height = widthScreen / numButtons + SystemParameters.WindowCaptionHeight;
			
			this.Top = heightScreen - this.Height - heightTaskBar;
			this.Left = 0;
			
		}

		private void buttonEsc_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_ESCAPE, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF1_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F1, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF2_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F2, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF3_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F3, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF4_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F4, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF5_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F5, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF6_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F6, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF7_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F7, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF8_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F8, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF9_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F9, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF10_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F10, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF11_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F11, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonF12_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_F12, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonPrtSc_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_SNAPSHOT, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonScroll_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_SCROLL, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonPause_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_PAUSE, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonIns_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_INSERT, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void buttonDel_Click(object sender, RoutedEventArgs e)
		{
			keybd_event(VK_DELETE, 0, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
		}

		private void WindowMain_Closing(object sender, EventArgs e)
		{
			this.Hide();
			var argsEvent = (CancelEventArgs)e;
			argsEvent.Cancel = true;
		}
	}
}
