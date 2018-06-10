testApp.service("navService", function ($rootScope) {

    //****Navigation Methods*****
    $rootScope.redirectToDashboard = function () {
        window.location.href = '/#/dashboard';
    };

    $rootScope.redirectToProcessCreate = function () {
        sessionStorage.removeItem('processId');
        window.location.href = '/#/processes/create';
    };

    $rootScope.redirectToProcessEdit = function (processId) {
        window.location.href = '/#/processes/edit/' + processId;
        sessionStorage.processId = id;
    };

    $rootScope.redirectToProcessDetails = function (processId) {
        window.location.href = '/#/processes/details/' + processId;
        sessionStorage.processId = processId;
    };

    $rootScope.redirectToDepartmentDetail = function () {
        window.location.href = '/#/departments/edit/' + sessionStorage.departmentId;
    };

    $rootScope.redirectToDepartmentIndex = function () {
        window.location.href = '/#/departments';
    };
});