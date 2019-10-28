using Logic;
using Microsoft.AspNetCore.Mvc;
using Semester2PersoonlijkProjectStreamLabs.Models;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    public class VideoController : Controller
    {

        private readonly VideoLogic _videoLogic;

        public VideoController(VideoLogic videoLogic)
        {
            _videoLogic = videoLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult UploadVideo()
        {
            return View();
        }

        public ActionResult UploadVideo(VideoViewModel video)
        {
            return View();
        }
    }
}