'use strict';
app.factory('HubConnector', ['$rootScope', function ($rootScope) {   

    return {
        on: function (eventName, callBack) {
            var connection = $.hubConnection();
            var hubProxy = connection.createHubProxy('ClientHub');

            hubProxy.on(eventName, function (data) {
                var args = arguments;

                $rootScope.$apply(function () {
                    callBack.apply(hubProxy, args);
                })
            });

            connection.start().done(function () { });
        }
    };
}]);