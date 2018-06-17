testApp.service("planService", function ($http) {

    //Get Organisation Plans
    this.getOrganisationPlans = function (organisationId) {
        return $http.get("/api/organisations/" + organisationId + "/plans");
    };


});  