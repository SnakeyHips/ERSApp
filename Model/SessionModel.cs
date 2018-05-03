using System.ComponentModel;

namespace ERSApp.Model
{
    public class SessionModel
    {
    }

    public class Session : INotifyPropertyChanged
    {
        private string date;
        private string startTime;
        private string endTime;
        private double length;
        private string location;
        private string mdc;
        private int chairs;
        private int sv1Id;
        private string sv1Name;
        private string sv1Start;
        private string sv1End;
        private int dri1Id;
        private string dri1Name;
        private string dri1Start;
        private string dri1End;
        private int dri2Id;
        private string dri2Name;
        private string dri2Start;
        private string dri2End;
        private int rn1Id;
        private string rn1Name;
        private string rn1Start;
        private string rn1End;
        private int rn2Id;
        private string rn2Name;
        private string rn2Start;
        private string rn2End;
        private int rn3Id;
        private string rn3Name;
        private string rn3Start;
        private string rn3End;
        private int cca1Id;
        private string cca1Name;
        private string cca1Start;
        private string cca1End;
        private int cca2Id;
        private string cca2Name;
        private string cca2Start;
        private string cca2End;
        private int cca3Id;
        private string cca3Name;
        private string cca3Start;
        private string cca3End;
        private string state;


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

        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    RaisePropertyChanged("StartTime");
                }
            }
        }

        public string EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                    RaisePropertyChanged("EndTime");
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

        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    RaisePropertyChanged("Location");
                }
            }
        }

        public string MDC
        {
            get
            {
                return mdc;
            }
            set
            {
                if (mdc != value)
                {
                    mdc = value;
                    RaisePropertyChanged("MDC");
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

        public string SV1Start
        {
            get
            {
                return sv1Start;
            }
            set
            {
                if (sv1Start != value)
                {
                    sv1Start = value;
                    RaisePropertyChanged("SV1Start");
                }
            }
        }

        public string SV1End
        {
            get
            {
                return sv1End;
            }
            set
            {
                if (sv1End != value)
                {
                    sv1End = value;
                    RaisePropertyChanged("SV1End");
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

        public string DRI1Start
        {
            get
            {
                return dri1Start;
            }
            set
            {
                if (dri1Start != value)
                {
                    dri1Start = value;
                    RaisePropertyChanged("DRI1Start");
                }
            }
        }

        public string DRI1End
        {
            get
            {
                return dri1End;
            }
            set
            {
                if (dri1End != value)
                {
                    dri1End = value;
                    RaisePropertyChanged("DRI1End");
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

        public string DRI2Start
        {
            get
            {
                return dri2Start;
            }
            set
            {
                if (dri2Start != value)
                {
                    dri2Start = value;
                    RaisePropertyChanged("DRI2Start");
                }
            }
        }

        public string DRI2End
        {
            get
            {
                return dri2End;
            }
            set
            {
                if (dri2End != value)
                {
                    dri2End = value;
                    RaisePropertyChanged("DRI2End");
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

        public string RN1Start
        {
            get
            {
                return rn1Start;
            }
            set
            {
                if (rn1Start != value)
                {
                    rn1Start = value;
                    RaisePropertyChanged("RN1Start");
                }
            }
        }

        public string RN1End
        {
            get
            {
                return rn1End;
            }
            set
            {
                if (rn1End != value)
                {
                    rn1End = value;
                    RaisePropertyChanged("RN1End");
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

        public string RN2Start
        {
            get
            {
                return rn2Start;
            }
            set
            {
                if (rn2Start != value)
                {
                    rn2Start = value;
                    RaisePropertyChanged("RN2Start");
                }
            }
        }

        public string RN2End
        {
            get
            {
                return rn2End;
            }
            set
            {
                if (rn2End != value)
                {
                    rn2End = value;
                    RaisePropertyChanged("RN2End");
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

        public string RN3Start
        {
            get
            {
                return rn3Start;
            }
            set
            {
                if (rn3Start != value)
                {
                    rn3Start = value;
                    RaisePropertyChanged("RN3Start");
                }
            }
        }

        public string RN3End
        {
            get
            {
                return rn3End;
            }
            set
            {
                if (rn3End != value)
                {
                    rn3End = value;
                    RaisePropertyChanged("RN3End");
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

        public string CCA1Start
        {
            get
            {
                return cca1Start;
            }
            set
            {
                if (cca1Start != value)
                {
                    cca1Start = value;
                    RaisePropertyChanged("CCA1Start");
                }
            }
        }

        public string CCA1End
        {
            get
            {
                return cca1End;
            }
            set
            {
                if (cca1End != value)
                {
                    cca1End = value;
                    RaisePropertyChanged("CCA1End");
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

        public string CCA2Start
        {
            get
            {
                return cca2Start;
            }
            set
            {
                if (cca2Start != value)
                {
                    cca2Start = value;
                    RaisePropertyChanged("CCA2Start");
                }
            }
        }

        public string CCA2End
        {
            get
            {
                return cca2End;
            }
            set
            {
                if (cca2End != value)
                {
                    cca2End = value;
                    RaisePropertyChanged("CCA2End");
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

        public string CCA3Start
        {
            get
            {
                return cca3Start;
            }
            set
            {
                if (cca3Start != value)
                {
                    cca3Start = value;
                    RaisePropertyChanged("CCA3Start");
                }
            }
        }

        public string CCA3End
        {
            get
            {
                return cca3End;
            }
            set
            {
                if (cca3End != value)
                {
                    cca3End = value;
                    RaisePropertyChanged("CCA3End");
                }
            }
        }

        public string State
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
