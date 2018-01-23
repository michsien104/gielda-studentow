using System.Web.Mvc;

namespace gielda_studentow.Controllers.View
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

      public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return PartialView();
        }

        public ActionResult Myprofile()
        {
            return PartialView();
        }

        public ActionResult MainTable()
        {
            return PartialView();
        }

        public ActionResult AddAnnouncement()
        {
            return PartialView();
        }

        public ActionResult UserProfile()
        {
            return PartialView();
        }

        public ActionResult CourseManage()
        {
            return PartialView();
        }

        public ActionResult JoinCourseOfStudy()
        {
            return PartialView();
        }

        public ActionResult Settings()
        {
            return PartialView();
        }

        public ActionResult SignupTutor()
        {
            return PartialView();
        }

        public ActionResult MainPage()
        {
            return PartialView();
        }

        public ActionResult AdminPage()
        {
            return PartialView();
        }

        public ActionResult NewFacultyForm()
        {
            return PartialView();
        }

        public ActionResult NewCourseOfStudyForm()
        {
            return PartialView();
        }

        public ActionResult NewGroupForm()
        {
            return PartialView();
        }

        public ActionResult NewUniversityForm()
        {
            return PartialView();
        }

        public ActionResult MyAnnouncements()
        {
            return PartialView();
        }
    }
}
