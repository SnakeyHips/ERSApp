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

        public static ObservableCollection<Session> GetSessions(string date)
        {
            string query = "SELECT * FROM SessionTable WHERE Date=@Date;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Session>(conn.Query<Session>(query, new { date }).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Session>();
                }
            };
        }

        public static int AddSession(Session s)
        {
            string query = "IF NOT EXISTS (SELECT * FROM SessionTable WHERE Date=@Date AND Location=@Location AND StartTime=@StartTime) " +
                "INSERT INTO SessionTable (Date, StartTime, EndTime, Length, Location, MDC, Chairs, SV1Id, SV1Name," +
                " SV1Start, SV1End, DRI1Id, DRI1Name, DRI1Start, DRI1End, DRI2Id, DRI2Name, DRI2Start, DRI2End, RN1Id, RN1Name, RN1Start, " +
                "RN1End, RN2Id, RN2Name, RN2Start, RN2End, RN3Id, RN3Name, RN3Start, RN3End, CCA1Id, CCA1Name, CCA1Start, " +
                "CCA1End, CCA2Id, CCA2Name, CCA2Start, CCA2End, CCA3Id, CCA3Name, CCA3Start, CCA3End, State) " +
                "VALUES (@Date, @StartTime, @EndTime, @Length, @Location, @MDC, @Chairs, @SV1Id, @SV1Name, @SV1Start, " +
                "@SV1End, @DRI1Id, @DRI1Name, @DRI1Start, @DRI1End, @DRI2Id, @DRI2Name, @DRI2Start, @DRI2End, @RN1Id, " +
                "@RN1Name, @RN1Start, @RN1End, @RN2Id, @RN2Name, @RN2Start, @RN2End, @RN3Id, @RN3Name, @RN3Start, " +
                "@RN3End, @CCA1Id, @CCA1Name, @CCA1Start, @CCA1End, @CCA2Id, @CCA2Name, @CCA2Start, @CCA2End, " +
                "@CCA3Id, @CCA3Name, @CCA3Start, @CCA3End, @State);";
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

        public static void UpdateSession(Session s)
        {
            string query = "UPDATE SessionTable" +
                " SET StartTime=@StartTime, EndTime=@EndTime, Length=@Length, Location=@Location, MDC=@MDC, Chairs=@Chairs" +
                " WHERE Date=@Date AND StartTime=@StartTime;";
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

        public static void DeleteSession(Session s)
        {
            string query = "DELETE FROM SessionTable WHERE Date=@Date AND Location=@Location AND StartTime=@StartTime;";
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
            string query = "UPDATE SessionTable" +
                " SET SV1Id=@SV1Id, SV1Name=@SV1Name, SV1Start=@SV1Start, SV1End=@SV1End," +
                " DRI1Id=@DRI1Id, DRI1Name=@DRI1Name, DRI1Start=@DRI1Start, DRI1End=@DRI1End," +
                " DRI2Id=@DRI2Id, DRI2Name=@DRI2Name, DRI2Start=@DRI2Start, DRI2End=@DRI2End," +
                " RN1Id=@RN1Id, RN1Name=@RN1Name, RN1Start=@RN1Start, RN1End=@RN1End," +
                " RN2Id=@RN2Id, RN2Name=@RN2Name, RN2Start=@RN2Start, RN2End=@RN2End," +
                " RN3Id=@RN3Id, RN3Name=@RN3Name, RN3Start=@RN3Start, RN3End=@RN3End," +
                " CCA1Id=@CCA1Id, CCA1Name=@CCA1Name, CCA1Start=@CCA1Start, CCA1End=@CCA1End," +
                " CCA2Id=@CCA2Id, CCA2Name=@CCA2Name, CCA2Start=@CCA2Start, CCA2End=@CCA2End," +
                " CCA3Id=@CCA3Id, CCA3Name=@CCA3Name, CCA3Start=@CCA3Start, CCA3End=@CCA3End," +
                " State=@State WHERE Date=@Date AND StartTime=@StartTime AND Location=@Location;";
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
            string query = "UPDATE StaffTable SET Role=@Role, ContractHours=@ContractHours WHERE Id=@Id;";
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
                "AppointedHours, AbsenceHours FROM RosterTable WHERE Week=@Week;";
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

        public static void AddRoster(int id, double appointed, double absence, double week)
        {
            string query = "INSERT INTO RosterTable (Week, StaffId, StaffName, Role, ContractHours, AppointedHours, AbsenceHours)" +
                " VALUES (@Week, @Id, @Name, @Role, @ContractHours, @Appointed, @Absence);";
            Staff s = StaffViewModel.Staffs.First(x => x.Id == id);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, new { week, id, s.Name, s.Role, s.ContractHours, appointed, absence });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public static void UpdateRoster(int id, double appointed, double absence, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed, AbsenceHours=AbsenceHours+@Absence " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { appointed, absence, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, appointed, absence, week);
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

        //Method for checking duplicates
        public static bool HasDuplicates(List<int> idList)
        {
            HashSet<int> hs = new HashSet<int>();
            foreach (int id in idList)
            {
                if (!hs.Add(id))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
