testApp.service("processService", function ($http) {

    //Get all processes
    this.getProcesses = function (departmentId) {
        return $http.get("/api/departments/" + departmentId + "/processes");
    }

    //Get process by id
    this.getProcessById = function (processId) {
        return $http.get("/api/processes/" + processId);
    }

});