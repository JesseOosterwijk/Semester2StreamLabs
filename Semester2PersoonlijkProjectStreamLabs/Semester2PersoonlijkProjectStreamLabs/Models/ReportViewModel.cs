using Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Semester2PersoonlijkProjectStreamLabs.Models
{
    public class ReportViewModel
    {
        public int ReportId { get; }
        public int VideoId { get; }
        public int UserId { get; }
        [Required(ErrorMessage = "Please fill in the reason for the report")]
        public string Content { get; }
        public List<Report> Reports { get; internal set; }

        public ReportViewModel(int reportId, int videoId, int userId, string content)
        {
            ReportId = reportId;
            VideoId = videoId;
            UserId = userId;
            Content = content;
        }

        public ReportViewModel(int videoId, int userId, string content)
        {
            VideoId = videoId;
            UserId = userId;
            Content = content;
        }

        public ReportViewModel(Report report)
        {
            ReportId = report.ReportId;
            VideoId = report.VideoId;
            UserId = report.UserId;
            Content = report.Content;
        }

        public ReportViewModel()
        {
            ;
        }
    }
}
