using Data.Interfaces;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Memory
{
    public class ReportMemory : IReportContext
    {
        private List<Report> reportList = new List<Report>();
        public ReportMemory(List<Report> _reportList)
        {
            reportList = _reportList;
        }
        public void ReportVideo(Report report)
        {

        }

        public List<Report> GetReportsVideo(Video video)
        {
            List<Report> resultList = new List<Report>();
            foreach(var item in reportList.Where(x => x.VideoId == video.VideoId))
            {
                resultList.Add(item);
            }
            return resultList;
        }

        public void DeleteReportVideo(Report report)
        {

        }

        public List<Report> GetAllReports()
        {
            return new List<Report>();
        }
    }
}
