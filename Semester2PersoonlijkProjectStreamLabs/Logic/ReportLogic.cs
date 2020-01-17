using Data.Interfaces;
using System.Collections.Generic;
using Models;

namespace Logic
{
    public class ReportLogic
    {
        private readonly IReportContext _report;

        public ReportLogic(IReportContext report)
        {
            _report = report;
        }

        public void ReportVideo(Report report)
        {
            _report.ReportVideo(report);
        }

        public List<Report> GetReportsVideo(Video video)
        {
            return _report.GetReportsVideo(video);
        }

        public void DeleteReportVideo(Report report)
        {
            _report.DeleteReportVideo(report);
        }

        public List<Report> GetAllReports()
        {
            return _report.GetAllReports();
        }
    }
}
