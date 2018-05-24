using System.ComponentModel;

namespace ERSApp.Model
{
    public class SessionModel
    {
    }

    public class Session : INotifyPropertyChanged
    {
        private string date;
        private string type;
        private string site;
        private string time;
        private double lod;
        private int chairs;
        private int bleeds;
        private int sv1Id;
        private string sv1Name;
        private double sv1LOD;
        private int dri1Id;
        private string dri1Name;
        private double dri1LOD;
        private int dri2Id;
        private string dri2Name;
        private double dri2LOD;
        private int rn1Id;
        private string rn1Name;
        private double rn1LOD;
        private int rn2Id;
        private string rn2Name;
        private double rn2LOD;
        private int rn3Id;
        private string rn3Name;
        private double rn3LOD;
        private int cca1Id;
        private string cca1Name;
        private double cca1LOD;
        private int cca2Id;
        private string cca2Name;
        private double cca2LOD;
        private int cca3Id;
        private string cca3Name;
        private double cca3LOD;
        private int staffCount;
        private int state;


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

        public string Site
        {
            get
            {
                return site;
            }
            set
            {
                if (site != value)
                {
                    site = value;
                    RaisePropertyChanged("Site");
                }
            }
        }

        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                if (time != value)
                {
                    time = value;
                    RaisePropertyChanged("Time");
                }
            }
        }

        public double LOD
        {
            get
            {
                return lod;
            }
            set
            {
                if (lod != value)
                {
                    lod = value;
                    RaisePropertyChanged("LOD");
                }
            }
        }

        public int Chairs
        {
            get
            {
                return chairs;
            }
            set
            {
                if (chairs != value)
                {
                    chairs = value;
                    RaisePropertyChanged("Chairs");
                }
            }
        }

        public int Bleeds
        {
            get
            {
                return bleeds;
            }
            set
            {
                if (bleeds != value)
                {
                    bleeds = value;
                    RaisePropertyChanged("Bleeds");
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

        public double SV1LOD
        {
            get
            {
                return sv1LOD;
            }
            set
            {
                if (sv1LOD != value)
                {
                    sv1LOD = value;
                    RaisePropertyChanged("SV1LOD");
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

        public double DRI1LOD
        {
            get
            {
                return dri1LOD;
            }
            set
            {
                if (dri1LOD != value)
                {
                    dri1LOD = value;
                    RaisePropertyChanged("DRI1LOD");
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

        public double DRI2LOD
        {
            get
            {
                return dri2LOD;
            }
            set
            {
                if (dri2LOD != value)
                {
                    dri2LOD = value;
                    RaisePropertyChanged("DRI2LOD");
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

        public double RN1LOD
        {
            get
            {
                return rn1LOD;
            }
            set
            {
                if (rn1LOD != value)
                {
                    rn1LOD = value;
                    RaisePropertyChanged("RN1LOD");
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

        public double RN2LOD
        {
            get
            {
                return rn2LOD;
            }
            set
            {
                if (rn2LOD != value)
                {
                    rn2LOD = value;
                    RaisePropertyChanged("RN2LOD");
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

        public double RN3LOD
        {
            get
            {
                return rn3LOD;
            }
            set
            {
                if (rn3LOD != value)
                {
                    rn3LOD = value;
                    RaisePropertyChanged("RN3LOD");
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

        public double CCA1LOD
        {
            get
            {
                return cca1LOD;
            }
            set
            {
                if (cca1LOD != value)
                {
                    cca1LOD = value;
                    RaisePropertyChanged("CCA1LOD");
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

        public double CCA2LOD
        {
            get
            {
                return cca2LOD;
            }
            set
            {
                if (cca2LOD != value)
                {
                    cca2LOD = value;
                    RaisePropertyChanged("CCA2LOD");
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

        public double CCA3LOD
        {
            get
            {
                return cca3LOD;
            }
            set
            {
                if (cca3LOD != value)
                {
                    cca3LOD = value;
                    RaisePropertyChanged("CCA3LOD");
                }
            }
        }

        public int StaffCount
        {
            get
            {
                return staffCount;
            }
            set
            {
                if (staffCount != value)
                {
                    staffCount = value;
                    RaisePropertyChanged("StaffCount");
                }
            }
        }

        public int State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    state = value;
                    RaisePropertyChanged("State");
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
