testApp.factory("sharedService", function ($rootScope) {
    var sharedService = {};

    sharedService.organisationId = null;

    sharedService.prepForPublish = function (organisationId) {
        this.organisationId = organisationId;
        this.setOrganisationID();
    };

    sharedService.setOrganisationID = function () {
        $rootScope.$broadcast("organisationSelected");
    };

    return sharedService;
});