using Data.Interfaces;
using Data.Memory;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models;
using System;
using System.Linq;

namespace StreamLabsTests
{
    [TestClass]
    public class ReportTests
    {
        private ReportLogic _reportLogic;
        private IReportContext _reportContext;
        public List<Report> reportList = new List<Report>();

        private void InstanceLogic()
        {
            reportList.Add(new Report(1, 8, "test"));
            reportList.Add(new Report(2, 7, "testing"));
            reportList.Add(new Report(6, 5, "new test"));
            reportList.Add(new Report(2, 11, "hello world"));
            reportList.Add(new Report(3, 8, "this is a test"));
            reportList.Add(new Report(8, 1, "content"));
            _reportContext = new ReportMemory(reportList);
            _reportLogic = new ReportLogic(_reportContext);
        }

        [TestMethod]
        public void GetAllReportsVideo()
        {
            InstanceLogic();
            List<Report> expectedList = new List<Report>();
            Video testVid = new Video(2, 2, "testvideo", "namevideo", DateTime.Now, "url", 1);
            foreach (var item in reportList.Where(x => x.VideoId == testVid.VideoId))
            {
                expectedList.Add(item);
            }

            List<Report> resultList = _reportLogic.GetReportsVideo(testVid);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Assert.AreEqual(expectedList[i], resultList[i]);
            }

        }
    }
}