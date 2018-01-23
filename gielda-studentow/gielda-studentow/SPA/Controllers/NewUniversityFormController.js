(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('NewUniversityFormController', NewUniversityFormController);

    NewUniversityFormController.$inject = ['$scope', '$http', '$location'];
    function NewUniversityFormController($scope, $http, $location) {
        var vm = this;

        vm.addUniversity = function() {
            var data = "Name=" + $scope.name + "&ShortName=" + $scope.shortName;
            $http({
                method: 'POST',
                url: "/api/university",
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