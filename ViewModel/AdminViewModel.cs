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
    public class AdminViewModel
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        public static ObservableCollection<Site> Sites { get; set; }
        public static ObservableCollection<Holiday> Holidays { get; set; }
        public static Site SelectedSite { get; set; }
        public static Holiday SelectedHoliday { get; set; }

        public AdminViewModel()
        {
            LoadAdmins();
        }

        public static void LoadAdmins()
        {
            Holidays = GetHolidays();
            Sites = GetSites();
        }

        public static ObservableCollection<Site> GetSites()
        {
            string query = "SELECT * FROM SiteTable";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Site>(conn.Query<Site>(query).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Site>();
                }
            }
        }

        public static ObservableCollection<Holiday> GetHolidays()
        {
            string query = "SELECT * FROM HolidayTable";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Holiday>(conn.Query<Holiday>(query).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Holiday>();
                }
            }
        }
        //public static int AddSites(List<Site> sites)
        //{
        //    string query = "INSERT INTO SiteTable (Name, Type, Times)" +
        //            "VALUES (@Name, @Type, @Times);";
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            return conn.Execute(query, sites);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //            return -1;
        //        }
        //    }
        //}
        //Method used to add holidays into holidaytable manually
        //public static int AddHolidays()
        //{
        //    List<Holiday> holidays = new List<Holiday>()
        //    {
        //        new Holiday(){ Name = "New Year's Day", Date = "01/01/2018" },
        //        new Holiday(){ Name = "Good Friday", Date = "30/03/2018" },
        //        new Holiday(){ Name = "Easter Monday", Date = "02/04/2018" },
        //        new Holiday(){ Name = "Early May Holiday", Date = "07/05/2018" },
        //        new Holiday(){ Name = "Spring Bank Holiday", Date = "27/05/2018" },
        //        new Holiday(){ Name = "Summer Bank Holiday", Date = "27/08/2018" },
        //        new Holiday(){ Name = "Christmas Day", Date = "25/12/2018" },
        //        new Holiday(){ Name = "Boxing Day", Date = "26/12/2018" }
        //    };
        //    string query = "INSERT INTO HolidayTable (Name, Date) VALUES (@Name, @Date);";
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            return conn.Execute(query, holidays);
        //        }
        //        catch
        //        {
        //            return -1;
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
