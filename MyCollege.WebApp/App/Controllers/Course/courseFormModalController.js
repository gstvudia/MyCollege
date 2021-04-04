'use strict';
app.controller('courseFormModalController', ['$scope', '$uibModalInstance', 'subjectService',
    function ($scope, $uibModalInstance, subjectService) {

    $scope.subjectsList = [];
    $scope.searchTerm = '';
    $scope.clearSearchTerm = function () {
        $scope.searchTerm = '';
    };



    $scope.listSubjects = function () {
        var courseId = null;
        if ($scope.course) { courseId = $scope.course.id };
        subjectService.list(courseId).then(function (response) {

            if (response.status == 200) {
                $scope.subjectsList = response.data;
                if ($scope.course && $scope.course.subjects) {
                    setSelectedCombo();
                }                
            }
        },
        function (response) {
            console.log(response);
        });
    };

    function setSelectedCombo() {
        var ids = $scope.subjectsList.map(x => x.id);
        Object.entries($scope.$parent.course.subjects).forEach(([key, value]) => {
            if (ids.includes(value.id)) {
                $scope.subjectsList.filter(x => x.id == value.id)[0].isSelected = true;
            }
        });
    }


     $scope.closeSelectBox = () => {
         $("md-backdrop").trigger("click");
     };

    $scope.save = function () {
        $uibModalInstance.close($scope.course);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };



    $scope.listSubjects();
}]);