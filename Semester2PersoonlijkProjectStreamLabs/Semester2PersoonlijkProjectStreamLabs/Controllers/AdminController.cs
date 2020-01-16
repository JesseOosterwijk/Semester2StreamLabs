using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Models;
using Semester2PersoonlijkProjectStreamLabs.Models;
using System.Linq;

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

        public AdminController(UserLogic userLogic, AccountLogic accountLogic, CategoryLogic categoryLogic, CommentLogic commentLogic, ReportLogic reportLogic)
        {
            _userLogic = userLogic;
            _accountLogic = accountLogic;
            _categoryLogic = categoryLogic;
            _commentLogic = commentLogic;
            _reportLogic = reportLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserOverview()
        {
            UserViewModel uvm = new UserViewModel()
            {
                Users = _userLogic.GetAllUsers(),
            };

            return View("../Viewer/ViewerList", uvm);
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

        public ActionResult AddNewCategory(CategoryViewModel categoryView)
        {
            _categoryLogic.AddNewCategory(new Category(categoryView.CategoryName, categoryView.Description));
            return View();
        }

        public ActionResult EditCategory()
        {
            return View();
        }

        public ActionResult EditCategory(CategoryViewModel categoryView)
        {
            _categoryLogic.EditCategory(new Category(categoryView.CategoryName, categoryView.Description));
            return View();
        }

        public ActionResult DeleteCategory(CategoryViewModel categoryView)
        {
            _categoryLogic.DeleteCategory(new Category(categoryView.CategoryName, categoryView.Description));
            return View();
        }

        public ActionResult DeleteComment(CommentViewModel commentView)
        {
            _commentLogic.DeleteComment(new Comment(commentView.VideoId, commentView.UserId, commentView.Content, commentView.TimeStamp));
            return View();
        }

        [HttpPost]
        public ActionResult CategoryOverview()
        {
            CategoryViewModel overview = new CategoryViewModel()
            {
                Categories = _categoryLogic.GetAllCategories(),
            };

            return View("CategoryOverview", overview);
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

        [HttpPost]
        public ActionResult ReportOverView(VideoViewModel video)
        {
            ReportViewModel overview = new ReportViewModel()
            {
                Reports = _reportLogic.GetReportsVideo(new Video(video.VideoId, video.VideoCategory, video.Name, video.Description, video.DateOfUpload, video.ContentUrl))
            };

            return View("CategoryOverview", overview);
        }
    }
}