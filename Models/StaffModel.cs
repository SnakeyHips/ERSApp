using System.ComponentModel;

namespace ERSApp.Models
{
    public class StaffModel
    {
    }

    public class Staff : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string role;
        private string skill;
        private string address;
        private string number;
        private double contractHours;
        private double appointedHours;
        private double absenceHours;
        private double holidayHours;
        private double unsocialHours;
        private double overtimeHours;
        private string workPattern;
        private string status;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                if (role != value)
                {
                    role = value;
                    RaisePropertyChanged("Role");
                }
            }
        }
        
        public string Skill
        {
            get
            {
                return skill;
            }
            set
            {
                if (skill != value)
                {
                    skill = value;
                    RaisePropertyChanged("Skill");
                }
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                if (address != value)
                {
                    address = value;
                    RaisePropertyChanged("Address");
                }
            }
        }

        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    RaisePropertyChanged("Number");
                }
            }
        }

        public double ContractHours
        {
            get
            {
                return contractHours;
            }
            set
            {
                if (contractHours != value)
                {
                    contractHours = value;
                    RaisePropertyChanged("ContractHours");
                }
            }
        }

        public double AppointedHours
        {
            get
            {
                return appointedHours;
            }
            set
            {
                if (appointedHours != value)
                {
                    appointedHours = value;
                    RaisePropertyChanged("AppointedHours");
                }
            }
        }

        public double AbsenceHours
        {
            get
            {
                return absenceHours;
            }
            set
            {
                if (absenceHours != value)
                {
                    absenceHours = value;
                    RaisePropertyChanged("AbsenceHours");
                }
            }
        }

        public double HolidayHours
        {
            get
            {
                return holidayHours;
            }
            set
            {
                if (holidayHours != value)
                {
                    holidayHours = value;
                    RaisePropertyChanged("HolidayHours");
                }
            }
        }

        public double UnsocialHours
        {
            get
            {
                return unsocialHours;
            }
            set
            {
                if (unsocialHours != value)
                {
                    unsocialHours = value;
                    RaisePropertyChanged("UnsocialHours");
                }
            }
        }
        
        public double OvertimeHours
        {
            get
            {
                return overtimeHours;
            }
            set
            {
                if (overtimeHours != value)
                {
                    overtimeHours = value;
                    RaisePropertyChanged("OvertimeHours");
                }
            }
        }

        public string WorkPattern
        {
            get
            {
                return workPattern;
            }
            set
            {
                if (workPattern != value)
                {
                    workPattern = value;
                    RaisePropertyChanged("WorkPattern");
                }
            }
        }

        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
