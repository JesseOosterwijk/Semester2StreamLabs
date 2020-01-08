using Data.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using Models;
using System;
using System.Data;

namespace Data.Contexts
{
    public class ReportContext : IReportContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void ReportVideo(Report report)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("ReportUser", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", SqlDbType.Int).Value = report.UserId;
                    cmd.Parameters.AddWithValue("@VideoId", SqlDbType.Int).Value = report.VideoId;
                    cmd.Parameters.AddWithValue("@Content", SqlDbType.NVarChar).Value = report.Content;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Report> GetReportsVideo(Video video)
        {
            try
            {
                List<Report> reports = new List<Report>();
                _conn.Open();

                SqlCommand cmd = new SqlCommand("GetAllReportsVideo", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@VideoId", SqlDbType.Int).Value = video.VideoId;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int reportId = (int)dr["ReportId"];
                    int userId = (int)dr["UserId"];
                    string content = dr["Content"].ToString();
                    Report report = new Report(reportId, video.VideoId, userId, content);
                    reports.Add(report);
                }
                return reports;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteReportVideo(Report report)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteReport", _conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("VideoId", SqlDbType.Int).Value = report.VideoId;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Report not deleted");
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
