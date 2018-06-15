testApp.service("thirdPartyService", function ($http) {

    //Get all thirdParties
    this.getThirdParties = function (processId) {
        return $http.get("/api/processes/" + processId + "/thirdParties");
    };

    //Get thirdParty by id
    this.getThirdPartyById = function (thirdPartyId) {
        return $http.get("/api/thirdparties/" + thirdPartyId + "/details");
    };

    //Add/Edit thirdParty
    this.addEditThirdParty = function (thirdParty) {
        return $http({
            method: "post",
            url: "/api/thirdparties",
            data: thirdParty
        });
    };

    //Delete thirdparties
    this.deleteThirdParties = function (ids) {
        return $http({
            method: "post",
            url: "/api/thirdparties/delete",
            data: ids
        });
    };

    //Import thirdparties
    this.importThirdParties = function (thirdparties) {
        return $http({
            method: "post",
            url: "/api/thirdparties/import",
            data: thirdparties
        });
    };
});