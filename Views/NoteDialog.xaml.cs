using System.Windows;
using MahApps.Metro.Controls;

namespace ERSApp.Views
{
    public partial class NoteDialog : MetroWindow
    {
        public NoteDialog()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
