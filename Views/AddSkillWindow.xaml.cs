using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ERSApp.Models;
using ERSApp.ViewModels;

namespace ERSApp.Views
{
    public partial class AddSkillWindow : MetroWindow
    {
        public AddSkillWindow()
        {
            InitializeComponent();
            cboRole.Items.Add("SV");
            cboRole.Items.Add("DRI");
            cboRole.Items.Add("RN");
            cboRole.Items.Add("CCA");
        }

        private async void btnAddSkill_Click(object sender, RoutedEventArgs e)
        {
            if (cboRole.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Role.");
            }
            else if (txtName.Text == "")
            {
                await this.ShowMessageAsync("", "Please enter a Skill Name.");
            }
            else
            {
                Skill temp = new Skill()
                {
                    Role = cboRole.Text,
                    Name = txtName.Text
                };
                AdminViewModel.AddSkill(temp);
                AdminViewModel.Skills.Add(temp);
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
