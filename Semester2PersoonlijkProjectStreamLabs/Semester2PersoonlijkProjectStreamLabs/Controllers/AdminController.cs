using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;
using System.Linq;
using System.Collections.Generic;

namespace Semester2PersoonlijkProjectStreamLabs.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly AccountLogic _accountLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly CommentLogic _commentLogic;
        private readonly ReportLogic _reportLogic;
        private readonly VideoLogic _videoLogic;

        public AdminController(UserLogic userLogic, AccountLogic accountLogic, CategoryLogic categoryLogic, CommentLogic commentLogic, ReportLogic reportLogic, VideoLogic videoLogic)
        {
            _userLogic = userLogic;
            _accountLogic = accountLogic;
            _categoryLogic = categoryLogic;
            _commentLogic = commentLogic;
            _reportLogic = reportLogic;
            _videoLogic = videoLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy ="Admin")]
        [HttpGet]
        public ActionResult UserOverview()
        {
            UserViewModel uvm = new UserViewModel()
            {
                Users = _userLogic.GetAllUsers(),
            };

            return View("../Viewer/ViewerList", uvm);
        }

        [HttpGet]
        public IActionResult CategoryOverview()
        {
            CategoryViewModel overview = new CategoryViewModel()
            {
                Categories = _categoryLogic.GetAllCategories(),
            };

            return View("CategoryOverView", overview);
        }

        public ActionResult DisableUser(User user)
        {
            bool status = !user.Status;

            _accountLogic.UpdateStatus(user.UserId, status);

            return RedirectToAction("UserOverview");
        }

        public ActionResult ChangePassword(User user)
        {
            string changedPassword = _accountLogic.ChangePassword(user.UserId);
            _userLogic.SendEmail(user.EmailAddress, changedPassword);
            UserViewModel userViewModel = new UserViewModel
            {
                Users = _userLogic.GetAllUsers()
            };

            return View("UserOverview", userViewModel);
        }

        public IActionResult DeleteUser()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);
            _accountLogic.DeleteUser(userId);

            return RedirectToAction("UserOverview");
        }

        public ActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCategory(CategoryViewModel categoryView)
        {
            _categoryLogic.AddNewCategory(new Category(categoryView.CategoryName, categoryView.Description));
            return RedirectToAction("CategoryOverview");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            Category category = _categoryLogic.GetCategoryById(id);
            return View("CategoryView", new CategoryViewModel(category));
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel categoryView)
        {
            _categoryLogic.EditCategory(new Category(categoryView.CategoryId, categoryView.CategoryName, categoryView.Description));
            return RedirectToAction("CategoryOverview");
        }

        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            List<Video> _list = _videoLogic.GetVideosWithCategory(id);
            foreach(Video video in _list)
            {
                _videoLogic.SetVideosToDefaultCategory(video.VideoId);
            }

            _categoryLogic.DeleteCategory(id);
            return RedirectToAction("CategoryOverview");
        }

        public ActionResult DeleteComment(CommentViewModel commentView)
        {
            _commentLogic.DeleteComment(new Comment(commentView.VideoId, commentView.UserId, commentView.Content, commentView.TimeStamp));
            return View();
        }

        [HttpPost]
        public ActionResult GetCommentsUser(UserViewModel userViewModel)
        {

            CommentViewModel comments = new CommentViewModel();
            foreach(Comment comment in _commentLogic.GetAllCommentsByUser(userViewModel.UserId))
            {
                CommentViewModel viewModel = new CommentViewModel(comment);
                comments.Comments.Add(viewModel);
            }

            return View("UserComments", comments);
        }

        [HttpGet]
        public ActionResult DeleteReportVideo(ReportViewModel report)
        {
            _reportLogic.DeleteReportVideo(new Report(report.VideoId, report.UserId, report.Content));

            return View();
        }

        [HttpGet]
        public ActionResult ReportOverView()
        {
            ReportViewModel overview = new ReportViewModel()
            {
                Reports = _reportLogic.GetAllReports()
            };

            return View("ReportOverview", overview);
        }

        [HttpGet]
        public ActionResult VideoOverview()
        {
            VideoViewModel overview = new VideoViewModel()
            {
                Videos = _videoLogic.GetVideos()
            };

            return View("VideoOverview", overview);
        }

        [HttpGet]
        public ActionResult ReportOverViewVideo(VideoViewModel video)
        {
            ReportViewModel overview = new ReportViewModel()
            {
                Reports = _reportLogic.GetReportsVideo(new Video(video.VideoId, video.VideoCategory, video.Name, video.Description, video.DateOfUpload, video.ContentUrl))
            };

            return View("CategoryOverview", overview);
        }
    }
}