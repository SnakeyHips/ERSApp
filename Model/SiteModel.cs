using System.ComponentModel;

namespace ERSApp.Model
{
    public class SiteModel
    {
    }

    public class Site : INotifyPropertyChanged
    {
        private string name;
        private string type;
        private string times;

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

        public string Times
        {
            get
            {
                return times;
            }
            set
            {
                if (times != value)
                {
                    times = value;
                    RaisePropertyChanged("Times");
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
