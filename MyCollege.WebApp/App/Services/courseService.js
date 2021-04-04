'use strict';
app.factory('courseService', ['$http', '$q', function ($http, $q) {

    var courseServiceFactory = {};


    var _getOverview = function () {

        var deferred = $q.defer();
        $http.get('api/Course/Overview').then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
                deferred.reject(err);
        });

        return deferred.promise;
    };

    var _add = function (data) {

        var deferred = $q.defer();
        $http.post('api/Course/Add', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (data) {

        var deferred = $q.defer();
        $http.put('api/Course/Update', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (courseId) {

        var deferred = $q.defer();
        $http.delete('api/Course/Delete?courseId=' + courseId).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    courseServiceFactory.getOverview = _getOverview;
    courseServiceFactory.add = _add;
    courseServiceFactory.update = _update;
    courseServiceFactory.delete = _delete;
    return courseServiceFactory;

}]);
