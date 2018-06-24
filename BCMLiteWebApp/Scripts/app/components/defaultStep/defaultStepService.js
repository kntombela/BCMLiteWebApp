testApp.service("defaultStepService", function ($http) {

    //Get Default Steps
    this.getDefaultSteps = function (planId) {
        return $http.get("/api/defaultPlans/" + planId + "/defaultSteps");
    };

    //Get default step by Id
    this.getDefaultStepById = function (stepId) {
        return $http.get("/api/defaultSteps/" + stepId + "/details");
    };

    //Add/Edit default step
    this.addEditDefaultStep = function (plan) {
        return $http({
            method: "post",
            url: "/api/defaultSteps",
            data: plan
        });
    };

    //Delete default step
    this.deleteDefaultStep = function (ids) {
        return $http({
            method: "post",
            url: "/api/defaultSteps/delete",
            data: ids
        });
    };

    //Import steps
    this.importSteps = function (steps) {
        return $http({
            method: "post",
            url: "/api/defaultSteps/import",
            data: steps
        });
    };
    
});  