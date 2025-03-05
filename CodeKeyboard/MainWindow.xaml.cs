using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;



namespace CodeKeyboard
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

        [DllImport("user32.dll")]
        static extern uint keybd_event(int bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);


        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;
        private Dictionary<string, string> keyDict = new Dictionary<string, string>();


        public MainWindow()
        {
            InitializeComponent();

            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingChanged);
            this.keyDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(@"keycode.json"));
        }

        private void SystemEvents_DisplaySettingChanged(object sender, EventArgs e)
        {
            int widthScreen = (int)SystemParameters.PrimaryScreenWidth;
            int heightScreen = (int)SystemParameters.PrimaryScreenHeight;
            int heightTaskBar = heightScreen - (int)SystemParameters.WorkArea.Height;

            int numButtons = this.LayoutRoot.Children.OfType<UIElement>().Count(elem => elem is StackPanel);
            this.Width = widthScreen;
            this.Height = widthScreen / numButtons + SystemParameters.WindowCaptionHeight;

            this.Top = heightScreen - this.Height - heightTaskBar;
            this.Left = 0;
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            string keyName = (sender as Button).Name.ToString();
            Debug.WriteLine(keyName);

            keybd_event(Convert.ToInt32(this.keyDict[keyName], 16), 0, KEYEVENTF_EXTENDEDKEY, 0);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            var helper = new WindowInteropHelper(this);
            SetWindowLong(helper.Handle, GWL_EXSTYLE, GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        private void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            SystemEvents_DisplaySettingChanged(null, null);
        }

        private void WindowMain_Closing(object sender, EventArgs e)
        {
            this.Hide();
            var argsEvent = (CancelEventArgs)e;
            argsEvent.Cancel = true;
        }
    }
}