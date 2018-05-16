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

        public static void LoadStaffs()
        {
            Staffs = CollectionManager.GetStaff();
        }

        private static Staff _selectedStaff;

        public static Staff SelectedStaff
        {
            get
            {
                return _selectedStaff;
            }

            set
            {
                _selectedStaff = value;
            }
        }
    }
}
