(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('NewGroupFormController', NewGroupFormController);

    NewGroupFormController.$inject = ['$scope', '$http', '$location'];
    function NewGroupFormController($scope, $http, $location) {
        var vm = this;

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
                                    });
                            });
                        });
                });
            });

        vm.getFaculties = function () {
            var uniId = $scope.UniId;
            vm.universities.forEach(function (university) {
                if (university.Id == uniId) {
                    vm.faculties = university.faculties;
                }
            });
        }

        vm.getCourses = function () {
            var facId = $scope.FacId;
            vm.faculties.forEach(function (faculty) {
                if (faculty.Id == facId) {
                    vm.courses = faculty.courses;
                }
            });
        }

        vm.addGroup = function () {
            var data = "Name=" + $scope.name;
            $http({
                method: 'POST',
                url: "/api/group/courseofstudy/" + $scope.CouId,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: data
            }).then(
                function (response) {
                    console.log(response);
                },
                function (error) {
                    console.log("Error: " + error);
                });
            $location.path('/adminpage');
        }
    }
})();