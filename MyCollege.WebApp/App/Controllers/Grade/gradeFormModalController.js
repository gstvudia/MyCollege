'use strict';
app.controller('gradeFormModalController', ['$scope', '$uibModalInstance',
    function ($scope, $uibModalInstance) {

    $scope.searchTerm = '';
    $scope.clearSearchTerm = function () {
        $scope.searchTerm = '';
    };

    $scope.closeSelectBox = () => {
        $("md-backdrop").trigger("click");
    };

    function setSelectedCombo() {

        Object.entries($scope.subjectsList).forEach(([key, value]) => {
            if (value.id === $scope.grade.selectedSubject) {
                value.isSelected = true;
            }
        });

        Object.entries($scope.studentsList).forEach(([key, value]) => {
            if (value.id === $scope.grade.selectedStudent) {
                value.isSelected = true;
            }
        });
    }


    $scope.closeSelectBox = () => {
        $("md-backdrop").trigger("click");
    };

    $scope.save = function () {
        $uibModalInstance.close($scope.grade);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    setSelectedCombo();
}]);