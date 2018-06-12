testApp.controller('thirdPartyCtrl', function ($scope, $rootScope, $http, $routeParams, thirdPartyService, navService) {

    //Variables
    $scope.thirdParties = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.thirdParty = {
        thirdPartyID: '',
        description: '',
        rto: '',
        processID: ''
    };

    //Get thirdParty list
    $scope.getThirdPartyList = function () {
        getThirdPartyList();
    }

    /**********************************HELPERS***************************************/
    //Get thirdParty list
    function getThirdPartyList() {
        if ($routeParams.processId) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get thirdParties
            thirdPartyService.getThirdParties($routeParams.processId).then(function (response) {
                $scope.thirdParties = response.data;
                if (!$scope.thirdParties.length) {
                    $scope.recordsError = "No third parties added yet, click 'New' to begin.";
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
