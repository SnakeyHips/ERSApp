using System.ComponentModel;

namespace ERSApp.Models
{
    public class SessionModel
    {
    }

    public class Session : INotifyPropertyChanged
    {
        private string date;
        private string day;
        private string type;
        private string site;
        private string time;
        private double lod;
        private int chairs;
        private int occ;
        private int estimate;
        private int holiday;
        private string note;
        private int sv1Id;
        private string sv1Name;
        private double sv1LOD;
        private double sv1UNS;
        private double sv1OT;
        private int dri1Id;
        private string dri1Name;
        private double dri1LOD;
        private double dri1UNS;
        private double dri1OT;
        private int dri2Id;
        private string dri2Name;
        private double dri2LOD;
        private double dri2UNS;
        private double dri2OT;
        private int rn1Id;
        private string rn1Name;
        private double rn1LOD;
        private double rn1UNS;
        private double rn1OT;
        private int rn2Id;
        private string rn2Name;
        private double rn2LOD;
        private double rn2UNS;
        private double rn2OT;
        private int rn3Id;
        private string rn3Name;
        private double rn3LOD;
        private double rn3UNS;
        private double rn3OT;
        private int cca1Id;
        private string cca1Name;
        private double cca1LOD;
        private double cca1UNS;
        private double cca1OT;
        private int cca2Id;
        private string cca2Name;
        private double cca2LOD;
        private double cca2UNS;
        private double cca2OT;
        private int cca3Id;
        private string cca3Name;
        private double cca3LOD;
        private double cca3UNS;
        private double cca3OT;
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

        public string Day
        {
            get
            {
                return day;
            }
            set
            {
                if (day != value)
                {
                    day = value;
                    RaisePropertyChanged("Day");
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

        public int OCC
        {
            get
            {
                return occ;
            }
            set
            {
                if (occ != value)
                {
                    occ = value;
                    RaisePropertyChanged("OCC");
                }
            }
        }

        public int Estimate
        {
            get
            {
                return estimate;
            }
            set
            {
                if (estimate != value)
                {
                    estimate = value;
                    RaisePropertyChanged("Estimate");
                }
            }
        }

        public int Holiday
        {
            get
            {
                return holiday;
            }
            set
            {
                if (holiday != value)
                {
                    holiday = value;
                    RaisePropertyChanged("Holiday");
                }
            }
        }

        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                if (note != value)
                {
                    note = value;
                    RaisePropertyChanged("Note");
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

        public double SV1UNS
        {
            get
            {
                return sv1UNS;
            }
            set
            {
                if (sv1UNS != value)
                {
                    sv1UNS = value;
                    RaisePropertyChanged("SV1UNS");
                }
            }
        }

        public double SV1OT
        {
            get
            {
                return sv1OT;
            }
            set
            {
                if (sv1OT != value)
                {
                    sv1OT = value;
                    RaisePropertyChanged("SV1OT");
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

        public double DRI1UNS
        {
            get
            {
                return dri1UNS;
            }
            set
            {
                if (dri1UNS != value)
                {
                    dri1UNS = value;
                    RaisePropertyChanged("DRI1UNS");
                }
            }
        }

        public double DRI1OT
        {
            get
            {
                return dri1OT;
            }
            set
            {
                if (dri1OT != value)
                {
                    dri1OT = value;
                    RaisePropertyChanged("DRI1OT");
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

        public double DRI2UNS
        {
            get
            {
                return dri2UNS;
            }
            set
            {
                if (dri2UNS != value)
                {
                    dri2UNS = value;
                    RaisePropertyChanged("DRI2UNS");
                }
            }
        }

        public double DRI2OT
        {
            get
            {
                return dri2OT;
            }
            set
            {
                if (dri2OT != value)
                {
                    dri2OT = value;
                    RaisePropertyChanged("DRI2OT");
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

        public double RN1UNS
        {
            get
            {
                return rn1UNS;
            }
            set
            {
                if (rn1UNS != value)
                {
                    rn1UNS = value;
                    RaisePropertyChanged("RN1UNS");
                }
            }
        }

        public double RN1OT
        {
            get
            {
                return rn1OT;
            }
            set
            {
                if (rn1OT != value)
                {
                    rn1OT = value;
                    RaisePropertyChanged("RN1OT");
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

        public double RN2UNS
        {
            get
            {
                return rn2UNS;
            }
            set
            {
                if (rn2UNS != value)
                {
                    rn2UNS = value;
                    RaisePropertyChanged("RN2UNS");
                }
            }
        }

        public double RN2OT
        {
            get
            {
                return rn2OT;
            }
            set
            {
                if (rn2OT != value)
                {
                    rn2OT = value;
                    RaisePropertyChanged("RN2OT");
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

        public double RN3UNS
        {
            get
            {
                return rn3UNS;
            }
            set
            {
                if (rn3UNS != value)
                {
                    rn3UNS = value;
                    RaisePropertyChanged("RN3UNS");
                }
            }
        }

        public double RN3OT
        {
            get
            {
                return rn3OT;
            }
            set
            {
                if (rn3OT != value)
                {
                    rn3OT = value;
                    RaisePropertyChanged("RN3OT");
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

        public double CCA1UNS
        {
            get
            {
                return cca1UNS;
            }
            set
            {
                if (cca1UNS != value)
                {
                    cca1UNS = value;
                    RaisePropertyChanged("CCA1UNS");
                }
            }
        }

        public double CCA1OT
        {
            get
            {
                return cca1OT;
            }
            set
            {
                if (cca1OT != value)
                {
                    cca1OT = value;
                    RaisePropertyChanged("CCA1OT");
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

        public double CCA2UNS
        {
            get
            {
                return cca2UNS;
            }
            set
            {
                if (cca2UNS != value)
                {
                    cca2UNS = value;
                    RaisePropertyChanged("CCA2UNS");
                }
            }
        }

        public double CCA2OT
        {
            get
            {
                return cca2OT;
            }
            set
            {
                if (cca2OT != value)
                {
                    cca2OT = value;
                    RaisePropertyChanged("CCA2OT");
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

        public double CCA3UNS
        {
            get
            {
                return cca3UNS;
            }
            set
            {
                if (cca3UNS != value)
                {
                    cca3UNS = value;
                    RaisePropertyChanged("CCA3UNS");
                }
            }
        }

        public double CCA3OT
        {
            get
            {
                return cca3OT;
            }
            set
            {
                if (cca3OT != value)
                {
                    cca3OT = value;
                    RaisePropertyChanged("CCA3OT");
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
