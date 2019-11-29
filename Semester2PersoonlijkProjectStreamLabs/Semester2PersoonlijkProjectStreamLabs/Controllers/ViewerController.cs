using System.Collections.Generic;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;
using Semester2PersoonlijkProjectStreamLabs.Models;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class ViewerController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly VideoLogic _videoLogic;

        public ViewerController(UserLogic userLogic, VideoLogic videoLogic)
        {
            _userLogic = userLogic;
            _videoLogic = videoLogic;
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
            string[] videos = _videoLogic.GetAllVideos(userName);
            foreach (string fileDirectory in videos)
            {
                VideoViewModel videoViewModel =  new VideoViewModel(new Video(fileDirectory));
                videoViewModels.Add(videoViewModel);
            }

            return View("ViewerVideoList", videoViewModels);
        }
    }
}