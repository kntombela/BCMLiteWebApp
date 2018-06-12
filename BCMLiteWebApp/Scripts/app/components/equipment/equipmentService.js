testApp.service("equipmentService", function ($http) {

    //Get all equipment
    this.getEquipment = function (processId) {
        return $http.get("/api/processes/" + processId + "/equipment");
    };
});