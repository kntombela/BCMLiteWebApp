testApp.service("navService", function ($rootScope) {

    //****Navigation Methods*****
    $rootScope.redirectToProcessCreate = function () {
        window.location.href = '/#/processes/create';
    };

    $rootScope.redirectToDepartmentDetail = function () {
        window.location.href = '/#/departments/edit/' + sessionStorage.departmentId;
    };

});