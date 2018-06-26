using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;
using System.Windows;
using ERSApp.Models;
using Dapper;

namespace ERSApp.ViewModels
{
    public class SessionViewModel
    {
        public static string connString = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        public static ObservableCollection<Session> Sessions { get; set; }
        public static Session SelectedSession { get; set; }

        public SessionViewModel()
        {
            Sessions = new ObservableCollection<Session>();
            SelectedDate.Date = DateTime.Now;
            LoadSessions();
        }

        public static void LoadSessions()
        {
            Sessions.Clear();
            foreach(Session s in GetSessions(SelectedDate.Date.ToShortDateString()))
            {
                Sessions.Add(s);
            }
        }

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
                "INSERT INTO SessionTable (Date, Day, Type, Site, Time, LOD, Chairs, OCC, Estimate, Holiday, Note, SV1Id, SV1Name, " +
                "SV1LOD, SV1UNS, SV1OT, DRI1Id, DRI1Name, DRI1LOD, DRI1UNS, DRI1OT, DRI2Id, DRI2Name, DRI2LOD, DRI2UNS, DRI2OT, RN1Id, RN1Name, " +
                "RN1LOD, RN1UNS, RN1OT, RN2Id, RN2Name, RN2LOD, RN2UNS, RN2OT, RN3Id, RN3Name, RN3LOD, RN3UNS, RN3OT, CCA1Id, CCA1Name, CCA1LOD, " +
                "CCA1UNS, CCA1OT, CCA2Id, CCA2Name, CCA2LOD, CCA2UNS, CCA2OT, CCA3Id, CCA3Name, CCA3LOD, CCA3UNS, CCA3OT, StaffCount, State) " +
                "VALUES (@Date, @Day, @Type, @Site, @Time, @LOD, @Chairs, @OCC, @Estimate, @Holiday, @Note, @SV1Id, @SV1Name, @SV1LOD, " +
                " @SV1UNS, @SV1OT, @DRI1Id, @DRI1Name, @DRI1LOD, @DRI1UNS, @DRI1OT, @DRI2Id, @DRI2Name, @DRI2LOD, @DRI2UNS, @DRI1OT, @RN1Id, @RN1Name, " +
                "@RN1LOD, @RN1UNS, @RN1OT, @RN2Id, @RN2Name, @RN2LOD, @RN2UNS, @RN2OT, @RN3Id, @RN3Name, @RN3LOD, @RN3UNS, @RN3OT, @CCA1Id, @CCA1Name, " +
                "@CCA1LOD, @CCA1UNS, @CCA1OT, @CCA2Id, @CCA2Name, @CCA2LOD, @CCA2UNS, @CCA2OT, @CCA3Id, @CCA3Name, @CCA3LOD, @CCA3UNS, @CCA3OT, " +
                "@StaffCount, @State);";
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
                " SET Time=@Time, Type=@Type, Site=@Site, LOD=@LOD, Chairs=@Chairs, OCC=@OCC, Estimate=@Estimate" +
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
                "SET SV1Id=@SV1Id, SV1Name=@SV1Name, SV1LOD=@SV1LOD, SV1UNS=@SV1UNS, SV1OT=@SV1OT, " +
                "DRI1Id=@DRI1Id, DRI1Name=@DRI1Name, DRI1LOD=@DRI1LOD, DRI1UNS=@DRI1UNS, DRI1OT=@DRI1OT, " +
                "DRI2Id=@DRI2Id, DRI2Name=@DRI2Name, DRI2LOD=@DRI2LOD, DRI2UNS=@DRI2UNS, DRI2OT=@DRI2OT, " +
                "RN1Id=@RN1Id, RN1Name=@RN1Name, RN1LOD=@RN1LOD, RN1UNS=@RN1UNS, RN1OT=@RN1OT, " +
                "RN2Id=@RN2Id, RN2Name=@RN2Name, RN2LOD=@RN2LOD, RN2UNS=@RN2UNS, RN2OT=@RN2OT, " +
                "RN3Id=@RN3Id, RN3Name=@RN3Name, RN3LOD=@RN3LOD, RN3UNS=@RN3UNS, RN3OT=@RN3OT, " +
                "CCA1Id=@CCA1Id, CCA1Name=@CCA1Name, CCA1LOD=@CCA1LOD, CCA1UNS=@CCA1UNS, CCA1OT=@CCA1OT, " +
                "CCA2Id=@CCA2Id, CCA2Name=@CCA2Name, CCA2LOD=@CCA2LOD, CCA2UNS=@CCA2UNS, CCA2OT=@CCA2OT, " +
                "CCA3Id=@CCA3Id, CCA3Name=@CCA3Name, CCA3LOD=@CCA3LOD, CCA3UNS=@CCA3UNS, CCA3OT=@CCA3OT, " +
                "StaffCount=@StaffCount, State=@State, Note=@Note " +
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

        public static Session GetStaffSession(string date, string staffid)
        {
            string query = "SELECT * FROM SessionTable WHERE Date=@Date AND @StaffId IN" +
                "(SV1Id, DRI1Id, DRI2Id, RN1Id, RN2Id, RN3Id, CCA1Id, CCA2Id, CCA3Id)";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    return conn.QuerySingle<Session>(query, new { date, staffid });
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
