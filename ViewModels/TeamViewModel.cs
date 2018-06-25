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
    public class TeamViewModel
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        public static ObservableCollection<Team> Teams { get; set; }
        public static Team SelectedTeam { get; set; }

        public TeamViewModel()
        {
            LoadLocations();
        }

        public static void LoadLocations()
        {
            Teams = GetTeams();
        }

        public static ObservableCollection<Team> GetTeams()
        {
            string query = "SELECT * FROM TeamTable";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return new ObservableCollection<Team>(conn.Query<Team>(query).ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return new ObservableCollection<Team>();
                }
            }
        }

        public static int AddGroup(Team g)
        {
            string query = "IF NOT EXISTS (SELECT * FROM TeamTable WHERE Name=@Name) " +
                 "INSERT INTO TeamTable (Name, SV1Id, SV1Name, DRI1Id, DRI1Name, DRI2Id, DRI2Name, RN1Id, RN1Name, " +
                "RN2Id, RN2Name, RN3Id, RN3Name, CCA1Id, CCA1Name, CCA2Id, CCA2Name, CCA3Id, CCA3Name) " +
                "VALUES (@Name, @SV1Id, @SV1Name, @DRI1Id, @DRI1Name, @DRI2Id, @DRI2Name, @RN1Id, @RN1Name, " +
                "@RN2Id, @RN2Name, @RN3Id, @RN3Name, @CCA1Id, @CCA1Name, @CCA2Id, @CCA2Name, @CCA3Id, @CCA3Name);";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, g);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return -1;
                }
            }
        }

        public static int UpdateGroup(Team g)
        {
            string query = "UPDATE TeamTable " +
                "SET SV1Id=@SV1Id, SV1Name=@SV1Name, DRI1Id=@DRI1Id, DRI1Name=@DRI1Name, " +
                "DRI2Id=@DRI2Id, DRI2Name=@DRI2Name, RN1Id=@RN1Id, RN1Name=@RN1Name, " +
                "RN2Id=@RN2Id, RN2Name=@RN2Name, RN3Id=@RN3Id, RN3Name=@RN3Name, " +
                "CCA1Id=@CCA1Id, CCA1Name=@CCA1Name, CCA2Id=@CCA2Id, CCA2Name=@CCA2Name, " +
                "CCA3Id=@CCA3Id, CCA3Name=@CCA3Name WHERE Name=@Name;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.Execute(query, g);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return -1;
                }
            }
        }

        public static void DeleteGroup(Team g)
        {
            string query = "DELETE FROM GroupTable WHERE Name=@Name;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(query, g);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
