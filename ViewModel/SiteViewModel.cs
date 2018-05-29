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
    }
}
