using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Data.SqlClient;
using System.Windows;
using ERSApp.Models;
using Dapper;

namespace ERSApp.ViewModels
{
    public class StaffViewModel
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        public static ObservableCollection<Staff> Staffs { get; set; }
        public static Staff SelectedStaff { get; set; }

        public StaffViewModel()
        {
            LoadStaffs();
        }

        public static void LoadStaffs()
        {
            Staffs = GetStaff();
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
            string query = "INSERT INTO StaffTable (Id, Name, Role, Skill, Address, Number, ContractHours, WorkPattern)" +
                "VALUES (@Id, @Name, @Role, @Skill, @Address, @Number, @ContractHours, @WorkPattern);";
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

        public static void UpdateStaff(Staff s)
        {
            string query = "UPDATE StaffTable SET Role=@Role, Skill=@Skill, Address=@Address, Number=@Number, " +
                "ContractHours=@ContractHours, WorkPattern=@WorkPattern WHERE Id=@Id;";
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
                "AppointedHours, AbsenceHours, LowRateUHours, HighRateUHours, OvertimeHours " +
                "FROM RosterTable WHERE Week=@Week;";
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

        public static void AddRoster(int id, double appointed, double absence, double lowrateu, double highrateu, double overtime, double week)
        {
            string query = "INSERT INTO RosterTable " +
                "(Week, StaffId, StaffName, Role, ContractHours, AppointedHours, AbsenceHours, LowRateUHours, HighRateUHours, OvertimeHours)" +
                " VALUES (@Week, @Id, @Name, @Role, @ContractHours, @Appointed, @Absence, @Lowrateu, @Highrateu, @Overtime);";
            Staff s = Staffs.First(x => x.Id == id);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, new { week, id, s.Name, s.Role, s.ContractHours, appointed, absence, lowrateu, highrateu, overtime });
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
                        AddRoster(id, appointed, 0.0, 0.0, 0.0, 0.0, week);
                    }
                }
            }
        }

        public static void UpdateHighRateAppointed(int id, double appointed, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed, HighRateUHours=HighRateUHours+@Appointed" +
                    " WHERE Week=@Week AND StaffId=@Id;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        conn.Execute(query, new { appointed, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
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
                        AddRoster(id, 0.0, absence, 0.0, 0.0, 0.0, week);
                    }
                }
            }
        }

        public static void UpdateLowRateU(int id, double lowrateu, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET LowRateUHours=LowRateUHours+@Unsocial " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        conn.Execute(query, new { lowrateu, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public static void UpdateOvertime(int id, double overtime, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET OvertimeHours=OvertimeHours+@Overtime " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        conn.Execute(query, new { overtime, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public static void UpdateHours(int id, double appointed, double lowrateu, double overtime, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed, LowRateUHours=LowRateUHours+@Unsocial, OvertimeHours=OvertimeHours+@Overtime " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { appointed, lowrateu, overtime, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, appointed, 0.0, lowrateu, 0.0, overtime, week);
                    }
                }
            }
        }

        public static void UpdateHighRateU(int id, double appointed, double highrateu, double overtime, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed, HighRateUHours=HighRateUHours+@highrateu, " +
                    "OvertimeHours=OvertimeHours+@Overtime " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                int rows = 0;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        rows = conn.Execute(query, new { appointed, highrateu, overtime, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    if (rows == 0)
                    {
                        //Add in new roster if update fails
                        AddRoster(id, appointed, 0.0, 0.0, highrateu, overtime, week);
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
            List<Staff> Available = new List<Staff>();
            foreach (Staff s in Staffs)
            {
                if (s.Status == "Okay" && s.WorkPattern.Contains(SelectedDate.Date.DayOfWeek.ToString().Substring(0, 3)))
                {
                    Available.Add(s);
                }
            }
            return Available;
        }

        public static double GetWeek(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            return double.Parse(cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek).ToString()
                + "." + date.Year.ToString());
        }
    }
}
