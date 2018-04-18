testApp.service("departmentService", function ($http) {

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

    //Delete departments
    this.deleteDepartments = function (ids) {
        return $http({
            method: "post",
            url: "/api/departments/delete",
            data: ids,
        });
    }

});