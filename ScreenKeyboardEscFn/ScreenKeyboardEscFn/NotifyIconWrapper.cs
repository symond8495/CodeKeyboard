using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace ScreenKeyboardEscFn
{
	public partial class NotifyIconWrapper : Component
	{
		public NotifyIconWrapper()
		{
			InitializeComponent();
		}

		public NotifyIconWrapper(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		private void toolStripMenuItemExit_Click(object sender, EventArgs e)
		{
			App.Current.Shutdown();
		}

		private void toolStripMenuItemView_Click(object sender, EventArgs e)
		{
			var windowMain = ScreenKeyboardEscFn.App.Current.MainWindow;
			var isVisible = windowMain.IsVisible;
			if (isVisible) {
				windowMain.Hide();
			} else {
				windowMain.Show();
			}
		}

		private void contextMenuStrip_Opened(object sender, EventArgs e)
		{
			var windowMain = ScreenKeyboardEscFn.App.Current.MainWindow;
			var isVisible = windowMain.IsVisible;
            this.toolStripMenuItemView.Checked = isVisible;
		}
    }
}
