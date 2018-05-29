using ERSApp.Model;
using System.Collections.ObjectModel;

namespace ERSApp.ViewModel
{
    public class StaffViewModel
    {
        public StaffViewModel()
        {
            LoadStaffs();
        }

        public static ObservableCollection<Staff> Staffs { get; set; }
        public static Staff SelectedStaff { get; set; }

        public static void LoadStaffs()
        {
            Staffs = CollectionManager.GetStaff();
        }
    }
}
