(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('MyAnnouncementsController', MyAnnouncementsController);

    MyAnnouncementsController.$inject = ['$scope', '$http', '$location', '$cookies'];
    function MyAnnouncementsController($scope, $http, $location, $cookies) {
        var vm = this;

        $http.get("/api/Announcement/senderAnnouncements").then(
            function (response) {
                vm.announcements = response.data;
            });

        vm.update = function () {
            $http.get("/api/Announcement/senderAnnouncements").then(
                function (response) {
                    vm.announcements = response.data;
                    console.log(response.data);
                });
        };

        vm.changeStatus = function (announcementId) {

            $http.put("/api/Announcement/changeStatus/" + announcementId).then(
                function (response) {
                    console.log(response.data);
                });
        };
    }


})();