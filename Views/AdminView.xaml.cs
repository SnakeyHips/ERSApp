using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;

namespace ERSApp.Views
{
    public partial class AdminView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public AdminView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new AdminViewModel();
            }
        }
    }
}
