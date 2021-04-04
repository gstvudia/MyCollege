var app = angular.module('myCollege',
    ['ngAnimate', 'ngRoute', 'ngSanitize', 'ui.bootstrap', 'ngMaterial', 'toaster', 'plunker.currencyMask']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/', {
        templateUrl: '/App/Templates/Home.html',
        controller: 'courseController'
    });
    $routeProvider.when("/Home/Courses", {
        controller: "courseController",
        templateUrl: '/App/Templates/Course.html'
    });
    $routeProvider.when("/Home/Subjects", {
        controller: "subjectController",
        templateUrl: '/App/Templates/Subject.html'
    });
    $routeProvider.when("/Home/Students", {
        controller: "studentController",
        templateUrl: '/App/Templates/Student.html'
    });
    $routeProvider.when("/Home/Teachers", {
        controller: "teacherController",
        templateUrl: '/App/Templates/Teacher.html'
    });
    $routeProvider.when("/Home/Grades", {
        controller: "gradeController",
        templateUrl: '/App/Templates/Grade.html'
    });

    $routeProvider.otherwise({
        templateUrl: '/App/Templates/404.html'
    });


    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
    $locationProvider.hashPrefix('');
}]);


