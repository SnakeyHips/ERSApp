using System;
using System.Windows;
using ERSApp.Model;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;

namespace ERSApp.Views
{
    public partial class FindByNameDialog : MetroWindow
    {
        public FindByNameDialog(string name)
        {
            InitializeComponent();
            foreach(Staff s in StaffViewModel.Staffs)
            {
                if (s.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    lstNames.Items.Add(s);
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(lstNames.SelectedItem != null)
            {
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
