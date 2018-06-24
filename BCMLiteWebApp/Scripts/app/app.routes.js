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
        .when('/applications/index/:processId', {
            templateUrl: function (params) { return '/applications/index?processId=' + params.processId; },
            controller: 'applicationCtrl'
        })
        .when('/skills/index/:processId', {
            templateUrl: function (params) { return '/skills/index?processId=' + params.processId; },
            controller: 'skillCtrl'
        })
        .when('/thirdparties/index/:processId', {
            templateUrl: function (params) { return '/thirdparties/index?processId=' + params.processId; },
            controller: 'thirdPartyCtrl'
        })
        .when('/documents/index/:processId', {
            templateUrl: function (params) { return '/documents/index?processId=' + params.processId; },
            controller: 'documentCtrl'
        })
        .when('/equipments/index/:processId', {
            templateUrl: function (params) { return '/equipments/index?processId=' + params.processId; },
            controller: 'equipmentCtrl'
        })
        .when('/plans', {
            templateUrl: 'departmentPlans/index',
            controller: 'planCtrl'
        })
        .when('/plans/create/:planId', {
            templateUrl: function (params) { return '/departmentplans/create?planId=' + params.planId; },
            controller: 'planCtrl'
        })
        .when('/defaultPlans/edit/:planId', {
            templateUrl: function (params) { return '/defaultPlans/index?planId=' + params.planId; },
            controller: 'defaultPlanCtrl'
        })
        .when('/routeTwo', {
            templateUrl: 'routesDemo/two'
        })
        .when('/routeThree', {
            templateUrl: 'routesDemo/three'
        });
});