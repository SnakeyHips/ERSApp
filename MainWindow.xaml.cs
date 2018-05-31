using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro;
using MahApps.Metro.Controls;
using ERSApp.Model;
using ERSApp.ViewModel;

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
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Crimson"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    dateCalender.Opacity = 1;
                    break;
                case 1:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Mauve"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    dateCalender.Opacity = 1;
                    break;
                case 2:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Olive"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    dateCalender.Opacity = 1;
                    break;
                case 3:
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Steel"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
                    dateCalender.Opacity = 0;
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
                s.Status = AbsenceViewModel.GetStatus(s.Id);
            }
        }
    }
}
