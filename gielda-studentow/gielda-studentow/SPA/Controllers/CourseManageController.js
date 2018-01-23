(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('CourseManageController', CourseManageController);

    CourseManageController.$inject = ['$scope', '$http','$location','$filter'];
    function CourseManageController($scope, $http, $location,$filter) {
        var vm = this;
        vm.courses = [];
        vm.groups = [];

        $http.get('/api/profile/').then(
            function (response) {
                $http.get('/api/student/' + response.data.Id + '/courses').then(
                    function (response) {
                        vm.courses = response.data;
                        vm.courses.forEach(function(course) {
                            $http.get('/api/student/mygroups/bycourse/' + course.Id).then(
                                function (response) {
                                    course.myGroups = response.data;
                                    course.myGroups.forEach(function(group) {
                                        $http.get('api/group/' + group.Id + '/members').then(
                                            function (response) {
                                                group.members = response.data;
                                            });
                                    });
                                });
                        });
                        vm.courses.forEach(function (course) {
                            $http.get('api/group/bycourse/' + course.Id + '/notmember/me').then(
                                function (response) {
                                    course.leftoverGroups = response.data;
                                });
                        });
                    });
            });
        vm.update = function () {
            $http.get('/api/profile/').then(
                function (response) {
                    $http.get('/api/student/' + response.data.Id + '/courses').then(
                        function (response) {
                            vm.courses = response.data;
                            vm.courses.forEach(function (course) {
                                $http.get('/api/student/mygroups/bycourse/' + course.Id).then(
                                    function (response) {
                                        course.myGroups = response.data;
                                        course.myGroups.forEach(function (group) {
                                            $http.get('api/group/' + group.Id + '/members').then(
                                                function (response) {
                                                    group.members = response.data;
                                                });
                                        });
                                    });
                            });
                            vm.courses.forEach(function (course) {
                                $http.get('api/group/bycourse/' + course.Id + '/notmember/me').then(
                                    function (response) {
                                        course.leftoverGroups = response.data;
                                    });
                            });
                        });
                });
        }
       

        $scope.getMyCourseGroups = function (courseId) {
            $http.get('/api/student/mygroups/bycourse/' + courseId).then(
                function (response) {
                    vm.groups.push(response.data);
                });
        }

        $scope.getMyCourseLeftoverGroups = function (courseId) {
            $http.get('api/group/bycourse/' + courseId + '/notmember/me').then(
                function (response) {
                    $scope.leftoverGroups = response.data;
                });
        }

        $scope.getGroupMembers = function(groupId) {
            $http.get('/api/group/' + groupId + '/members').then(
                function (response) {
                    $scope.members = response.data;
                });
        }

        $scope.leaveGroup = function(groupId) {
            $http.put('/api/group/' + groupId + '/leave').then(
                function (response) {
                });
            vm.update();
        }

        $scope.joinGroup = function(groupId) {
            $http.put('/api/group/' + groupId + '/join').then(
                function (response) {
                    vm.update();
                });
        }

        $scope.leaveCourse = function (courseId) {
            vm.courses.forEach(function(course) {
                if (course.Id == courseId) {
                    course.myGroups.forEach(function (group) {
                        $http.put('/api/group/' + group.Id + '/leave').then(
                            function (response) {
                            });
                    });
                }
            });
            vm.update();
        }
    }
})();