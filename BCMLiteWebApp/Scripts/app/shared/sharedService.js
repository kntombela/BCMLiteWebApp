testApp.factory("sharedService", function ($rootScope) {
    var sharedService = {};

    sharedService.organisationId = null;
    sharedService.departmentId = null;
    sharedService.departmentPlanId = null;
    sharedService.userId = null;

    //Public methods that set IDs based on drop downs 
    sharedService.prepForPublish = function (organisationId) {
        this.organisationId = organisationId;
        this.setOrganisationID();
    };

    sharedService.setDepartmentId = function (departmentId) {
        this.departmentId = departmentId;
        this.onDepartmentIdSelected();
    }

    sharedService.setPlanOwner = function (userId) {
        this.userId = userId;
        this.onPlanOwnerSelected();
    }

    sharedService.setDepartmentPlanId = function (departmentPlanId) {
        this.departmentPlanId = departmentPlanId;
        //Only broadcast departmentPlanId if not = 0
        if (departmentPlanId) {
            this.onDepartmentPlanIdSet();
        }
    }

    //Private methods used to broadcast drop down selects for organisation and department
    sharedService.setOrganisationID = function () {
        $rootScope.$broadcast("organisationSelected");
    };

    sharedService.onDepartmentIdSelected = function () {
        $rootScope.$broadcast("departmentSelected");
    };

    sharedService.onPlanOwnerSelected = function () {
        $rootScope.$broadcast("planOwnerSelected");
    };

    sharedService.onDepartmentPlanIdSet = function () {
        $rootScope.$broadcast("departmentPlanIdSet");
    };

    return sharedService;
});