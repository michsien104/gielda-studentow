using System.Web;
using System.Web.Optimization;

namespace gielda_studentow
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-cookies.js",
                        "~/Scripts/angular-route.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/angular-material.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/controllers").Include(
                        "~/SPA/Controllers/RegisterController.js",
                        "~/SPA/Controllers/ProfileController.js",
                        "~/SPA/Controllers/LoginController.js",
                        "~/SPA/Controllers/UserProfileController.js",
                        "~/SPA/Controllers/AddAnnouncementController.js",
                        "~/SPA/Controllers/MainTableController.js",
                        "~/SPA/Controllers/CourseManageController.js",
                        "~/SPA/Controllers/JoinCourseOfStudyController.js",
                        "~/SPA/Controllers/SettingsController.js",
                        "~/SPA/Controllers/IndexController.js",
                        "~/SPA/Controllers/SignupStudentController.js",
                        "~/SPA/Controllers/SignupTutorController.js",
                        "~/SPA/Controllers/MainPageController.js",
                        "~/SPA/Controllers/AdminPageController.js",
                        "~/SPA/Controllers/NewCourseOfStudyFormController.js",
                        "~/SPA/Controllers/NewFacultyFormController.js",
                        "~/SPA/Controllers/NewGroupFormController.js",
                        "~/SPA/Controllers/AddAnnouncementController.js",
                        "~/SPA/Controllers/MyAnnouncementsController.js",
                        "~/SPA/Controllers/NewUniversityFormController.js"));

            bundles.Add(new ScriptBundle("~/bundles/services").Include(
                        "~/SPA/Services/AuthenticationService.js",
                        "~/SPA/Services/FlashService.js",
                        "~/SPA/Services/UserService.js",
                        "~/SPA/Services/AnnouncementsService.js"));
        }
    }
}
