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
        .when('/departments/edit/:departmentId', {
            templateUrl: function (params) { return '/departments/edit?departmentId=' + params.departmentId; },
            controller: 'departmentCtrl'
        })
        .when('/processes/create', {
            templateUrl: 'processes/create',
            controller: 'processCtrl'
        })
        .when('/processes/edit/:processId', {
            templateUrl: function (params) { return '/processes/edit?processId=' + params.processId; },
            controller: 'processCtrl'
        })
        .when('/processes/details/:processId', {
            templateUrl: function (params) { return '/processes/details?processId=' + params.processId; },
            controller: 'processCtrl'
        })
        .when('/routeTwo', {
            templateUrl: 'routesDemo/two'
        })
        .when('/routeThree', {
            templateUrl: 'routesDemo/three'
        });
});