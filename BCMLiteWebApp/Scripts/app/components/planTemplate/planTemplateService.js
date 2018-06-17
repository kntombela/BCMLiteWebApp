testApp.service("planTemplateService", function ($http) {

    //Get Template Plans
    this.getPlanTemplates = function () {
        return $http.get("/api/planTemplates");
    };

    //Get plan template by Id
    this.getPlanTemplateById = function (templatePlanId) {
        return $http.get("/api/planTemplates/" + templatePlanId + "/details");
    };
});  