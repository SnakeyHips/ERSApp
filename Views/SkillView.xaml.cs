using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class SkillView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public SkillView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new AdminViewModel();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSkillWindow addSkillWindow = new AddSkillWindow();
            addSkillWindow.Owner = mainWindow;
            addSkillWindow.ShowDialog();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AdminViewModel.SelectedSkill != null)
            {
                MessageDialogResult choice = await mainWindow.ShowMessageAsync("",
                        "Are you sure you want to delete this Skill?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    AdminViewModel.DeleteSkill(AdminViewModel.SelectedSkill);
                    AdminViewModel.Skills.Remove(AdminViewModel.SelectedSkill);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
