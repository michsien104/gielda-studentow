(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('SignupTutorController', SignupTutorController);

    SignupTutorController.$inject = ['$scope', '$http', '$location'];
    function SignupTutorController($scope, $http, $location) {
        var vm = this;
        $scope.courses = [];

        vm.signup = function() {
            var data = "FirstName=" + $scope.tutor.FirstName + "&LastName=" + $scope.tutor.LastName;
            $http({
                method: 'POST',
                url: "/api/tutor/me/names",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: data
            }).then(
                function (response) {
                    $scope.courses.forEach(function (course) {
                        $http.post('/api/tutor/me/course/' + course.Id).then(
                            function (response) {
                            });
                    });
                },
                function (error) {
                    console.log("Error: " + error);
                });
            
        }

        vm.addCourseToTable = function () {
            $http.get("/api/courseofstudy/" + $scope.Course.CouId).then(
            function(response) {
                var course = response.data;
                $http.get("/api/university/byfaculty/" + course.Faculty.Id).then(
                    function (response) {
                        course.Faculty.University = response.data;
                        $scope.courses.push(course);
                    });
                });
        }

        $http.get('/api/university/').then(
            function (response) {
                vm.universities = response.data;
                vm.universities.forEach(function (university) {
                    $http.get('/api/university/' + university.Id + '/faculties').then(
                        function (response) {
                            university.faculties = response.data;
                            university.faculties.forEach(function (faculty) {
                                $http.get('/api/faculty/' + faculty.Id + '/courses').then(
                                    function (response) {
                                        faculty.courses = response.data;
                                        faculty.courses.forEach(function (course) {
                                            $http.get('/api/courseofstudy/' + course.Id + '/groups').then(
                                                function (response) {
                                                    course.groups = response.data;
                                                });
                                        });
                                    });
                            });
                        });
                });
            });

        vm.getFaculties = function () {
            var uniId = $scope.Course.UniId;
            vm.universities.forEach(function (university) {
                if (university.Id == uniId) {
                    vm.faculties = university.faculties;
                }
            });
        }

        vm.getCourses = function () {
            var facId = $scope.Course.FacId;
            vm.faculties.forEach(function (faculty) {
                if (faculty.Id == facId) {
                    vm.courses = faculty.courses;
                }
            });
        }

        vm.getGroups = function () {
            var couId = $scope.Course.CouId;
            vm.courses.forEach(function (course) {
                if (course.Id == couId) {
                    vm.groups = course.groups;
                }
            });
        }
    }
})();