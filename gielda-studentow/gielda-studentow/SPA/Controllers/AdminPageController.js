(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('AdminPageController', AdminPageController);

    AdminPageController.$inject = ['$scope', '$http', '$location', '$cookies'];
    function AdminPageController($scope, $http, $location, $cookies) {
        var vm = this;

        vm.sendUniversityMessage = function (subject, content, id) {
            var data = "Subject=" + subject + "&Content=" + content;
            $http({
                method: 'POST',
                url: "/api/message/university/" + id,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: data
            }).then(
                function (response) {
                    console.log(response);
                },
                function (error) {
                    console.log("Error: " + error);
                });
        };

        vm.sendFacultyMessage = function (subject, content, id) {
            var data = "Subject=" + subject + "&Content=" + content;
            $http({
                method: 'POST',
                url: "/api/message/faculty/" + id,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: data
            }).then(
                function (response) {
                    console.log(response);
                },
                function (error) {
                    console.log("Error: " + error);
                });
        };

        vm.sendCourseMessage = function (subject, content, id) {
            var data = "Subject=" + subject + "&Content=" + content;
            $http({
                method: 'POST',
                url: "/api/message/course/" + id,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: data
            }).then(
                function (response) {
                    console.log(response);
                },
                function (error) {
                    console.log("Error: " + error);
                });
        };

        vm.sendGroupMessage = function (subject, content, id) {
            var data = "Subject=" + subject + "&Content=" + content;
            $http({
                method: 'POST',
                url: "/api/message/group/" + id,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: data
            }).then(
                function (response) {
                    console.log(response);
                },
                function (error) {
                    console.log("Error: " + error);
                });
        };

        $http.get("/api/student/me/admin/groups").then(
            function(response) {
                vm.admGroups = response.data;
                vm.admGroups.forEach(function(group) {
                    $http.get("/api/courseofstudy/bygroup/" + group.Id).then(
                        function (response) {
                            group.CourseOfStudy = response.data;
                            $http.get("/api/faculty/bycourse/" + group.CourseOfStudy.Id).then(
                                function (response) {
                                    group.CourseOfStudy.Faculty = response.data;
                                    $http.get("/api/university/byfaculty/" + group.CourseOfStudy.Faculty.Id).then(
                                        function (response) {
                                            group.CourseOfStudy.Faculty.University = response.data;
                                        });
                                });
                        });
                });
            });
        $http.get("/api/student/me/admin/courses").then(
            function (response) {
                vm.admCourses = response.data;
                vm.admCourses.forEach(function(course) {
                    $http.get("/api/faculty/bycourse/" + course.Id).then(
                        function (response) {
                            course.Faculty = response.data;
                            $http.get("/api/university/byfaculty/" + course.Faculty.Id).then(
                                function (response) {
                                    course.Faculty.University = response.data;
                                });
                        });
                });
            });
        $http.get("/api/student/me/admin/faculties").then(
            function (response) {
                vm.admFaculties = response.data;
                vm.admFaculties.forEach(function(fac) {
                    $http.get("/api/university/byfaculty/" + fac.Id).then(
                        function (response) {
                            fac.University = response.data;
                        });
                });
            });
        $http.get("/api/student/me/admin/universities").then(
            function (response) {
                vm.admUniversities = response.data;
            });
    }
})();