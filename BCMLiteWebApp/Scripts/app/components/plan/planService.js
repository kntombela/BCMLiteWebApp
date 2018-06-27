testApp.service("planService", function ($http) {

    //Get Organisation Department Plans
    this.getOrganisationPlans = function (organisationId) {
        return $http.get("/api/organisations/" + organisationId + "/departmentPlans");
    };

    //Get department plan by id
    this.getPlanById = function (planId) {
        return $http.get("/api/departmentPlans/" + planId + "/details");
    };

    //Add/Edit department plan
    this.addEditPlan = function (plan) {
        return $http({
            method: "post",
            url: "/api/departmentPlans",
            data: plan
        });
    };

    //Delete department plans
    this.deletePlans = function (ids) {
        return $http({
            method: "post",
            url: "/api/departmentPlans/delete",
            data: ids
        });
    };

    //Create new plan from default plan
    this.addPlanFromDefaultPlan = function (defaultPlan) {
        return $http({
            method: "post",
            url: "/api/plans",
            data: defaultPlan
        });
    };

});  