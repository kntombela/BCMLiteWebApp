testApp.config(function ($routeProvider, $locationProvider) {

    $locationProvider.hashPrefix('');

    $routeProvider.
        when('/departments', {
            templateUrl: 'departments/index',
            controller: 'organogramCtrl'
        })
        .when('/departments/create/:organisationId', {
            templateUrl: function (params) { return '/departments/create?organisationId=' + params.organisationId; },
            controller: 'organogramCtrl'
        })
        .when('/departments/edit/:id', {
            templateUrl: function (params) { return '/departments/edit?id=' + params.id; },
            controller: 'organogramCtrl'
        })
        .when('/departments/delete/:id', {
            templateUrl: function (params) { return '/departments/delete?id=' + params.id; },
            controller: 'organogramCtrl'
        })
        .when('/routeTwo', {
            templateUrl: 'routesDemo/two'
        })
        .when('/routeThree', {
            templateUrl: 'routesDemo/three'
        });
});