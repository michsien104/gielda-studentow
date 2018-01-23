(function () {
    'use strict';

    angular
        .module('MyApp')
        .factory('UserService', UserService);

    UserService.$inject = ['$http'];
    function UserService($http) {
        var service = {};

        service.Login = Login;
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.GetByUsername = GetByUsername;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;

        return service;

        function Login(user) {
            return $http({
                method: 'POST',
                url: '/token',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: "grant_type=password" + "&userName=" + user.username + "&password=" + user.password
            });
        }

        function GetAll() {
            return $http.get('/api/users').then(handleSuccess, handleError('Error getting all users'));
        }

        function GetById(id) {
            return $http.get('/api/users/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }

        function GetByUsername(username) {
            return $http.get('/api/users/' + username).then(handleSuccess, handleError('Error getting user by username'));
        }

        function Create(user) {
           return $http.post('/api/user/register', user ).then(handleSuccess, handleError('Error creating user'));
        }

        function Update(user) {
            return $http.put('/api/users/' + user.id, user).then(handleSuccess, handleError('Error updating user'));
        }

        function Delete(id) {
            return $http.delete('/api/users/' + id).then(handleSuccess, handleError('Error deleting user'));
        }

        // private functions

        function handleSuccess(res) {
            console.log(res);
            return res.data;
        }

        function handleError(error) {
            return function () {
                console.log({ success: false, message: error });
                return { success: false, message: error };
            };
        }
    }

})();