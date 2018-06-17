testApp.controller('planTemplateCtrl', function ($scope, $rootScope, $http, $routeParams, planTemplateService, navService, sharedService) {

    //Variables
    $scope.pageTitle = 'Plan Templates';
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.planTemplates = [];
    $scope.newRecords = [];
    $scope.planTemplate = {
        planID: -1,
        planAbbreviation: '',
        name: '',
        description: '',
        type: ''
    };

    //Get template plan list
    $scope.getPlanTemplateList = function () {
        getPlanTemplateList();
    }

    $scope.onPlanTemplateSelected = function (planId) {
        $scope.planTemplateId = planId;
    }

    ////Get plan template on edit and add load
    if ($routeParams.planId) {
        getPlanTemplate($routeParams.planId);
    }

    /**********************************HELPERS***************************************/
    //Get template plan list
    function getPlanTemplateList() {
        planTemplateService.getPlanTemplates().then(function (response) {
            $scope.templatePlans = response.data;
            if (!$scope.templatePlans.length) {
                $scope.recordsError = "No templates created yet, click 'New' to create new plan template.";
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
    function getPlanTemplate(id) {
        planTemplateService.getPlanTemplateById(id).then(function (response) {
            $scope.planTemplate = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

    /**********************************CRUD ACTIONS**********************************/
    
});