(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('MainTableController', MainTableController);

    MainTableController.$inject = ['AnnouncementsService', '$scope', '$http','$location'];
    function MainTableController(AnnouncementsService, $scope, $http, $location) {
        var vm = this;

        $scope.type = 0;

        vm.changeType = function (num) {
            $scope.type = num;
        };

        $http.get("/api/message/order/newest").then(
            function(response) {
                vm.messages = response.data;
            });

        $http.get("/api/announcement/order/issuedate/newest").then(
            function (response) {
                $scope.announcements = response.data;
                console.log($scope.announcements);
            },
            function(error) {
                console.log("Error: " + error);
            }
        );

        $scope.gotoUser = function(Id) {
            $location.path("/userprofile/" + Id);
        };

        $scope.getMainImage = function(AnnouncementId) {
            $http.get("/api/announcement/" + AnnouncementId + "/images").then(
                function(response) {
                    $scope.mainImage = response.data[0].Url;
                },
                function(error) {
                    console.log("Error: " + error);
                }
            );
        };

        $scope.getImages = function (AnnouncementId) {
            $http.get("/api/announcement/" + AnnouncementId + "/images").then(
                function(response) {
                    $scope.images = response.data;
                },
                function(error) {
                    console.log("Error: " + error);
                }
            );
        };

        vm.evalAnnouncementType = function(id) {
            vm.getSellItemAnnouncement(id);
            vm.getBuyItemAnnouncement(id);
            vm.getTakePrivateLessonsAnnouncement(id);
            vm.getGivePrivateLessonsAnnouncement(id);
            vm.getChangeGroupAnnouncement(id);
        }

        vm.getSellItemAnnouncement = function(id) {
            $http.get("/api/itemannouncement/sell/" + id).then(
                function(response) {
                    vm.sellItemAnnouncement = response.data;
                });
        }

        vm.getBuyItemAnnouncement = function (id) {
            $http.get("/api/itemannouncement/buy/" + id).then(
                function (response) {
                    vm.buyItemAnnouncement = response.data;
                });
        }

        vm.getTakePrivateLessonsAnnouncement = function(id) {
            $http.get("/api/privatelessons/take/" + id).then(
                function (response) {
                    vm.takePrivateLessonsAnnouncement = response.data;
                });
        }

        vm.getGivePrivateLessonsAnnouncement = function (id) {
            $http.get("/api/privatelessons/give/" + id).then(
                function (response) {
                    vm.givePrivateLessonsAnnouncement = response.data;
                });
        }

        vm.getChangeGroupAnnouncement = function(id) {
            $http.get("/api/changegroupannouncement/" + id).then(
            function(response) {
                vm.changeGroupAnnouncement = response.data;
            });
        }

    }
})();