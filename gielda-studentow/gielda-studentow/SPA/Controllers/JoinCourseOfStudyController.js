(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('JoinCourseOfStudyController', JoinCourseOfStudyController);

    JoinCourseOfStudyController.$inject = ['$scope', '$http', '$location'];
    function JoinCourseOfStudyController($scope, $http, $location) {
        var vm = this;

        $http.get('/api/university/').then(
            function (response) {
                vm.universities = response.data;
                vm.universities.forEach(function(university) {
                    $http.get('/api/university/' + university.Id + '/faculties').then(
                        function (response) {
                            university.faculties = response.data;
                            university.faculties.forEach(function(faculty) {
                                $http.get('/api/student/me/courses/faculty/' + faculty.Id + '/leftover').then(
                                    function(response) {
                                        faculty.courses = response.data;
                                        faculty.courses.forEach(function(course) {
                                            $http.get('/api/courseofstudy/' + course.Id + '/groups').then(
                                            function(response) {
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

        vm.join = function () {
            var newGroups = [];
            vm.groups.forEach(function(group) {
                $scope.Course.GrpIds.forEach(function(grpId) {
                    if (group.Id == grpId) {
                        newGroups.push(group);
                    }
                });
                $location.path('/coursemanage');
            });

            newGroups.forEach(function(group) {
                $http({
                    method: 'PUT',
                    url: "/api/group/" + group.Id + "/join",
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    data: ""
                }).then(
                    function (response) {
                        console.log(response);
                    },
                    function (error) {
                        console.log("Error: " + error);
                    });
            });            
        };
    }
})();