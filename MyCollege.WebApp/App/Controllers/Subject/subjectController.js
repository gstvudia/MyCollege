'use strict';
app.controller('subjectController', ['$scope', 'subjectService', 'HubConnector', '$uibModal', 'toaster'
    , function ($scope, subjectService, HubConnector, $uibModal, toaster) {

        $scope.overviews = "";

        HubConnector.on('reloadSubject', function (data) {
            $scope.overviews = data.overviews;
        });


        $scope.getOverview = function () {
            $scope.loading = true;
            subjectService.getOverview().then(function (response) {

                if (response.status == 200) {
                    $scope.overviews = response.data.overviews;
                }

            },
            function (response) {
                console.log(response);
            });
        };

        $scope.addSubject = function (subject) {
            $scope.loading = true;
            subjectService.add(subject)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Subject saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Subject not saved',
                            body: response.data.errors[0],
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                });
        };

        $scope.updateSubject= function (subject) {
            $scope.loading = true;
            subjectService.update(subject)
                .then(function (response) {
                    if (response.data.errors.length === 0) {
                        $scope.overviews = response.data.overviews;
                        toaster.pop({
                            type: 'success',
                            title: 'Success',
                            body: 'Subject saved',
                            timeout: 3000,
                            showCloseButton: true
                        });
                        $scope.loading = false;
                    }
                    else {
                        toaster.pop({
                            type: 'error',
                            title: 'Subject not saved',
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
            save: function (subject) {
                $scope.subject = subject;

                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: '/App/Templates/SubjectFormModal.html',
                    controller: 'subjectFormModalController',
                    size: 'lg',
                    scope: $scope,
                    backdrop: 'static'
                });

                modalInstance.result.then(
                    function (subject) {
                        if (subject.id != null) {
                            $scope.updatesubject(subject);
                        }
                        else {
                            $scope.addSubject(subject);
                        }
                    },
                    function (event) {

                    });
            },
            delete: function (subjectId) {
                if (confirm('Are you sure you want to delete this Subject?')) {
                    $scope.loading = true;
                    subjectService.delete(subjectId).then(
                        function (response) {

                            if (response.data.errors.length === 0) {
                                $scope.overviews = response.data.overviews;

                                toaster.pop({
                                    type: 'success',
                                    title: 'Success',
                                    body: 'Subject removed',
                                    timeout: 3000,
                                    showCloseButton: true
                                });
                                $scope.loading = false;
                            }
                            else {
                                toaster.pop({
                                    type: 'error',
                                    title: 'Subject not removed',
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