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
            ObservableCollection<Staff> temp = new ObservableCollection<Staff>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    foreach(Staff s in conn.Query<Staff>(query))
                    {
                        temp.Add(s);
                    }
                    return temp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return temp;
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

        public static ObservableCollection<double> GetRosterWeeks()
        {
            string query = "SELECT Week FROM RosterTable;";
            HashSet<double> temp = new HashSet<double>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    foreach(double d in conn.Query<double>(query))
                    {
                        temp.Add(d);
                    }
                    return new ObservableCollection<double>(temp);
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
                "AppointedHours, HolidayHours, AbsenceHours, UnsocialHours FROM RosterTable WHERE Week=@Week;";
            ObservableCollection<Staff> temp = new ObservableCollection<Staff>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    foreach (Staff s in conn.Query<Staff>(query, new { week }))
                    {
                        temp.Add(s);
                    }
                    return temp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return temp;
                }
            }
        }

        public static Staff GetStaffRoster(double week, int id)
        {
            string query = "SELECT * FROM RosterTable WHERE Week=@Week AND StaffId=@Id;";
            Staff temp = new Staff();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    foreach (Staff s in conn.Query<Staff>(query, new { week, id }))
                    {
                        if (s.Id == id)
                        {
                            temp = s;
                            break;
                        }
                    }
                    return temp;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static void AddRoster(int id, double appointed, double absence, double holiday, double unsocial, double week)
        {
            string query = "INSERT INTO RosterTable " +
                "(Week, StaffId, StaffName, Role, ContractHours, AppointedHours, AbsenceHours, HolidayHours, UnsocialHours)" +
                " VALUES (@Week, @Id, @Name, @Role, @ContractHours, @Appointed, @Absence, @Holiday, @Unsocial);";
            Staff temp = new Staff();
            foreach(Staff s in Staffs)
            {
                if(s.Id == id)
                {
                    temp = s;
                    break;
                }
            }
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, new { week, id, temp.Name, temp.Role, temp.ContractHours, appointed, absence, holiday, unsocial });
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
                        AddRoster(id, appointed, 0.0, 0.0, 0.0, week);
                    }
                }
            }
        }

        public static void UpdateHoliday(int id, double holiday, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET HolidayHours=HolidayHours+@Holiday " +
                    " WHERE Week=@Week AND StaffId=@Id;";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        conn.Execute(query, new { holiday, week, id });
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
                        AddRoster(id, 0.0, 0.0, absence, 0.0, week);
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
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        conn.Execute(query, new { unsocial, week, id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
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
                        AddRoster(id, appointed, 0.0, 0.0, unsocial, week);
                    }
                }
            }
        }

        public static void UpdateAppointedHolidayUnsocial(int id, double appointed, double unsocial, double week)
        {
            if (id != 0)
            {
                string query = "UPDATE RosterTable" +
                    " SET AppointedHours=AppointedHours+@Appointed, HolidayHours=HolidayHours+@Appointed, UnsocialHours=UnsocialHours+@Unsocial " +
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
                        AddRoster(id, appointed, 0.0, appointed, unsocial, week);
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
