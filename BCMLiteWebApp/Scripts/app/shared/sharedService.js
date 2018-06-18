testApp.factory("sharedService", function ($rootScope) {
    var sharedService = {};

    sharedService.organisationId = null;
    sharedService.departmentId = null;

    //Public methods that set IDs based on drop downs 
    sharedService.prepForPublish = function (organisationId) {
        this.organisationId = organisationId;
        this.setOrganisationID();
    };

    sharedService.setDepartmentId = function (departmentId) {
        this.departmentId = departmentId;
        this.onDepartmentIdSelected();
    }

    //Private methods used to broadcast drop down selects for organisation and department
    sharedService.setOrganisationID = function () {
        $rootScope.$broadcast("organisationSelected");
    };

    sharedService.onDepartmentIdSelected = function () {
        $rootScope.$broadcast("departmentSelected");
    };

    return sharedService;
});