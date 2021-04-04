'use strict';
app.factory('gradeService', ['$http', '$q', function ($http, $q) {

    var gradeServiceFactory = {};


    var _getOverview = function () {

        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/Grade/Overview'
        }).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _list = function () {

        var deferred = $q.defer();
        $http.get('api/Grade/List')
        .then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _add = function (data) {

        var deferred = $q.defer();
        $http.post('api/Grade/Add', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (data) {

        var deferred = $q.defer();
        $http.put('api/Grade/Update', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    gradeServiceFactory.getOverview = _getOverview;
    gradeServiceFactory.add = _add;
    gradeServiceFactory.update = _update;
    gradeServiceFactory.list = _list;
    return gradeServiceFactory;

}]);
