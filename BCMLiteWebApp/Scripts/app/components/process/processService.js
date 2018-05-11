testApp.service("processService", function ($http) {

    //Get all processes
    this.getProcesses = function (departmentId) {
        return $http.get("/api/departments/" + departmentId + "/processes");
    };

    //Get process by id
    this.getProcessById = function (processId) {
        return $http.get("/api/processes/" + processId + "/details");
    };

    //Add/Edit process
    this.addEditProcess = function (process) {
        return $http({
            method: "post",
            url: "/api/processes",
            data: process
        });
    };

    //Delete process
    this.deleteProcesses = function (ids) {
        return $http({
            method: "post",
            url: "/api/processes/delete",
            data: ids
        });
    };

    //Import processes
    this.importProcesses = function (processes) {
        return $http({
            method: "post",
            url: "/api/processes/import",
            data: processes
        });
    };

});