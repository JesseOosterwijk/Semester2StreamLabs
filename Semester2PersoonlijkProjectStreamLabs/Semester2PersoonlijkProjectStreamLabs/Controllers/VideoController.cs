using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class VideoController : Controller
    {

        private readonly VideoLogic _videoLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReportLogic _reportLogic;
        private readonly CommentLogic _commentLogic;

        public VideoController(VideoLogic videoLogic, CategoryLogic categoryLogic, ReportLogic reportLogic, CommentLogic commentLogic)
        {
            _videoLogic = videoLogic;
            _categoryLogic = categoryLogic;
            _reportLogic = reportLogic;
            _commentLogic = commentLogic;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UploadVideo()
        {
            ViewData["Categories"] = _categoryLogic.GetAllCategories();

            return View("UploadVideo");
        }
      

        [HttpPost]
        public async Task<IActionResult> UploadVideo(VideoViewModel video, IFormFile file)
        {
            if (file.Length > 0)
            {
                try
                {
                    string userName = (User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name).Value);
                    string pathString = Path.Combine(@"C:\Users\jesse\source\repos\Semester2PPStreamLabs\Semester2PersoonlijkProjectStreamLabs\Semester2PersoonlijkProjectStreamLabs\wwwroot\video", userName);
                    if (!Directory.Exists(pathString))
                    {
                        Directory.CreateDirectory(pathString);
                    }
                    int fCount = Directory.GetFiles(pathString, "*", SearchOption.TopDirectoryOnly).Length + 1;
                    var filePath = Path.Combine(pathString, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        string url = filePath.Remove(0, 125);
                        _videoLogic.SaveVideo(new Video(video.VideoId, video.Description, file.FileName, DateTime.Now, url, video.CategoryId));
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR: " + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("UploadVideo");
        }

        [Authorize(Policy="Admin")]
        public ActionResult DeleteVideo(VideoViewModel video)
        {
            _videoLogic.DeleteVideo(new Video(video.VideoId, video.VideoCategory, video.Description, video.Name, video.DateOfUpload, video.ContentUrl));
            string userName = (User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name).Value);
            string pathString = Path.Combine(@"C:\Users\jesse\source\repos\Semester2PPStreamLabs\Semester2PersoonlijkProjectStreamLabs\Semester2PersoonlijkProjectStreamLabs\wwwroot\video", userName);
            var filePath = Path.Combine(pathString, video.Name);
            if ((System.IO.File.Exists(filePath)))
            {
                System.IO.File.Delete(filePath);
            }

            return View();
        }

        public ActionResult ReportVideo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReportVideo(ReportViewModel report)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            _reportLogic.ReportVideo(new Report(report.VideoId, userId, report.Content));
            return View();
        }

        public ActionResult DeleteComment(CommentViewModel commentView)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            if(userId == commentView.UserId)
            {
                _commentLogic.DeleteComment(new Comment(commentView.VideoId, commentView.UserId, commentView.Content, commentView.TimeStamp));
            }
            else
            {
                ViewBag.Message = "Can't delete other people's comments!";
            }
            return View();
        }
        
        public ActionResult EditComment()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditComment(CommentViewModel commentView)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            if (userId == commentView.UserId)
            {
                _commentLogic.EditComment(new Comment(commentView.VideoId, commentView.UserId, commentView.Content, commentView.TimeStamp));
            }
            return View("VideoComments");
        }

        public ActionResult GetCommentsVideo(VideoViewModel video)
        {
            CommentViewModel overview = new CommentViewModel()
            {
                Comments = _commentLogic.GetAllCommentsOnVideo(video.VideoId)
            };

            return View("VideoComments", overview);
        }
    }
}