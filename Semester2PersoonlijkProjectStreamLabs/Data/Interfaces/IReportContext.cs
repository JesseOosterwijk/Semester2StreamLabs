using Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IReportContext
    {
        void ReportVideo(Report report);
        List<Report> GetReportsVideo(Video video);
        void DeleteReportVideo(Report report);
        List<Report> GetAllReports();
    }
}
