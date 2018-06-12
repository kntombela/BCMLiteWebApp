testApp.service("thirdPartyService", function ($http) {

    //Get all thirdParties
    this.getThirdParties = function (processId) {
        return $http.get("/api/processes/" + processId + "/thirdParties");
    };
});