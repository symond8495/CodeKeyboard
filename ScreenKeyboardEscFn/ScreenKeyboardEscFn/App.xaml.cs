using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace ScreenKeyboardEscFn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private NotifyIconWrapper wrapperIconNotify;

		private const int SW_SHOWNORMAL = 1;
		
		[DllImport("user32.dll")]
		private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

		/// <summary>
		/// System.Windows.Application.Startup イベント を発生させます。
		/// </summary>
		/// <param name="e">イベントデータ を格納している StartupEventArgs</param>
		protected override void OnStartup(StartupEventArgs e)
		{
            Process currentProcess = Process.GetCurrentProcess();
			var nameProcess = currentProcess.ProcessName;
			var regex = new Regex(@"^(\w+)\.vshost$");
			if (regex.IsMatch(nameProcess)) {
				var match = regex.Match(nameProcess);
				nameProcess = match.Groups[1].Captures[0].Value;
			}
			var processes = Process.GetProcesses();
			var runningProcess = (from process in Process.GetProcesses()
								  where
									process.Id != currentProcess.Id &&
									process.ProcessName.Equals(
									  nameProcess,
									  StringComparison.Ordinal)
								  select process).FirstOrDefault();
			if (runningProcess != null) {
				ShowWindow(runningProcess.MainWindowHandle, SW_SHOWNORMAL);
				Application.Current.Shutdown();
				return;
			}

			base.OnStartup(e);
			this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			this.wrapperIconNotify = new NotifyIconWrapper();
		}

		/// <summary>
		/// System.Windows.Application.Exit イベント を発生させます。
		/// </summary>
		/// <param name="e">イベントデータ を格納している ExitEventArgs</param>
		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			if (this.wrapperIconNotify != null) {
				this.wrapperIconNotify.Dispose();
			}
		}
	}
}
