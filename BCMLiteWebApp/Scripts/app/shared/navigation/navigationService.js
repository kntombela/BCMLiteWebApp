testApp.service("navService", function ($rootScope) {

    //****Navigation Methods*****
    $rootScope.redirectToDashboard = function () {
        window.location.href = '/#/dashboard';
    };

    $rootScope.redirectToProcessCreate = function () {
        sessionStorage.removeItem('processId');
        window.location.href = '/#/processes/create';
    };

    $rootScope.redirectToProcessEdit = function (id) {
        window.location.href = '/#/processes/edit/' + id;
        sessionStorage.processId = id;
    };

    $rootScope.redirectToDepartmentDetail = function () {
        window.location.href = '/#/departments/edit/' + sessionStorage.departmentId;
    };

    $rootScope.redirectToDepartmentIndex = function () {
        window.location.href = '/#/departments';
    };
});