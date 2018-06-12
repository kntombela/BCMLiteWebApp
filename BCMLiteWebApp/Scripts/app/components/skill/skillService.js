testApp.service("skillService", function ($http) {

    //Get all skills
    this.getSkills = function (processId) {
        return $http.get("/api/processes/" + processId + "/skills");
    };
});