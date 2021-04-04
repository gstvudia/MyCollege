'use strict';
app.controller('courseController', ['$scope', 'courseService', 'HubConnector', '$uibModal', 'toaster'
    , function ($scope, courseService, HubConnector, $uibModal, toaster) {

    $scope.overviews = "";
    $scope.loading = false;
        HubConnector.on('reloadCourse', function (data) {
            $scope.overviews = data.overviews;
        });

    $scope.getOverview = function () {
        $scope.loading = true;
        courseService.getOverview().then(function (response) {
            if (response.status == 200) {
                $scope.overviews = response.data.overviews;
                $scope.loading = false;
            }          

        },
        function (response) {
            console.log(response);
        });
    };

    $scope.addCourse = function (course) {
        $scope.loading = true;
        courseService.add(course)
            .then(function (response) {   
                if(response.data.errors.length === 0) {
                    $scope.overviews = response.data.overviews;
                    toaster.pop({
                        type: 'success',
                        title: 'Success',
                        body: 'Course saved',
                        timeout: 3000,
                        showCloseButton: true
                    });
                    $scope.loading = false;
                }
                else {
                    toaster.pop({
                        type: 'error',
                        title: 'Course not saved',
                        body: response.data.errors[0],
                        timeout: 3000,
                        showCloseButton: true
                    });
                    $scope.loading = false;
                }                 
            });
    };

    $scope.updateCourse = function (course) {
        $scope.loading = true;
        courseService.update(course)
            .then(function (response) {
                if (response.data.errors.length === 0) {
                    $scope.overviews = response.data.overviews;
                    toaster.pop({
                        type: 'success',
                        title: 'Success',
                        body: 'Course saved',
                        timeout: 3000,
                        showCloseButton: true
                    });
                    $scope.loading = false;
                }
                else {
                    toaster.pop({
                        type: 'error',
                        title: 'Course not saved',
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
        save: function (course) {
            $scope.course = course;

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/App/Templates/CourseFormModal.html',
                controller: 'courseFormModalController',
                size: 'lg',
                scope: $scope,
                backdrop: 'static'
            });

            modalInstance.result.then(
                function (course) {
                    if (course.id != null) {
                        $scope.updateCourse(course);
                    }
                    else {
                        $scope.addCourse(course);                        
                    }
                },
                function (event) {

                });
        },
        delete: function (courseId) {
            if (confirm('Are you sure you want to delete this Course?')) {
                $scope.loading = true;
                courseService.delete(courseId).then(
                    function (response) {
                        if (response.data.errors.length === 0) {
                            $scope.overviews = response.data.overviews;
                            toaster.pop({
                                type: 'success',
                                title: 'Success',
                                body: 'Course removed',
                                timeout: 3000,
                                showCloseButton: true
                            });
                            $scope.loading = false;
                        }
                        else {
                            toaster.pop({
                                type: 'error',
                                title: 'Course not removed',
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