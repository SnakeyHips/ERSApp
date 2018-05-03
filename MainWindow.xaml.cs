using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro;
using MahApps.Metro.Controls;

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
            if (tabSessions.IsSelected)
            {
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Crimson"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
            }
            else if (tabStaff.IsSelected)
            {
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Mauve"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
            }
            else if (tabAbsence.IsSelected)
            {
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.Accents.First(x => x.Name == "Olive"),
                        ThemeManager.AppThemes.First(x => x.Name == "BaseLight"));
            }
        }
    }
}
