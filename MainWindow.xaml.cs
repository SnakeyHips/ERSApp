using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro;
using MahApps.Metro.Controls;
using ERSApp.Models;
using ERSApp.ViewModels;

namespace ERSApp
{
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        //TabControl selection handler for saving which tab was last selected and app accent
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tabsMain.SelectedIndex)
            {
                case 0:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Crimson"), ThemeManager.GetAppTheme("BaseLight"));
                    dateCalender.Visibility = Visibility.Visible;
                    break;
                case 1:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Mauve"), ThemeManager.GetAppTheme("BaseLight"));
                    dateCalender.Visibility = Visibility.Visible;
                    break;
                case 2:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Steel"), ThemeManager.GetAppTheme("BaseLight"));
                    dateCalender.Visibility = Visibility.Visible;
                    break;
                case 3:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Olive"), ThemeManager.GetAppTheme("BaseLight"));
                    dateCalender.Visibility = Visibility.Visible;
                    break;
                case 4:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Sienna"), ThemeManager.GetAppTheme("BaseLight"));
                    dateCalender.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        //Calender selection change stuff here
        private void dateCalender_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            Mouse.Capture(null);
            SelectedDate.Date = dateCalender.SelectedDate.Value.Date;
            SessionViewModel.LoadSessions();
            foreach (Staff s in StaffViewModel.Staffs)
            {
                s.Status = AbsenceViewModel.GetStatus(s.Id, SelectedDate.Date);
            }
        }
    }
}
