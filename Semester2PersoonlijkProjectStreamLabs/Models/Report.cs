using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Report
    {
        public int ReportId { get; }
        public int VideoId { get; }
        public int UserId { get; }
        public string Content { get; }

        public Report(int reportId, int videoId, int userId, string content)
        {
            ReportId = reportId;
            VideoId = videoId;
            UserId = userId;
            Content = content;
        }

        public Report(int videoId, int userId, string content)
        {
            VideoId = videoId;
            UserId = userId;
            Content = content;
        }
    }
}
