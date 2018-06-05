using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using ERSApp.Models;
using Dapper;

namespace ERSApp.ViewModels
{
    public class SiteViewModel
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        public static ObservableCollection<Site> CommunityLocations { get; set; }
        public static ObservableCollection<Site> MDCLocations { get; set; }
        public static string SelectedLocation { get; set; }

        public SiteViewModel()
        {
            LoadLocations();
        }

        public static void LoadLocations()
        {
            CommunityLocations = GetSitesType("Community");
            MDCLocations = GetSitesType("MDC");
        }

        public static ObservableCollection<Site> GetSitesType(string type)
        {
            string query = "SELECT * FROM SiteTable Where Type=@Type";
            ObservableCollection<Site> temp = new ObservableCollection<Site>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    foreach(Site s in conn.Query<Site>(query, new { type }))
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
    }
}
