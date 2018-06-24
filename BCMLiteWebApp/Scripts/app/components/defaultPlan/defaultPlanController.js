testApp.controller('defaultPlanCtrl', function ($scope, $rootScope, $http, $routeParams, defaultPlanService, navService, sharedService) {

    //Variables
    $scope.pageTitle = 'Default Plans';
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.defaultPlans = [];
    $scope.newRecords = [];
    $scope.defaultPlan = {
        planID: -1,
        abbreviation: '',
        name: '',
        description: '',
        type: ''
    };

    //Get template plan list
    getDefaultPlanList();
    

    $scope.onDefaultPlanSelected = function (planId) {
        $scope.planId = planId;
    }

    ////Get plan template on edit and add load
    if ($routeParams.planId) {
        getDefaultPlan($routeParams.planId);
    }

    /**********************************HELPERS***************************************/
    //Get template plan list
    function getDefaultPlanList() {
        defaultPlanService.getDefaultPlans().then(function (response) {
            $scope.defaultPlans = response.data;
            if (!$scope.defaultPlans.length) {
                $scope.recordsError = "No default plans created yet, click 'New' to create new default plan.";
            }
            else {
                $scope.recordsError = "";
            }
        }).finally(function () {
            //Close loader when data has been loaded
            $scope.showLoader = false;
        });
    }

    //Get plan
    function getDefaultPlan(id) {
        defaultPlanService.getDefaultPlanById(id).then(function (response) {
            $scope.defaultPlan = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

    /**********************************CRUD ACTIONS**********************************/
    
});