testApp.service("processService", function ($http) {

    //Get all processes
    this.getProcesses = function (departmentId) {
        return $http.get("/api/departments/" + departmentId + "/processes");
    }

    //Get process by id
    this.getProcessById = function (processId) {
        return $http.get("/api/processes/" + processId);
    }

    //Add/Edit process
    this.addEditProcess = function (process) {
        return $http({
            method: "post",
            url: "/api/processes",
            data: department
        });
    }

    //Delete process
    this.deleteProcess = function (ids) {
        return $http({
            method: "post",
            url: "/api/processes/delete",
            data: ids,
        });
    }

});