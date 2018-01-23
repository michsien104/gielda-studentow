(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('MainPageController', MainPageController);

    MainPageController.$inject = ['$scope', '$http', '$location', '$cookies'];
    function MainPageController($scope, $http, $location, $cookies) {
        var vm = this;
        
    }
})();