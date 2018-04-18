testApp.controller('processCtrl', function ($scope, $http, $routeParams, processService) {

    //Variables
    $scope.processes = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.process = {
        processID: '',
        name: '',
        rto: '',
        departmentID: ''
    };

    //Get processes
    $scope.getProcesses = function () {

        if ($routeParams.id) {
            processService.getProcesses($routeParams.id).then(function (response) {
                $scope.processes = response.data;
                if (!$scope.processes.length) {
                    $scope.recordsError = "No processes added yet, click 'New' to begin.";
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