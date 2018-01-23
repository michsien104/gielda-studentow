(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('AddAnnouncementController', AddAnnouncementController);

    AddAnnouncementController.$inject = ['$scope', '$location','$http'];

    function AddAnnouncementController($scope, $location, $http) {
        var vm = this;

        this.ExpirationDate = new Date();
        // utworzenie daty
        function formatDate() {
            var d = new Date(),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;

            return [year, month, day].join('-');
        }
        function datePlusMonth() {
            var d = new Date(),
                month = '' + (d.getMonth() + 2),
                day = '' + d.getDate(),
                year = d.getFullYear();
            if (month === '13') {
                year = d.getFullYear() + 1;
                month = '12';
            }
            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;

            return [year, month, day].join('-');
        }

 
        $scope.Content = "";
        $scope.Title = "";
        $scope.Category = "";
        $scope.Price = "";
        $scope.Subject = "";
        $scope.Location = "";
        $scope.FromGroup = "";
        $scope.ToGroup = "";

        
        $http.get("/api/student/MyProfile").then(
            function (response) {
                var student = response.data;
                $http.get("/api/student/"+student.Id+"/Groups").then(
                    function (response) {
                        $scope.Groups = response.data;
                    });
            });

        vm.getGroups = function getGroups() {
            var courseId = "";
            for (var i = 0; i < $scope.Groups.length; i++){
                var courseOfStudyName = $scope.Groups[i].CourseOfStudy.Name + " , " + $scope.Groups[i].Name;
                if (courseOfStudyName == $scope.FromGroup)
                    courseId = $scope.Groups[i].CourseOfStudy.Id;
            }
            $http.get("/api/CourseOfStudy/"+ courseId+"/groups").then(
                function (response) {
                    $scope.Groups2 = response.data;
                });
        };

        vm.submit = function submit() {
            if ($scope.Type === "buy") {
                var data1 = "IssueDate=" + formatDate() + "&ExpirationDate=" + datePlusMonth() + "&Content=" + encodeURIComponent($scope.Content)
                        + "&Title=" + $scope.Title + "&CurrentStatus=0&Category=" + $scope.Category + "&Price=" + $scope.Price;
                    $http({
                        method: 'POST',
                        url: "/api/itemannouncement/buy",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        data: data1
                    }).then(
                        function (response) {
                            console.log(response);
                        },
                        function (error) {
                            console.log("Error: " + error);
                        });
                }
                else if ($scope.Type === "sell") {
                var data2 = "IssueDate=" + formatDate() + "&ExpirationDate=" + datePlusMonth() + "&Content=" + encodeURIComponent($scope.Content)
                        + "&Title=" + $scope.Title + "&CurrentStatus=0&Category=" + $scope.Category + "&Price=" + $scope.Price;
                    $http({
                        method: 'POST',
                        url: "/api/itemannouncement/sell",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        data: data2
                    }).then(
                        function (response) {
                            console.log(response);
                        },
                        function (error) {
                            console.log("Error: " + error);
                        });
                }
                else if ($scope.Type === "giveLesson") {
                var data3 = "IssueDate=" + formatDate() + "&ExpirationDate=" + datePlusMonth()  + "&Content=" + encodeURIComponent($scope.Content)
                        + "&Title=" + $scope.Title + "&CurrentStatus=0&Category=" + $scope.Category + "&Price=" + $scope.Price +
                        "&Subject=" + $scope.Subject + "&Location=" + $scope.Location;
                    $http({
                        method: 'POST',
                        url: "/api/PrivateLessons/give",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        data: data3
                    }).then(
                        function (response) {
                            console.log(response);
                        },
                        function (error) {
                            console.log("Error: " + error);
                        });
                }
                else if ($scope.Type === "takeLesson") {
                var data4 = "IssueDate=" + formatDate() + "&ExpirationDate=" + datePlusMonth()  + "&Content=" + encodeURIComponent($scope.Content)
                        + "&Title=" + $scope.Title + "&CurrentStatus=0&Category=" + $scope.Category + "&Price=" + $scope.Price +
                        "&Subject=" + $scope.Subject + "&Location=" + $scope.Location;
                    $http({
                        method: 'POST',
                        url: "/api/PrivateLessons/take",
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                        data: data4
                    }).then(
                        function (response) {
                            console.log(response);
                        },
                        function (error) {
                            console.log("Error: " + error);
                        });

            } else if ($scope.Type === "groupChange") {
                var data5 = "IssueDate=" + formatDate() + "&ExpirationDate=" + datePlusMonth() + "&Content=" + encodeURIComponent($scope.Content)
                    + "&Title=" + $scope.Title + "&CurrentStatus=0&Category=" + $scope.Category + "&Price=" + $scope.Price +
                    "&Subject=" + $scope.Subject + "&Location=" + $scope.Location;
                $http({
                    method: 'POST',
                    url: "/api/PrivateLessons/take",
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    data: data5
                }).then(
                    function (response) {
                        console.log(response);
                    },
                    function (error) {
                        console.log("Error: " + error);
                    });

            }
            $location.path('/myAnnouncements');


            };
    }

})
();