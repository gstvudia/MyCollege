'use strict';
app.controller('gradeController', ['$scope', 'gradeService', 'HubConnector', '$uibModal', 'toaster', 'subjectService', 'studentService'
    , function ($scope, gradeService, HubConnector, $uibModal, toaster, subjectService, studentService) {

        $scope.overviews = "";

        HubConnector.on('reloadGrade', function (data) {
            $scope.overviews = data.overviews;
        });


        $scope.getOverview = function () {
            $scope.loading = true;
            gradeService.getOverview().then(function (response) {

                if (response.status == 200) {
                    $scope.overviews = response.data.overviews;
                    setSubjects();
                    setStudents();
                }

            },
            function (response) {
                console.log(response);
            });
        };

        function setSubjects() {          
            let subjects = $scope.subjectsList;
            Object.entries($scope.overviews).forEach(([key, value]) => {
                value.grade.subjectName = subjects.filter(c => c.id == value.grade.selectedSubject)[0].name;
            });
        }

        function setStudents() {
            let students = $scope.studentsList;
            Object.entries($scope.overviews).forEach(([key, value]) => {
                value.grade.studentName = students.filter(c => c.id == value.grade.selectedStudent)[0].name;
            });
        }

        $scope.listSubjects = function () {
            subjectService.list(0,0,0).then(function (response) {

                if (response.status == 200) {
                    $scope.subjectsList = response.data;
                    $scope.listStudents(); 
                }
            },
                function (response) {
                    console.log(response);
                });
        };

        $scope.listStudents = function () {
            studentService.list(0, 0, 0).then(function (response) {

                if (response.status == 200) {
                    $scope.studentsList = response.data;
                    $scope.getOverview();
                }
            },
                function (response) {
                    console.log(response);
                });
        };

        $scope.addGrade = function (grade) {
            $scope.loading = true;
            gradeService.add(grade)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Grade saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Grade not saved',
                            body: response.data.errors[0],
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }  
                });
        };

        $scope.updateGrade= function (grade) {
            $scope.loading = true;
            gradeService.update(grade)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Grade saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Grade not saved',
                            body: response.data.errors[0],
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                });
        };


        //Modal
        $scope.Modals = {
            save: function (grade) {
                $scope.grade = grade;

                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/App/Templates/GradeFormModal.html',
                    controller: 'gradeFormModalController',
                    size: 'lg',
                    scope: $scope
                });

                modalInstance.result.then(
                    function (grade) {
                        if (grade.id != null) {
                            $scope.updateGrade(grade);
                        }
                        else {
                            $scope.addGrade(grade);
                        }
                    },
                    function (event) {

                    });
            }
        }

        $scope.listSubjects();       
        
    }]);