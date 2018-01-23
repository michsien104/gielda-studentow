(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('IndexController', IndexController);

    IndexController.$inject = ['$scope', '$http', '$location', '$cookies'];
    function IndexController($scope, $http, $location, $cookies) {
        var vm = this;

        vm.isUserSignedIn = function() {
            var cookie = $cookies.get('globals');
            if (cookie === undefined || cookie === null)
                return false;
            else
                return true;
        }

        vm.logout = function() {
            $cookies.remove('globals');
            $location.path('/login');
        }
    }
})();