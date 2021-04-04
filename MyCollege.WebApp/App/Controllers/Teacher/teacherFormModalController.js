'use strict';
app.controller('teacherFormModalController', ['$scope', '$uibModalInstance',
    function ($scope, $uibModalInstance) {

    $scope.searchTerm = '';
    $scope.clearSearchTerm = function () {
        $scope.searchTerm = '';
    };

    $scope.closeSelectBox = () => {
        $("md-backdrop").trigger("click");
    };

    $scope.save = function () {
        $uibModalInstance.close($scope.teacher);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

}]);