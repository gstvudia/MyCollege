'use strict';
app.controller('teacherController', ['$scope', 'teacherService', 'HubConnector', '$uibModal', 'toaster', 'subjectService'
    , function ($scope, teacherService, HubConnector, $uibModal, toaster, subjectService) {

        $scope.overviews = "";

        HubConnector.on('reloadTeacher', function (data) {
            $scope.overviews = data.overviews;
        });


        $scope.getOverview = function () {
            $scope.loading = true;
            teacherService.getOverview().then(function (response) {

                if (response.data.errors.length === 0) {
                    $scope.overviews = response.data.overviews;
                }

            },
            function (response) {
                console.log(response);
            });
        };

        $scope.addteacher = function (teacher) {
            $scope.loading = true;
            teacherService.add(teacher)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Teacher saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Teacher not saved',
                            body: response.data.errors[0],
                            timeout: 3000,
                            showCloseButton: true
                        });
                    } 
                });
        };

        $scope.updateteacher= function (teacher) {
            $scope.loading = true;
            teacherService.update(teacher)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'teacher saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Teacher not saved',
                            body: response.data.errors[0],
                            timeout: 3000,
                            showCloseButton: true
                        });
                    }

                });
        };


        //Modal
        $scope.Modals = {
            save: function (teacher) {
                $scope.teacher = teacher;

                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/App/Templates/teacherFormModal.html',
                    controller: 'teacherFormModalController',
                    size: 'lg',
                    scope: $scope
                });

                modalInstance.result.then(
                    function (teacher) {
                        if (teacher.id != null) {
                            $scope.updateteacher(teacher);
                        }
                        else {
                            $scope.addteacher(teacher);
                        }
                    },
                    function (event) {

                    });
            },
            delete: function (teacherId) {
                if (confirm('Are you sure you want to delete this teacher?')) {
                    $scope.loading = true;
                    teacherService.delete(teacherId).then(
                        function (response) {
                            if (response.data.errors.length === 0) {
                                $scope.overviews = response.data.overviews;
                                $scope.loading = false;
                                toaster.pop({
                                    type: 'success',
                                    title: 'Success',
                                    body: 'Teacher removed',
                                    timeout: 3000,
                                    showCloseButton: true
                                });
                            }
                            else {
                                toaster.pop({
                                    type: 'error',
                                    title: 'Teacher not removed',
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

        $scope.getOverview();       

    }]);