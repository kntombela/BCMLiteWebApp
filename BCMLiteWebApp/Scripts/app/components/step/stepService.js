﻿testApp.service("stepService", function ($http) {

    //Get Steps
    this.getSteps = function (planId) {
        return $http.get("/api/departmentPlans/" + planId + "/steps");
    };

    //Get step by Id
    this.getStepById = function (stepId) {
        return $http.get("/api/steps/" + stepId + "/details");
    };

    //Add/Edit step
    this.addEditStep = function (step) {
        return $http({
            method: "post",
            url: "/api/steps",
            data: step
        });
    };

    this.copyDefaultSteps = function (planId, departmentPlanId) {
        return $http({
            method: "post",
            url: "/api/steps/copyDefaultSteps/" + planId + "/" + departmentPlanId
        });
    };

    //Delete step
    this.deleteStep = function (ids) {
        return $http({
            method: "post",
            url: "/api/steps/delete",
            data: ids
        });
    };

    //Import steps
    this.importSteps = function (steps) {
        return $http({
            method: "post",
            url: "/api/steps/import",
            data: steps
        });
    };
    
});  