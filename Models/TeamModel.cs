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
    
    public class TeamSite : INotifyPropertyChanged
    {
        private string date,
        private string sv1Name;
        private string sv1Site;
        private string dri1Name;
        private string dri1Site;
        private string dri2Name;
        private string dri2Site;
        private string rn1Name;
        private string rn1Site;
        private string rn2Name;
        private string rn2Site;
        private string rn3Name;
        private string rn3Site;
        private string cca1Name;
        private string cca1Site;
        private string cca2Name;
        private string cca2Site;
        private string cca3Name;
        private string cca3Site;

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged("Date");
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

        public string SV1Site
        {
            get
            {
                return sv1Site;
            }
            set
            {
                if (sv1Site != value)
                {
                    sv1Site = value;
                    RaisePropertyChanged("SV1Site");
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

        public string DRI1Site
        {
            get
            {
                return dri1Site;
            }
            set
            {
                if (dri1Site != value)
                {
                    dri1Site = value;
                    RaisePropertyChanged("DRI1Site");
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

        public string DRI2Site
        {
            get
            {
                return dri2Site;
            }
            set
            {
                if (dri2Site != value)
                {
                    dri2Site = value;
                    RaisePropertyChanged("DRI2Site");
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

        public string RN1Site
        {
            get
            {
                return rn1Site;
            }
            set
            {
                if (rn1Site != value)
                {
                    rn1Site = value;
                    RaisePropertyChanged("RN1Site");
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

        public string RN2Site
        {
            get
            {
                return rn2Site;
            }
            set
            {
                if (rn2Site != value)
                {
                    rn2Site = value;
                    RaisePropertyChanged("RN2Site");
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

        public string RN3Site
        {
            get
            {
                return rn3Site;
            }
            set
            {
                if (rn3Site != value)
                {
                    rn3Site = value;
                    RaisePropertyChanged("RN3Site");
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

        public string CCA1Site
        {
            get
            {
                return cca1Site;
            }
            set
            {
                if (cca1Site != value)
                {
                    cca1Site = value;
                    RaisePropertyChanged("CCA1Site");
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

        public string CCA2Site
        {
            get
            {
                return cca2Site;
            }
            set
            {
                if (cca2Site != value)
                {
                    cca2Site = value;
                    RaisePropertyChanged("CCA2Site");
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

        public string CCA3Site
        {
            get
            {
                return cca3Site;
            }
            set
            {
                if (cca3Site != value)
                {
                    cca3Site = value;
                    RaisePropertyChanged("CCA3Site");
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
