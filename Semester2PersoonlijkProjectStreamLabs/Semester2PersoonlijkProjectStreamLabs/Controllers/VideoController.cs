using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;
using System;
using System.Collections.Generic;
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
        private readonly UserLogic _userLogic;
        private readonly IHostingEnvironment _environment;

        public VideoController(VideoLogic videoLogic, CategoryLogic categoryLogic, ReportLogic reportLogic, CommentLogic commentLogic, UserLogic userLogic, IHostingEnvironment environment)
        {
            _videoLogic = videoLogic;
            _categoryLogic = categoryLogic;
            _reportLogic = reportLogic;
            _commentLogic = commentLogic;
            _userLogic = userLogic;
            _environment = environment;
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
                    int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
                    string userName = (User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name).Value);
                    string folder = Path.Combine(_environment.WebRootPath, "video");
                    string url = @"\video\" + userName + @"\" + file.FileName;
                    string pathString = Path.Combine(folder, userName);
                    if (!Directory.Exists(pathString))
                    {
                        Directory.CreateDirectory(pathString);
                    }
                    var filePath = Path.Combine(pathString, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        _videoLogic.SaveVideo(new Video(userId, video.VideoId, video.Description, file.FileName, DateTime.Now, url, video.CategoryId));
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

        [Authorize(Policy = "Admin")]
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

        [HttpGet]
        public ActionResult ReportVideo(VideoViewModel video)
        {
            ReportViewModel model = new ReportViewModel
            {
                VideoId = video.VideoId
            };
            return View("../Viewer/CreateReport", model);
        }

        [HttpPost]
        public ActionResult ReportVideo(ReportViewModel report)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            _reportLogic.ReportVideo(new Report(report.VideoId, userId, report.Content));
            VideoViewModel model = new VideoViewModel(_videoLogic.GetVideoById(report.VideoId));
            return RedirectToAction("VideoDetails", model);
        }

        public ActionResult VideoDetails(VideoViewModel video)
        {
            return View("VideoDetails", video);
        }

        public ActionResult DeleteComment(CommentViewModel commentView)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            if (userId == commentView.UserId)
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

        [HttpGet]
        public ActionResult SearchForVideos(string searchTerm)
        {
            List<Video> videos = _videoLogic.SearchForVideos(searchTerm);
            List<VideoViewModel> videoViewModels = new List<VideoViewModel>();
            foreach (Video video in videos)
            {
                VideoViewModel videoViewModel = new VideoViewModel(video);
                videoViewModels.Add(videoViewModel);
            }
            return View("../Viewer/ViewerVideoList", videoViewModels);
        }

        [HttpGet]
        public ActionResult CommentOnVideo(VideoViewModel video)
        {
            CommentViewModel model = new CommentViewModel
            {
                VideoId = video.VideoId,
                Comments = new List<CommentViewModel>(),
                Status = true
            };
            foreach (Comment comment in _commentLogic.GetAllCommentsOnVideo(video.VideoId))
            {
                CommentViewModel viewModel = new CommentViewModel(comment);
                User commentUser = _userLogic.GetUserById(comment.UserId);
                viewModel.UserName = commentUser.UserName;
                model.Comments.Add(viewModel);
            }

            return View("../Viewer/CommentOnVideo", model);
        }

        [HttpPost]
        public ActionResult CommentOnVideo(CommentViewModel comment)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            _commentLogic.CommentOnVideo(new Comment(comment.VideoId, userId, comment.Content, DateTime.Now));
            return RedirectToAction("CommentOnVideo", new VideoViewModel(_videoLogic.GetVideoById(comment.VideoId)));
        }

    }
}