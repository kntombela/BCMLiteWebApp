testApp.controller('equipmentCtrl', function ($scope, $rootScope, $http, $routeParams, equipmentService, navService) {

    //Variables
    $scope.equipments = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.equipment = {
        equipmentID: '',
        description: '',
        rto: '',
        processID: ''
    };

    //Get equipment list
    $scope.getEquipmentList = function () {
        getEquipmentList();
    }

    /**********************************HELPERS***************************************/
    //Get equipment list
    function getEquipmentList() {
        if ($routeParams.processId) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get equipments
            equipmentService.getEquipment($routeParams.processId).then(function (response) {
                $scope.equipments = response.data;
                if (!$scope.equipments.length) {
                    $scope.recordsError = "No equipment added yet, click 'New' to begin.";
                }
                else {
                    $scope.recordsError = "";
                }
            }).finally(function () {
                //Close loader when data has been loaded
                $scope.showLoader = false;
            });
        }
    }
});
