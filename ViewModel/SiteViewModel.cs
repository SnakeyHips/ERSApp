using System.Collections.ObjectModel;
using ERSApp.Model;

namespace ERSApp.ViewModel
{
    public class SiteViewModel
    {
        public SiteViewModel()
        {
            LoadLocations();
        }

        public static ObservableCollection<Site> CommunityLocations { get; set; }
        public static ObservableCollection<Site> MDCLocations { get; set; }

        public static void LoadLocations()
        {
            CommunityLocations = CollectionManager.GetSites("Community");
            MDCLocations = CollectionManager.GetSites("MDC");
        }

        private static string _selectedLocation;

        public static string SelectedLocation
        {
            get
            {
                return _selectedLocation;
            }

            set
            {
                _selectedLocation = value;
            }
        }
    }
}
