testApp.config(function ($routeProvider, $locationProvider) {

    $locationProvider.hashPrefix('');

    $routeProvider.
        when('/departments', {
            templateUrl: 'departments/index',
            controller: 'departmentCtrl'
        })
        .when('/departments/create/:organisationId', {
            templateUrl: function (params) { return '/departments/create?organisationId=' + params.organisationId; },
            controller: 'departmentCtrl'
        })
        .when('/departments/edit/:id', {
            templateUrl: function (params) { return '/departments/edit?id=' + params.id; },
            controller: 'departmentCtrl'
        })
        .when('/processes/create', {
            templateUrl: 'processes/create',
            controller: 'processCtrl'
        })
        .when('/processes/edit/:id', {
            templateUrl: function (params) { return '/processes/edit?id=' + params.id; },
            controller: 'processCtrl'
        })
        .when('/routeTwo', {
            templateUrl: 'routesDemo/two'
        })
        .when('/routeThree', {
            templateUrl: 'routesDemo/three'
        });
});