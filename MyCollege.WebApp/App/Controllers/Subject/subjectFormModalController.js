'use strict';
app.controller('subjectFormModalController', ['$scope', '$uibModalInstance', 'teacherService',
    function ($scope, $uibModalInstance, teacherService) {

    $scope.subjectsList = [];
    $scope.searchTerm = '';
    $scope.clearSearchTerm = function () {
        $scope.searchTerm = '';
    };

    $scope.listTeachers = function () {
        teacherService.list().then(function (response) {

            if (response.status == 200) {
                $scope.teachersList = response.data;
                if ($scope.subject && $scope.subject.teacher) {
                    setSelectedCombo();
                }                
            }
        },
        function (response) {
            console.log(response);
        });
    };

    function setSelectedCombo() {
        Object.entries($scope.teachersList).forEach(([key, value]) => {
            if (value.id == $scope.subject.teacher.id) {
                $scope.teachersList.filter(x => x.id == value.id)[0].isSelected = true;
            }
        });
    }


     $scope.closeSelectBox = () => {
         $("md-backdrop").trigger("click");
     };

    $scope.save = function () {
        $uibModalInstance.close($scope.subject);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };



    $scope.listTeachers();
}]);