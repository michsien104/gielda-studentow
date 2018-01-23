(function () {
        'use strict';

        angular
            .module('MyApp')
            .controller('ProfileController', ProfileController);

        ProfileController.$inject = ['$scope', '$http'];
        function ProfileController($scope, $http) {
            var vm = this;

            vm.updateData = function (firstName, lastName) {
                var data = "FirstName=" + firstName + "&LastName=" + lastName;
                $http({
                    method: 'POST',
                    url: "/api/student/me/names",
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    data: data
                }).then(
                    function (response) {
                        
                    },
                    function (error) {
                        console.log("Error: " + error);
                    });
            }

            $http.get('/api/profile/').then(
                function (response) {
                    $scope.profile = response.data;

                    $http.get('/api/student/' + response.data.Id + '/courses').then(
                        function (response) {
                            vm.courses = response.data;
                            vm.courses.forEach(function(course) {
                                $http.get('/api/faculty/bycourse/' + course.Id).then(
                                    function (response) {
                                        course.Faculty = response.data;
                                        $http.get('/api/university/byfaculty/' + course.Faculty.Id).then(
                                            function (response) {
                                                course.Faculty.University = response.data;
                                            });
                                    });
                            });
                        });
                });

            $http.get("/api/tutor/mycourses").then(
                function(response) {
                    vm.tutorCourses = response.data;
                    vm.tutorCourses.forEach(function (course) {
                        $http.get('/api/faculty/bycourse/' + course.Id).then(
                            function (response) {
                                course.Faculty = response.data;
                                $http.get('/api/university/byfaculty/' + course.Faculty.Id).then(
                                    function (response) {
                                        course.Faculty.University = response.data;
                                    });
                            });
                    });
                });

            $http.get('/api/student/myprofile').then(
                function (response) {
                    $scope.student = response.data;
                });

            $http.get('/api/tutor/myprofile').then(
                function (response) {
                    $scope.tutor = response.data;
                });

            $scope.updateAvatarUrl = function() {
                var data = "Url=" + $scope.Url;
                $http({
                    method: 'PUT',
                    url: "/api/profile/avatar",
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    data: data
                }).then(
                    function(response) {
                        console.log(response);
                    },
                    function(error) {
                        console.log("Error: " + error);
                    });
            };
        }
})();