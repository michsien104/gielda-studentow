(function () {
    'use strict';

    angular
        .module('MyApp')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['UserService', '$location', '$rootScope', 'FlashService', '$cookies', '$http'];
    function LoginController(UserService, $location, $rootScope, FlashService, $cookies, $http) {
        var vm = this;

        vm.login = login;
        function login() {
            vm.dataLoading = true;
            UserService.Login(vm.user)
                .then(function (response) {
                    if (response.status === 200) {

                        $rootScope.globals = {
                            currentUser: {
                                username: vm.user.username,
                                authdata: response.data["access_token"]
                            }
                        };

                        console.log(response.data);
                        $http.defaults.headers.common['Authorization'] = 'Bearer ' + $rootScope.globals.currentUser.authdata;
                        console.log($http.defaults.headers.common['Authorization']);
                        var cookieExp = new Date();
                        cookieExp.setDate(cookieExp.getDate() + 7);
                        $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp });
                        vm.dataLoading = false;
                        $location.path('/cards');
                    } else {
                        console.log(response.status);
                        FlashService.Error(response.message);
                        vm.dataLoading = false;
                }
            });
        }

        function ClearCredentials() {
            $rootScope.globals = {};
            $cookies.remove('globals');
            $http.defaults.headers.common.Authorization = 'Basic';
        }
                /*, function (response) {
                if (response.success) {
                    //AuthenticationService.SetCredentials(vm.username, vm.password);
                    $location.path('/');
                } else {
                    FlashService.Error(response.message);
                    vm.dataLoading = false;
                }
            });*/
        
    }

})();