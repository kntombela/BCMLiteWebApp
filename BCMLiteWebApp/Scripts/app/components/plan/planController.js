testApp.controller('planCtrl', function ($scope, $rootScope, $http, $routeParams, planService, navService, sharedService) {

    //Variables
    $scope.pageTitle = 'Plans';
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.plans = [];
    $scope.newRecords = [];
    $scope.plan = {
        id: -1,
        name: '',
        description: '',
        type: '',
        invoked: '',
        departmentName: '',
        departmentID: ''
    };
    $scope.selected = true;

    //Get plan list
    $scope.getPlanList = function () {
        getPlanList();
    }

    //Handle organisation dropdown select event 
    $scope.$on('organisationSelected', function () {
        //Show loader
        $scope.showLoader = true;
        getPlanList();
    });

    /**********************************HELPERS***************************************/
    //Get plan list
    function getPlanList() {
        if (sharedService.organisationId != null) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get applications
            planService.getOrganisationPlans(sharedService.organisationId).then(function (response) {
                $scope.plans = response.data;
                if (!$scope.plans.length) {
                    $scope.recordsError = "No plans created yet, click 'New' to begin.";
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
   
    /**********************************CRUD ACTIONS**********************************/
    
});