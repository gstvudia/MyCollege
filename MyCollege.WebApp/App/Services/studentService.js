'use strict';
app.factory('studentService', ['$http', '$q', function ($http, $q) {

    var studentServiceFactory = {};


    var _getOverview = function () {

        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: 'api/Student/Overview'
        }).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _list = function (courseId, teacherid) {

        var deferred = $q.defer();
        $http.get('api/Student/List/?courseId=' + courseId + '&teacherid=' + teacherid)
        .then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(response) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _add = function (data) {

        var deferred = $q.defer();
        $http.post('api/Student/Add', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (data) {

        var deferred = $q.defer();
        $http.put('api/Student/Update', data).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (studentId) {

        var deferred = $q.defer();
        $http.delete('api/Student/Delete?studentId=' + studentId).then(function successCallback(response) {
            deferred.resolve(response);
        }, function errorCallback(err) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    studentServiceFactory.getOverview = _getOverview;
    studentServiceFactory.add = _add;
    studentServiceFactory.update = _update;
    studentServiceFactory.delete = _delete;
    studentServiceFactory.list = _list;
    return studentServiceFactory;

}]);
