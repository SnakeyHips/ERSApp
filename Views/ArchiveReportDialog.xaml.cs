using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class ArchiveReportDialog : MetroWindow
    {
        public ArchiveReportDialog()
        {
            InitializeComponent();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (lstReportWeeks.SelectedItems != null)
            {
                DialogResult = true;
            }
            else
            {
                await this.ShowMessageAsync("", "Please select weeks to create report.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
