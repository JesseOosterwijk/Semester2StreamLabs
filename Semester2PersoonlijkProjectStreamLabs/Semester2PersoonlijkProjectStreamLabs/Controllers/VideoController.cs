using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProftaakASP_S2.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public ActionResult UploadVideo()
        {
            return View();
        }

        [DisableFormValueModelBinding]
        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file.Length > 0)
            {
                try
                {
                    string userName = (User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name).Value);
                    string pathString = Path.Combine(@"D:\Stream", userName);
                    if (!Directory.Exists(pathString))
                    {
                        Directory.CreateDirectory(pathString);
                    }
                    int fCount = Directory.GetFiles(pathString, "*", SearchOption.TopDirectoryOnly).Length + 1;
                    var filePath = Path.Combine(pathString, "video" + fCount.ToString());
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
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
            return Ok();
        }

    }
}