testApp.service("equipmentService", function ($http) {

    //Get all equipment
    this.getEquipments = function (processId) {
        return $http.get("/api/processes/" + processId + "/equipments");
    };

    //Get equipment by id
    this.getEquipmentById = function (equipmentId) {
        return $http.get("/api/equipments/" + equipmentId + "/details");
    };

    //Add/Edit equipment
    this.addEditEquipment = function (equipment) {
        return $http({
            method: "post",
            url: "/api/equipments",
            data: equipment
        });
    };

    //Delete equipments
    this.deleteEquipments = function (ids) {
        return $http({
            method: "post",
            url: "/api/equipments/delete",
            data: ids
        });
    };

    //Import equipments
    this.importEquipments = function (equipments) {
        return $http({
            method: "post",
            url: "/api/equipments/import",
            data: equipments
        });
    };
});