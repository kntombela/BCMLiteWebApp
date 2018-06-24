testApp.service("defaultPlanService", function ($http) {

    //Get Default Plans
    this.getDefaultPlans = function () {
        return $http.get("/api/defaultPlans");
    };

    //Get default plan by Id
    this.getDefaultPlanById = function (planId) {
        return $http.get("/api/defaultPlans/" + planId + "/details");
    };

    //Add/Edit default plan
    this.addEditDefaultPlan = function (plan) {
        return $http({
            method: "post",
            url: "/api/defaultPlans",
            data: plan
        });
    };

    //Delete default plan
    this.deleteDefaultPlan = function (ids) {
        return $http({
            method: "post",
            url: "/api/defaultPlans/delete",
            data: ids
        });
    };
    
});  