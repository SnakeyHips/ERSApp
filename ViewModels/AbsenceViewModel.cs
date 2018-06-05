using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;
using System.Windows;
using ERSApp.Models;
using Dapper;

namespace ERSApp.ViewModels
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
                s.Status = GetStatus(s.Id);
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

        public static string GetStatus(int id)
        {
            string status = "Okay";
            foreach(Absence a in Absences)
            {
                if (a.StaffId == id
                    && DateTime.Parse(a.StartDate).CompareTo(SelectedDate.Date) <= 0
                    && DateTime.Parse(a.EndDate).CompareTo(SelectedDate.Date) >= 0)
                {
                    status = a.Type;
                    break;
                }
            }
            return status;
        }
    }
}
