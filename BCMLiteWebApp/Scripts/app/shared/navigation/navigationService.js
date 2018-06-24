testApp.service("navService", function ($rootScope) {

    //****Navigation Methods*****
    $rootScope.redirectToDashboard = function () {
        window.location.href = '/#/dashboard';
    };

    //Process Routes
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

    //Department Routes
    $rootScope.redirectToDepartmentDetail = function () {
        window.location.href = '/#/departments/edit/' + sessionStorage.departmentId;
    };

    $rootScope.redirectToDepartmentIndex = function () {
        window.location.href = '/#/departments';
    };

    //Application Routes
    $rootScope.redirectToApplicationsIndex = function (processId) {
        window.location.href = '/#/applications/index/' + processId;
    };

    //Skills Routes
    $rootScope.redirectToSkillsIndex = function (processId) {
        window.location.href = '/#/skills/index/' + processId;
    };

    //Documents Routes
    $rootScope.redirectToDocumentsIndex = function (processId) {
        window.location.href = '/#/documents/index/' + processId;
    };

    //Equipment Routes
    $rootScope.redirectToEquipmentsIndex = function (processId) {
        window.location.href = '/#/equipments/index/' + processId;
    };

    //Third Party Routes
    $rootScope.redirectToThirdPartiesIndex = function (processId) {
        window.location.href = '/#/thirdparties/index/' + processId;
    };

    //Plan Routes
    $rootScope.redirectToPlansCreate = function (planId) {
        window.location.href = '/#/plans/create/' + planId;
        sessionStorage.planId = planId;
        //Close popup window
        $('#addModal').modal('hide');
    };

    $rootScope.redirectToPlansIndex = function () {
        window.location.href = '/#/plans';
    };
});