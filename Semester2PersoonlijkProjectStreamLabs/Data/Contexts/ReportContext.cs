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
        private readonly Connection _connection;
        public ReportContext(Connection connection)
        {
            _connection = connection;
        }

        public void ReportVideo(Report report)
        {
            try
            {
                _connection.conn.Open();
                using (SqlCommand cmd = new SqlCommand("ReportVideo", _connection.conn))
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
                _connection.conn.Close();
            }
        }

        public List<Report> GetReportsVideo(Video video)
        {
            try
            {
                List<Report> reports = new List<Report>();
                _connection.conn.Open();

                SqlCommand cmd = new SqlCommand("GetAllReportsVideo", _connection.conn)
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
                _connection.conn.Close();
            }
        }

        public void DeleteReportVideo(int reportId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteReport", _connection.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("ReportId", SqlDbType.Int).Value = reportId;

                _connection.conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }

        public List<Report> GetAllReports()
        {
            try
            {
                List<Report> reports = new List<Report>();
                _connection.conn.Open();

                SqlCommand cmd = new SqlCommand("GetAllReports", _connection.conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int reportId = (int)dr["ReportId"];
                    int videoId = (int)dr["VideoId"];
                    int userId = (int)dr["UserId"];
                    string content = dr["Content"].ToString();
                    Report report = new Report(reportId, videoId, userId, content);
                    reports.Add(report);
                }
                return reports;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _connection.conn.Close();
            }
        }
    }
}
