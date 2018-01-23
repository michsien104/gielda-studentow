(function() {
    'use strict';

    angular
        .module('MyApp')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['UserService', '$http', '$scope', '$location', '$rootScope', 'FlashService'];

    function RegisterController(UserService, $http, $scope, $location, $rootScope, FlashService) {
        var vm = this;

        vm.register = function (num) {
            vm.dataLoading = true;
            var url = "/api/user/register/";
            if (num == 1) {
                url += "student";
            }
            else if (num == 2) {
                url += "tutor";
            }
            $http.post(url, vm.user).then(function (response) {
                console.log("response: " + response);
                $location.path('/login');
            },
                function (error) {
                    console.log("error: " + error);
                    vm.dataLoading = false;
                });
        };
        vm.changeType = function (num) {
            $scope.type = num;
        };
    } 
})
();