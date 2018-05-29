using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using ERSApp.Model;
using ERSApp.ViewModel;
using Dapper;
using System.Collections.ObjectModel;

namespace ERSApp
{
    public class CollectionManager
    {

        public CollectionManager()
        {
        }

        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;

        public static DateTime SelectedDate = DateTime.Now.Date;

        public static List<Session> GetSessions(string date)
        {
            string query = "SELECT * FROM SessionTable WHERE Date=@Date;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Query<Session>(query, new { date }).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new List<Session>();
                }
            };
        }

        public static int AddSession(Session s)
        {
            string query = "IF NOT EXISTS (SELECT * FROM SessionTable WHERE Date=@Date AND Site=@Site AND Time=@Time) " +
                "INSERT INTO SessionTable (Date, Type, Site, Time, LOD, Chairs, Bleeds, SV1Id, SV1Name, " +
                "SV1LOD, SV1UNS, DRI1Id, DRI1Name, DRI1LOD, DRI1UNS, DRI2Id, DRI2Name, DRI2LOD, DRI2UNS, RN1Id, RN1Name, " +
                "RN1LOD, RN1UNS, RN2Id, RN2Name, RN2LOD, RN2UNS, RN3Id, RN3Name, RN3LOD, RN3UNS, CCA1Id, CCA1Name, CCA1LOD, " +
                "CCA1UNS, CCA2Id, CCA2Name, CCA2LOD, CCA2UNS, CCA3Id, CCA3Name, CCA3LOD, CCA3UNS, StaffCount, State) " +
                "VALUES (@Date, @Type, @Site, @Time, @LOD, @Chairs, @Bleeds, @SV1Id, @SV1Name, @SV1LOD, @SV1UNS, " +
                "@DRI1Id, @DRI1Name, @DRI1LOD, @DRI1UNS, @DRI2Id, @DRI2Name, @DRI2LOD, @DRI2UNS, @RN1Id, @RN1Name, @RN1LOD, " +
                "@RN1UNS, @RN2Id, @RN2Name, @RN2LOD, @RN2UNS, @RN3Id, @RN3Name, @RN3LOD, @RN3UNS, @CCA1Id, @CCA1Name, @CCA1LOD, " +
                "@CCA1UNS, @CCA2Id, @CCA2Name, @CCA2LOD, @CCA2UNS, @CCA3Id, @CCA3Name, @CCA3LOD, @CCA3UNS, @StaffCount, @State);";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return -1;
                }
            }
        }

        public static int UpdateSession(Session s)
        {
            string query = "UPDATE SessionTable" +
                " SET Time=@Time, Type=@Type, Site=@Site, LOD=@LOD, Chairs=@Chairs, Bleeds=@Bleeds" +
                " WHERE Date=@Date AND Time=@Time;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return -1;
                }
            }
        }

        public static void DeleteSession(Session s)
        {
            string query = "DELETE FROM SessionTable WHERE Date=@Date AND Site=@Site AND Time=@Time;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateSessionStaff(Session s)
        {
            string query = "UPDATE SessionTable " +
                "SET SV1Id=@SV1Id, SV1Name=@SV1Name, SV1LOD=@SV1LOD, SV1UNS=@SV1UNS, " +
                "DRI1Id=@DRI1Id, DRI1Name=@DRI1Name, DRI1LOD=@DRI1LOD, DRI1UNS=@DRI1UNS, " +
                "DRI2Id=@DRI2Id, DRI2Name=@DRI2Name, DRI2LOD=@DRI2LOD, DRI2UNS=@DRI2UNS, " +
                "RN1Id=@RN1Id, RN1Name=@RN1Name, RN1LOD=@RN1LOD, RN1UNS=@RN1UNS, " +
                "RN2Id=@RN2Id, RN2Name=@RN2Name, RN2LOD=@RN2LOD, RN2UNS=@RN2UNS, " +
                "RN3Id=@RN3Id, RN3Name=@RN3Name, RN3LOD=@RN3LOD, RN3UNS=@RN3UNS, " +
                "CCA1Id=@CCA1Id, CCA1Name=@CCA1Name, CCA1LOD=@CCA1LOD, CCA1UNS=@CCA1UNS, " +
                "CCA2Id=@CCA2Id, CCA2Name=@CCA2Name, CCA2LOD=@CCA2LOD, CCA2UNS=@CCA2UNS, " +
                "CCA3Id=@CCA3Id, CCA3Name=@CCA3Name, CCA3LOD=@CCA3LOD, CCA3UNS=@CCA3UNS, " +
                "StaffCount=@StaffCount, State=@State " +
                "WHERE Date=@Date AND Site=@Site AND Time=@Time;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static ObservableCollection<Site> GetSites(string type)
        {
            string query = "SELECT * FROM SiteTable Where Type=@Type";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Site>(conn.Query<Site>(query, new { type }).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Site>();
                }
            }
        }

        public static int AddSites(List<Site> sites)
        {
            string query = "INSERT INTO SiteTable (Name, Type, Times)" +
                    "VALUES (@Name, @Type, @Times);";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, sites);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return -1;
                }
            }
        }

        public static ObservableCollection<Staff> GetStaff()
        {
            string query = "SELECT * FROM StaffTable";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Staff>(conn.Query<Staff>(query).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Staff>();
                }
            }
        }
        
        public static Session GetStaffSession(string date, string staffid)
        {
            string query = "SELECT * FROM SessionTable WHERE Date=@Date AND " +
                "(SV1Id=@StaffId OR DRI1Id=@StaffId OR DRI2Id=@StaffId " +
                "OR RN1Id=@StaffId OR RN2Id=@StaffId OR RN3Id=@StaffId " +
                "OR CCA1Id=@StaffId OR CCA2Id=@StaffId OR CCA3Id=@StaffId)";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.QuerySingle<Session>(query, new { date, staffid });
                }
                catch
                {
                    return new Session();
                }
            }
        }

        public static int AddStaff(Staff s)
        {
            string query = "INSERT INTO StaffTable (Id, Name, Role, ContractHours, WorkPattern)" +
                "VALUES (@Id, @Name, @Role, @ContractHours, @WorkPattern);";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, s);
                }
                catch
                {
                    return -1;
                }
            }
        }

        public static void UpdateStaff(Staff s)
        {
            string query = "UPDATE StaffTable SET Role=@Role, ContractHours=@ContractHours, WorkPattern=@WorkPattern WHERE Id=@Id;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, s);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static ObservableCollection<Absence> GetAbsences()
        {
            string query = "SELECT * FROM AbsenceTable";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Absence>(conn.Query<Absence>(query).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Absence>();
                }
            }
        }

        public static int AddAbsence(Absence a)
        {
            string query = "IF NOT EXISTS (SELECT * FROM AbsenceTable WHERE StaffId=@StaffId AND StartDate=@StartDate) " +
                "INSERT INTO AbsenceTable (StaffId, StaffName, Type, StartDate, EndDate, Length)" +
                "VALUES (@StaffId, @StaffName, @Type, @StartDate, @EndDate, @Length);";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, a);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return -1;
                }
            }
        }

        public static void UpdateAbsence(Absence a)
        {
            string query = "UPDATE AbsenceTable" +
                " SET Type=@Type, Length=@Length" +
                " WHERE StaffId=@StaffId AND StartDate=@StartDate;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, a);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void DeleteAbsence(Absence a)
        {
            string query = "DELETE FROM AbsenceTable WHERE StaffId=@StaffId AND StartDate=@StartDate;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, a);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static ObservableCollection<double> GetRosterWeeks()
        {
            string query = "SELECT Week FROM RosterTable;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<double>(conn.Query<double>(query).Distinct().ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<double>();
                }
            }
        }

        public static ObservableCollection<Staff> GetRoster(double week)
        {
            string query = "SELECT Week, StaffId as Id, StaffName as Name, Role, ContractHours, " +
                "AppointedHours, AbsenceHours, UnsocialHours FROM RosterTable WHERE Week=@Week;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Staff>(conn.Query<Staff>(query, new { week }).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Staff>();
                }
            }
        }

        public static Staff GetStaffRoster(double week, int id)
        {
            string query = "SELECT * FROM RosterTable WHERE Week=@Week AND StaffId=@Id;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Query<Staff>(query, new { week, id }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static void AddRoster(int id, double appointed, double absence, double unsocial, double week)
        {
            string query = "INSERT INTO RosterTable (Week, StaffId, StaffName, Role, ContractHours, AppointedHours, AbsenceHours, UnsocialHours)" +
                " VALUES (@Week, @Id, @Name, @Role, @ContractHours, @Appointed, @Absence, @Unsocial);";
            Staff s = StaffViewModel.Staffs.First(x => x.Id == id);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, new { week, id, s.Name, s.Role, s.ContractHours, appointed, absence, unsocial });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateAppointed(int id, double appointed, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { appointed, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, appointed, 0.0, 0.0, week);
                    }
                }
            }
        }

        public static void UpdateAbsence(int id, double absence, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AbsenceHours=AbsenceHours+@Absence " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { absence, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, 0.0, absence, 0.0, week);
                    }
                }
            }
        }

        public static void UpdateUnsocial(int id, double unsocial, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET UnsocialHours=UnsocialHours+@Unsocial " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { unsocial, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, 0.0, 0.0, unsocial, week);
                    }
                }
            }
        }

        public static void UpdateAppointedUnsocial(int id, double appointed, double unsocial, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed, UnsocialHours=UnsocialHours+@Unsocial " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { appointed, unsocial, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, appointed, 0.0, unsocial, week);
                    }
                }
            }
        }

        public static void DeleteRoster(int id, double week)
        {
            string query = "DELETE FROM RosterTable WHERE Week=@Week AND StaffId=@Id;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, new { id, week });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static List<Staff> GetAvailableStaff()
        {
            return StaffViewModel.Staffs
                .Where(x => x.Status == "Okay" && x.WorkPattern.Contains(SelectedDate.DayOfWeek.ToString().Substring(0, 3)))
                .ToList();
        }

        public static string GetStatus(int id, DateTime selectedDate)
        {
            try
            {
                return AbsenceViewModel.Absences.First(x => x.StaffId == id
                && DateTime.Parse(x.StartDate).CompareTo(selectedDate) <= 0
                && DateTime.Parse(x.EndDate).CompareTo(selectedDate) >= 0).Type;
            }
            catch
            {
                return "Okay";
            }
        }

        public static double GetLength(string starttime, string endtime)
        {
            try
            {
                return DateTime.Parse(endtime).Subtract(DateTime.Parse(starttime)).TotalHours;
            }
            catch
            {
                return 0.0;
            }
        }

        public static double GetWeek(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            return double.Parse(cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString()
                + "." + date.Year.ToString());
        }

        //Old update roster method which does all hour times at once
        //public static void UpdateRoster(int id, double appointed, double absence, double unsocial, double week)
        //{
        //    if (id != 0)
        //    {
        //        string query = "UPDATE RosterTable" +
        //            " SET AppointedHours=AppointedHours+@Appointed, AbsenceHours=AbsenceHours+@Absence, UnsocialHours=UnsocialHours+@Unsocial " +
        //            " WHERE Week=@Week AND StaffId=@Id;";
        //        int rows = 0;
        //        using (SqlConnection conn = new SqlConnection(connString))
        //        {
        //            try
        //            {
        //                conn.Open();
        //                rows = conn.Execute(query, new { appointed, absence, unsocial, week, id });
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.ToString());
        //            }
        //            if (rows == 0)
        //            {
        //                //Add in new roster if update fails
        //                AddRoster(id, appointed, absence, unsocial, week);
        //            }
        //        }
        //    }
        //}

        //Method for reading site csv 
        //private void btnRead_Click(object sender, RoutedEventArgs e)
        //{
        //    SiteList = new List<Site>();
        //    System.IO.StreamReader file = new System.IO.StreamReader("C:\\Source\\ERSApp\\ERSApp\\Sites.csv");
        //    string line;
        //    while ((line = file.ReadLine()) != null)
        //    {
        //        string[] array = line.Split(',');
        //        SiteList.Add(new Site()
        //        {
        //            Name = array[0],
        //            Type = array[1],
        //            Times = array[2]
        //        });
        //    }
        //    MessageBox.Show(SiteList[0].Name);
        //    MessageBox.Show(SiteList[SiteList.Count - 1].Name);
        //}
    }
}
