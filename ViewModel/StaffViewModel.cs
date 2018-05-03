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
            foreach (Staff s in Staffs)
            {
                s.Status = CollectionManager.GetStatus(s.Id, CollectionManager.SelectedDate);
            }
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
