(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('NewFacultyFormController', NewFacultyFormController);

    NewFacultyFormController.$inject = ['$scope', '$http', '$location'];
    function NewFacultyFormController($scope, $http, $location) {
        var vm = this;

        $http.get('/api/university/').then(
            function (response) {
                vm.universities = response.data;
            });

        vm.addFaculty = function() {
            var data = "Name=" + $scope.name + "&ShortName=" + $scope.shortName;
            $http({
                method: 'POST',
                url: "/api/faculty/university/" + $scope.UniId,
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