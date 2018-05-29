using System.ComponentModel;

namespace ERSApp.Model
{
    public class AbsenceModel
    {
    }

    public class Absence : INotifyPropertyChanged
    {
        private int staffId;
        private string staffName;
        private string type;
        private string startDate;
        private string endDate;
        private double length;

        public int StaffId
        {
            get
            {
                return staffId;
            }
            set
            {
                if (staffId != value)
                {
                    staffId = value;
                    RaisePropertyChanged("StaffId");
                }
            }
        }

        public string StaffName
        {
            get
            {
                return staffName;
            }
            set
            {
                if (staffName != value)
                {
                    staffName = value;
                    RaisePropertyChanged("StaffName");
                }
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (type != value)
                {
                    type = value;
                    RaisePropertyChanged("Type");
                }
            }
        }

        public string StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    RaisePropertyChanged("StartDate");
                }
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    RaisePropertyChanged("EndDate");
                }
            }
        }

        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                if (length != value)
                {
                    length = value;
                    RaisePropertyChanged("Length");
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
