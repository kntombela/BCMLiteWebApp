testApp.service("organogramService", function ($http) {

    //Get all departments
    this.getDepartments = function (organisationId) {
        return $http.get("/api/organisations/" + organisationId + "/departments");
    }

    //Get department by id
    this.getDepartmentById = function (departmentId) {
        return $http.get("/api/departments/" + departmentId);
    }

    //Add/Edit department
    this.addEditDepartment = function (department) {
        return $http({
            method: "post",
            url: "/api/departments",
            data: department
        });
    }

    //Delete department
    this.deleteDepartment = function (departmentId) {
        return $http({
            method: "post",
            url: "/api/departments/" + departmentId,
            data: "{ id:" + departmentId + " }",
        });
    }

    //Delete departments
    this.deleteMultipleDepartments = function (ids) {
        return $http({
            method: "post",
            url: "/api/departments/delete",
            data: ids,
        });
    }

});