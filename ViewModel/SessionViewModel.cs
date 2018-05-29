using ERSApp.Model;
using System.Collections.ObjectModel;

namespace ERSApp.ViewModel
{
    public class SessionViewModel
    {
        public SessionViewModel()
        {
            Sessions = new ObservableCollection<Session>();
            LoadSessions();
        }

        public static ObservableCollection<Session> Sessions { get; set; }
        public static Session SelectedSession { get; set; }

        public static void LoadSessions()
        {
            Sessions.Clear();
            foreach(Session s in CollectionManager.GetSessions(CollectionManager.SelectedDate.ToShortDateString()))
            {
                Sessions.Add(s);
            }
        }
    }
}
