'use strict';
app.factory('teacherService', ['$http', '$q', function ($http, $q) {

    var teacherServiceFactory = {};


    var _getOverview = function () {

        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/Teacher/Overview'
        }).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _list = function () {

        var deferred = $q.defer();
        $http.get('api/Teacher/List')
        .then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _add = function (data) {

        var deferred = $q.defer();
        $http.post('api/Teacher/Add', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (data) {

        var deferred = $q.defer();
        $http.put('api/Teacher/Update', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (teacherId) {

        var deferred = $q.defer();
        $http.delete('api/Teacher/Delete?teacherId=' + teacherId).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    teacherServiceFactory.getOverview = _getOverview;
    teacherServiceFactory.add = _add;
    teacherServiceFactory.update = _update;
    teacherServiceFactory.delete = _delete;
    teacherServiceFactory.list = _list;
    return teacherServiceFactory;

}]);
