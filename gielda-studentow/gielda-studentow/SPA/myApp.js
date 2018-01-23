(function () {
    //Create a module
    var app = angular.module('MyApp', ['ngRoute', 'ngCookies']);

    //Showing Routing  
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $locationProvider
            .html5Mode(false)
            .hashPrefix('!');
        $routeProvider.when('/cards',
            {
                templateUrl: 'Home/MainTable',
                controller: 'MainTableController',
                controllerAs: 'vm'
            });

        $routeProvider.when('/register',
            {
                templateUrl: 'Home/Register',
                controller: 'RegisterController',
                controllerAs: 'vm'
            });

        $routeProvider.when('/login',
            {
                templateUrl: 'Home/Login',
                controller: 'LoginController',
                controllerAs: 'vm'
            });

        $routeProvider.when('/myprofile',
            {
                templateUrl: 'Home/Myprofile',
                controller: 'ProfileController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/addAnnouncement',
            {
                templateUrl: 'Home/AddAnnouncement',
                controller: 'AddAnnouncementController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/userprofile/:id',
            {
                templateUrl: 'Home/UserProfile',
                controller: 'UserProfileController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/coursemanage',
            {
                templateUrl: 'Home/CourseManage',
                controller: 'CourseManageController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/joincourse',
            {
                templateUrl: 'Home/JoinCourseOfStudy',
                controller: 'JoinCourseOfStudyController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/settings',
            {
                templateUrl: 'Home/Settings',
                controller: 'SettingsController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/signuptutor',
            {
                templateUrl: 'Home/SignupTutor',
                controller: 'SignupTutorController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/mainpage',
            {
                templateUrl: 'Home/MainPage',
                controller: 'MainPageController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/adminpage',
            {
                templateUrl: 'Home/AdminPage',
                controller: 'AdminPageController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/newuniversity',
            {
                templateUrl: 'Home/NewUniversityForm',
                controller: 'NewUniversityFormController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/newfaculty',
            {
                templateUrl: 'Home/NewFacultyForm',
                controller: 'NewFacultyFormController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/newcourseofstudy',
            {
                templateUrl: 'Home/NewCourseOfStudyForm',
                controller: 'NewCourseOfStudyFormController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/newgroup',
            {
                templateUrl: 'Home/NewGroupForm',
                controller: 'NewGroupFormController',
                controllerAs: 'vm'
            });
        $routeProvider.when('/myAnnouncements',
            {
                templateUrl: 'Home/MyAnnouncements',
                controller: 'MyAnnouncementsController',
                controllerAs: 'vm'
            });
        $routeProvider.otherwise({ redirectTo: '/mainpage' });
    }]);
    
   app.run(['$rootScope', '$location', '$cookies', '$http',
       function ($rootScope, $location, $cookies, $http) {


            // keep user logged in after page refresh
            $rootScope.globals = $cookies.get('globals') || {};
            if ($rootScope.globals.currentUser) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
            }

            $rootScope.$on('$locationChangeStart', function (event, next, current) {
                // redirect to login page if not logged in
                if ($location.path() !== '/login' && $location.path() !== '/register' && $rootScope.globals.currentUser === {}) {
                    $location.path('/login');
                }
            });
        }]);
})();