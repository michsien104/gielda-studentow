(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('SettingsController', SettingsController);

    SettingsController.$inject = ['$scope', '$http', '$location'];
    function SettingsController($scope, $http, $location) {
        var vm = this;

        $http.get("/api/profile/isstudent").then(
            function (response) {
                vm.isStudent = response.data;
            });

        $http.get("/api/profile/istutor").then(
            function (response) {
                vm.isTutor = response.data;
            });

        vm.changeUsername = function() {
            $http.put("/api/profile/username/?newUsername=" + $scope.username).then(
                function(response) {
                });
        };

        vm.changePassword = function() {
            var data = "Field=" + $scope.password;
            $http({
                method: 'PUT',
                url: "/api/profile/password",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                data: data
            }).then(
                function (response) {
                    console.log(response);
                },
                function (error) {
                    console.log("Error: " + error);
                });
        }

    }
})();