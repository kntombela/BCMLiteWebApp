testApp.service("planService", function ($http) {

    //Get Organisation Plans
    this.getOrganisationPlans = function (organisationId) {
        return $http.get("/api/organisations/" + organisationId + "/plans");
    };

    //Get plan by id
    this.getPlanById = function (planId) {
        return $http.get("/api/plans/" + planId + "/details");
    };

    //Add/Edit plan
    this.addEditPlan = function (plan) {
        return $http({
            method: "post",
            url: "/api/plans",
            data: plan
        });
    };

    //Delete plans
    this.deletePlans = function (ids) {
        return $http({
            method: "post",
            url: "/api/plans/delete",
            data: ids
        });
    };

});  