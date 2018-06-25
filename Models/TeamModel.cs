using System.ComponentModel;

namespace ERSApp.Models
{
    public class TeamModel
    {
    }
    public class Team : INotifyPropertyChanged
    {
        private string name;
        private int sv1Id;
        private string sv1Name;
        private int dri1Id;
        private string dri1Name;
        private int dri2Id;
        private string dri2Name;
        private int rn1Id;
        private string rn1Name;
        private int rn2Id;
        private string rn2Name;
        private int rn3Id;
        private string rn3Name;
        private int cca1Id;
        private string cca1Name;
        private int cca2Id;
        private string cca2Name;
        private int cca3Id;
        private string cca3Name;

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

        public int SV1Id
        {
            get
            {
                return sv1Id;
            }
            set
            {
                if (sv1Id != value)
                {
                    sv1Id = value;
                    RaisePropertyChanged("SV1Id");
                }
            }
        }

        public string SV1Name
        {
            get
            {
                return sv1Name;
            }
            set
            {
                if (sv1Name != value)
                {
                    sv1Name = value;
                    RaisePropertyChanged("SV1Name");
                }
            }
        }

        public int DRI1Id
        {
            get
            {
                return dri1Id;
            }
            set
            {
                if (dri1Id != value)
                {
                    dri1Id = value;
                    RaisePropertyChanged("DRI1Id");
                }
            }
        }

        public string DRI1Name
        {
            get
            {
                return dri1Name;
            }
            set
            {
                if (dri1Name != value)
                {
                    dri1Name = value;
                    RaisePropertyChanged("DRI1Name");
                }
            }
        }

        public int DRI2Id
        {
            get
            {
                return dri2Id;
            }
            set
            {
                if (dri2Id != value)
                {
                    dri2Id = value;
                    RaisePropertyChanged("DRI2Id");
                }
            }
        }

        public string DRI2Name
        {
            get
            {
                return dri2Name;
            }
            set
            {
                if (dri2Name != value)
                {
                    dri2Name = value;
                    RaisePropertyChanged("DRI2Name");
                }
            }
        }

        public int RN1Id
        {
            get
            {
                return rn1Id;
            }
            set
            {
                if (rn1Id != value)
                {
                    rn1Id = value;
                    RaisePropertyChanged("RN1Id");
                }
            }
        }

        public string RN1Name
        {
            get
            {
                return rn1Name;
            }
            set
            {
                if (rn1Name != value)
                {
                    rn1Name = value;
                    RaisePropertyChanged("RN1Name");
                }
            }
        }

        public int RN2Id
        {
            get
            {
                return rn2Id;
            }
            set
            {
                if (rn2Id != value)
                {
                    rn2Id = value;
                    RaisePropertyChanged("RN2Id");
                }
            }
        }

        public string RN2Name
        {
            get
            {
                return rn2Name;
            }
            set
            {
                if (rn2Name != value)
                {
                    rn2Name = value;
                    RaisePropertyChanged("RN2Name");
                }
            }
        }

        public int RN3Id
        {
            get
            {
                return rn3Id;
            }
            set
            {
                if (rn3Id != value)
                {
                    rn3Id = value;
                    RaisePropertyChanged("RN3Id");
                }
            }
        }

        public string RN3Name
        {
            get
            {
                return rn3Name;
            }
            set
            {
                if (rn3Name != value)
                {
                    rn3Name = value;
                    RaisePropertyChanged("RN3Name");
                }
            }
        }

        public int CCA1Id
        {
            get
            {
                return cca1Id;
            }
            set
            {
                if (cca1Id != value)
                {
                    cca1Id = value;
                    RaisePropertyChanged("CCA1Id");
                }
            }
        }

        public string CCA1Name
        {
            get
            {
                return cca1Name;
            }
            set
            {
                if (cca1Name != value)
                {
                    cca1Name = value;
                    RaisePropertyChanged("CCA1Name");
                }
            }
        }

        public int CCA2Id
        {
            get
            {
                return cca2Id;
            }
            set
            {
                if (cca2Id != value)
                {
                    cca2Id = value;
                    RaisePropertyChanged("CCA2Id");
                }
            }
        }

        public string CCA2Name
        {
            get
            {
                return cca2Name;
            }
            set
            {
                if (cca2Name != value)
                {
                    cca2Name = value;
                    RaisePropertyChanged("CCA2Name");
                }
            }
        }

        public int CCA3Id
        {
            get
            {
                return cca3Id;
            }
            set
            {
                if (cca3Id != value)
                {
                    cca3Id = value;
                    RaisePropertyChanged("CCA3Id");
                }
            }
        }

        public string CCA3Name
        {
            get
            {
                return cca3Name;
            }
            set
            {
                if (cca3Name != value)
                {
                    cca3Name = value;
                    RaisePropertyChanged("CCA3Name");
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
