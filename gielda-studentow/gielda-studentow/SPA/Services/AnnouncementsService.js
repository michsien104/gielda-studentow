(function () {
    'use strict';

    angular
        .module('MyApp')
        .factory('AnnouncementsService', AnnouncementsService);

    AnnouncementsService.$inject = ['$http'];
    function AnnouncementsService($http) {
        var service = {};
        
        AnnouncementsService.GetAll = GetAll;
        AnnouncementsService.Create = Create;
        
        return service;


        function GetAll() {
            return $http.get('/api/announcement').then(handleSuccess, handleError('Error getting announcement by id'));
        }

        function GetById(id) {
            return $http.get('/api/announcement/' + id).then(handleSuccess, handleError('Error getting announcement by id'));
        }

        function Create(data) {
            return $http.post('/api/AddAnnouncement', data).then(handleSuccess, handleError('Error creating announcement'));
        }

        function Update(user) {
            return $http.put('/api/' + user.id, user).then(handleSuccess, handleError('Error updating user'));
        }

        function Delete(id) {
            return $http.delete('/api/' + id).then(handleSuccess, handleError('Error deleting user'));
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