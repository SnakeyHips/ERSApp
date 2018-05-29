using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;
using System.Windows;
using ERSApp.Model;
using Dapper;

namespace ERSApp.ViewModel
{
    public class AbsenceViewModel
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        public static ObservableCollection<Absence> Absences { get; set; }
        public static Absence SelectedAbsence { get; set; }

        public AbsenceViewModel()
        {
            LoadAbsences();
        }

        public static void LoadAbsences()
        {
            Absences = GetAbsences();
            foreach (Staff s in StaffViewModel.Staffs)
            {
                s.Status = GetStatus(s.Id, SelectedDate.Date);
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
    }
}
