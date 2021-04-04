'use strict';
app.controller('studentFormModalController', ['$scope', '$uibModalInstance',
    function ($scope, $uibModalInstance) {

    $scope.searchTerm = '';
    $scope.clearSearchTerm = function () {
        $scope.searchTerm = '';
    };

    $scope.closeSelectBox = () => {
        $("md-backdrop").trigger("click");
    };

    $scope.save = function () {
        $uibModalInstance.close($scope.student);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}]);