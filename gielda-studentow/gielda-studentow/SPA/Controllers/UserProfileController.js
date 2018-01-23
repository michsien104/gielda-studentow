(function() {
    'use strict';

    angular
        .module('MyApp')
        .controller('UserProfileController', UserProfileController);

    UserProfileController.$inject = ['$scope', '$http', '$routeParams'];

    function UserProfileController($scope, $http, $routeParams) {

        $http.get('/api/student/' + $routeParams.id).then(
            function(response) {
                $scope.student = response.data;
            });

        $http.get('/api/student/' + $routeParams.id + '/courses').then(
            function(response) {
                $scope.coursesOfStudy = response.data;
            });
}
})
();