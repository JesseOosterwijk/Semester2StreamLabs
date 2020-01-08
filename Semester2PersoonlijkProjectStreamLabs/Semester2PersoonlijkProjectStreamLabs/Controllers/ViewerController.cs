using System;
using System.Collections.Generic;
using System.IO;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;
using System.Linq;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class ViewerController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly VideoLogic _videoLogic;
        private readonly CommentLogic _commentLogic;

        public ViewerController(UserLogic userLogic, VideoLogic videoLogic, CommentLogic commentLogic)
        {
            _userLogic = userLogic;
            _videoLogic = videoLogic;
            _commentLogic = commentLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserOverview()
        {
            List<UserViewModel> userViewList = new List<UserViewModel>();
            foreach (User user in _userLogic.GetAllUsers())
            {
                userViewList.Add(new UserViewModel(user));
            }

            return View("ViewerList", userViewList);
        }

        [HttpGet]
        public ActionResult GetVideosUser(UserViewModel user)
        {
            List<VideoViewModel> videoViewModels = new List<VideoViewModel>();
            string userName = user.FirstName + " " + user.LastName;
            string pathString = Path.Combine(@"C:\Users\jesse\source\repos\Semester2PPStreamLabs\Semester2PersoonlijkProjectStreamLabs\Semester2PersoonlijkProjectStreamLabs\wwwroot\video", userName);
            List<Video> videos = _videoLogic.GetVideos();
            foreach (Video video in videos)
            {
                VideoViewModel videoViewModel =  new VideoViewModel(video);
                videoViewModels.Add(videoViewModel);
            }

            return View("ViewerVideoList", videoViewModels);
        }

        [HttpGet]
        public ActionResult CommentOnVideo(VideoViewModel video)
        {
            CommentViewModel model = new CommentViewModel
            {
                VideoId = video.VideoId
            };
            return View("CommentOnVideo", model);
        }

        [HttpPost]
        public ActionResult CommentOnVideo(CommentViewModel comment)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            _commentLogic.CommentOnVideo(new Comment(comment.VideoId, userId, comment.Content, DateTime.Now));
            return RedirectToAction("CommentOnVideo");
        }


        [HttpGet]
        public ActionResult GetVideo(VideoViewModel video)
        {
            return View("VideoView", video);
        }
    }
}