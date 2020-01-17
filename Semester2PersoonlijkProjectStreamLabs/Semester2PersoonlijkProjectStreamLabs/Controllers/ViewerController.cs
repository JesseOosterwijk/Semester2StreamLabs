using System.Collections.Generic;
using System.IO;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

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

        [Authorize(Policy = "Viewer")]
        public IActionResult UpdateUserInfo()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            User currentUser = _userLogic.GetUserById(userId);
            UserViewModel model = new UserViewModel(currentUser);
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy ="Admin")]
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
        public ActionResult GetVideosUser(UserViewModel user)
        {
            List<Video> videos = _videoLogic.GetVideosUser(user.UserId);
            List<VideoViewModel> videoViewModels = new List<VideoViewModel>();
            foreach (Video video in videos)
            {
                VideoViewModel videoViewModel = new VideoViewModel(video);
                videoViewModels.Add(videoViewModel);
            }

            return View("ViewerVideoList", videoViewModels);
        }


        [HttpGet]
        public ActionResult GetVideo(VideoViewModel video)
        {
            return View("VideoView", video);
        }
    }
}