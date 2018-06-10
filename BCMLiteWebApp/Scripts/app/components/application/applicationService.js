testApp.service("applicationService", function ($http) {

    //Get all processes
    this.getApplications = function (processId) {
        return $http.get("/api/processes/" + processId + "/applications");
    };
});