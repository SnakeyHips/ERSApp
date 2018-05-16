﻿using ERSApp.Model;
using System.Collections.ObjectModel;

namespace ERSApp.ViewModel
{
    public class AbsenceViewModel
    {
        public AbsenceViewModel()
        {
            LoadAbsences();
        }

        public static ObservableCollection<Absence> Absences { get; set; }

        public static void LoadAbsences()
        {
            Absences = CollectionManager.GetAbsences();
            foreach (Staff s in StaffViewModel.Staffs)
            {
                s.Status = CollectionManager.GetStatus(s.Id, CollectionManager.SelectedDate);
            }
        }

        private static Absence _selectedAbsence;

        public static Absence SelectedAbsence
        {
            get
            {
                return _selectedAbsence;
            }

            set
            {
                _selectedAbsence = value;
            }
        }
    }
}
