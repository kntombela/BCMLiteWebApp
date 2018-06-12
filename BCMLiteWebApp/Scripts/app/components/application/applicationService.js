testApp.service("applicationService", function ($http) {

    //Get all applications
    this.getApplications = function (processId) {
        return $http.get("/api/processes/" + processId + "/applications");
    };

    //Get application by id
    this.getApplicationById = function (applicationId) {
        return $http.get("/api/applications/" + applicationId + "/details");
    };

    //Add/Edit application
    this.addEditApplication = function (application) {
        return $http({
            method: "post",
            url: "/api/applications",
            data: application
        });
    };

    //Delete applications
    this.deleteApplications = function (ids) {
        return $http({
            method: "post",
            url: "/api/applications/delete",
            data: ids
        });
    };

    //Import application
    //this.importProcesses = function (processes) {
    //    return $http({
    //        method: "post",
    //        url: "/api/processes/import",
    //        data: processes
    //    });
    //};

});