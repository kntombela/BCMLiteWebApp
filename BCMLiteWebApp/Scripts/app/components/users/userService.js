testApp.service("userService", function ($http) {

    //Get all user by organisationId
    this.getUsers = function (organisationId) {
        return $http.get("/api/organisations/" + organisationId + "/users");
    };

    
});