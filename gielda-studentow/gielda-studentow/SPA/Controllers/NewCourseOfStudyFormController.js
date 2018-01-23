(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('NewCourseOfStudyFormController', NewCourseOfStudyFormController);

    NewCourseOfStudyFormController.$inject = ['$scope', '$http', '$location'];
    function NewCourseOfStudyFormController($scope, $http, $location) {
        var vm = this;
        $http.get('/api/university/').then(
            function (response) {
                vm.universities = response.data;
                vm.universities.forEach(function (university) {
                    $http.get('/api/university/' + university.Id + '/faculties').then(
                        function (response) {
                            university.faculties = response.data;
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

        vm.addCourse = function () {
            var data = "Name=" + $scope.name + "&StartYear=" + $scope.startYear;
            $http({
                method: 'POST',
                url: "/api/courseofstudy/faculty/" + $scope.FacId,
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