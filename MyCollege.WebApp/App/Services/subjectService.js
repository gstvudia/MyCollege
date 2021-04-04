'use strict';
app.factory('subjectService', ['$http', '$q', function ($http, $q) {

    var subjectServiceFactory = {};


    var _getOverview = function () {

        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/Subject/Overview'
        }).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _list = function (courseId, teacherId, studentId) {

        var deferred = $q.defer();
        $http.get('api/Subject/List/?courseId=' + courseId + '&teacherId=' + teacherId + '&studentId=' + studentId)
        .then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _add = function (data) {

        var deferred = $q.defer();
        $http.post('api/Subject/Add', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (data) {

        var deferred = $q.defer();
        $http.put('api/Subject/Update', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (subjectId) {

        var deferred = $q.defer();
        $http.delete('api/Subject/Delete?subjectId=' + subjectId).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    subjectServiceFactory.getOverview = _getOverview;
    subjectServiceFactory.add = _add;
    subjectServiceFactory.update = _update;
    subjectServiceFactory.delete = _delete;
    subjectServiceFactory.list = _list;
    return subjectServiceFactory;

}]);
