'use strict';
app.controller('studentController', ['$scope', 'studentService', 'HubConnector', '$uibModal', 'toaster', 'subjectService'
    , function ($scope, studentService, HubConnector, $uibModal, toaster, subjectService) {

        $scope.overviews = "";

        HubConnector.on('reloadStudent', function (data) {
            $scope.overviews = data.overviews;
        });


        $scope.getOverview = function () {
            $scope.loading = true;
            studentService.getOverview().then(function (response) {

                if (response.status == 200) {
                    $scope.overviews = response.data.overviews;
                    setSubjects();
                }

            },
            function (response) {
                console.log(response);
            });
        };

        function setSubjects() {          
            let subjects = $scope.subjectsList;
            Object.entries($scope.overviews).forEach(([key, value]) => {
                Object.entries(value.student.grades).forEach(([key, value]) => {
                    value.subjectName = subjects.filter(c => c.id == value.selectedSubject)[0].name;
                });
            });
        }

        $scope.listSubjects = function () {
            subjectService.list(0,0,0).then(function (response) {

                if (response.status == 200) {
                    $scope.subjectsList = response.data;
                    $scope.getOverview();
                }
            },
                function (response) {
                    console.log(response);
                });
        };

        $scope.addStudent = function (student) {
            $scope.loading = true;
            studentService.add(student)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Student saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Student not saved',
                            body: response.data.errors[0],
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }  
                });
        };

        $scope.updateStudent= function (student) {
            $scope.loading = true;
            studentService.update(student)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Student saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Student not saved',
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
            save: function (student) {
                $scope.student = student;

                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/App/Templates/studentFormModal.html',
                    controller: 'studentFormModalController',
                    size: 'lg',
                    scope: $scope
                });

                modalInstance.result.then(
                    function (student) {
                        if (student.id != null) {
                            $scope.updateStudent(student);
                        }
                        else {
                            $scope.addStudent(student);
                        }
                    },
                    function (event) {

                    });
            },
            delete: function (studentId) {
                if (confirm('Are you sure you want to delete this student?')) {
                    $scope.loading = true;
                    studentService.delete(studentId).then(
                        function (response) {
                            if(response.data.errors.length === 0) {
                                $scope.overviews = response.data.overviews;

                                toaster.pop({
                                    type: 'success',
                                    title: 'Success',
                                    body: 'Student removed',
                                    timeout: 3000,
                                    showCloseButton: true
                                });ading = false;
                            }
                            else {
                                toaster.pop({
                                    type: 'error',
                                    title: 'Student not removed',
                                    body: response.data.errors[0],
                                    timeout: 3000,
                                    showCloseButton: true
                                });
                                $scope.loading = false;
                            }
                        });
                }
            }
        }

        $scope.listSubjects();        

    }]);