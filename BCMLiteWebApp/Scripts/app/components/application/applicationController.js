testApp.controller('applicationCtrl', function ($scope, $rootScope, $http, $routeParams, applicationService, navService) {

    //Variables
    $scope.applications = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.application = {
        applicationID: 11,
        name: '',
        description: '',
        rto: '',
        rpo: '',
        processID: ''
    };


    //Get application list
    $scope.getApplicationList = function () {
        getApplicationList();
    }

    /**********************************HELPERS***************************************/
    //Get application list
    function getApplicationList() {
        if ($routeParams.processId) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get applications
            applicationService.getApplications($routeParams.processId).then(function (response) {
                $scope.applications = response.data;
                if (!$scope.applications.length) {
                    $scope.recordsError = "No applications added yet, click 'New' to begin.";
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
