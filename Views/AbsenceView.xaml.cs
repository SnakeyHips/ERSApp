using System;
using System.Windows;
using System.Windows.Controls;
using ERSApp.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ERSApp.Views
{
    public partial class AbsenceView : UserControl
    {
        MetroWindow mainWindow = (Application.Current.MainWindow as MetroWindow);

        public AbsenceView()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new AbsenceViewModel();
            }
        }

        private void btnAddAbsence_Click(object sender, RoutedEventArgs e)
        {
            AddAbsenceWindow addAbsenceWindow = new AddAbsenceWindow();
            addAbsenceWindow.Owner = mainWindow;
            addAbsenceWindow.ShowDialog();
        }

        private void btnUpdateAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (lstAbsences.SelectedIndex > -1)
            {
                UpdateAbsenceWindow updateAbsenceWindow = new UpdateAbsenceWindow(AbsenceViewModel.SelectedAbsence);
                updateAbsenceWindow.Owner = mainWindow;
                updateAbsenceWindow.ShowDialog();
            }
        }

        private async void btnDelAbsence_Click(object sender, RoutedEventArgs e)
        {
            if (lstAbsences.SelectedIndex > -1)
            {
                MessageDialogResult choice = await mainWindow.ShowMessageAsync("",
                            "Are you sure you want to delete this Absence?",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (choice == MessageDialogResult.Affirmative)
                {
                    StaffViewModel.UpdateAbsence(AbsenceViewModel.SelectedAbsence.StaffId, -AbsenceViewModel.SelectedAbsence.Length,
                    StaffViewModel.GetWeek(DateTime.Parse(AbsenceViewModel.SelectedAbsence.StartDate)));
                    AbsenceViewModel.DeleteAbsence(AbsenceViewModel.SelectedAbsence);
                    AbsenceViewModel.Absences.Remove(AbsenceViewModel.SelectedAbsence);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Close();
        }
    }
}
