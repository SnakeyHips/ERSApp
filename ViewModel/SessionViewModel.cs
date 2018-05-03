using ERSApp.Model;
using System.Collections.ObjectModel;

namespace ERSApp.ViewModel
{
    public class SessionViewModel
    {

        public SessionViewModel()
        {
            LoadSessions();
        }

        public static ObservableCollection<Session> Sessions { get; set; }

        public static void LoadSessions()
        {
            Sessions = CollectionManager.GetSessions(CollectionManager.SelectedDate.ToShortDateString());
        }

        private static Session _selectedSession;

        public static Session SelectedSession
        {
            get
            {
                return _selectedSession;
            }

            set
            {
                _selectedSession = value;
            }
        }
    }
}
