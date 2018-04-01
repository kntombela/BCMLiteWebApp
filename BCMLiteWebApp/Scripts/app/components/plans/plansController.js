//app.controller('plansCtrl', function ($scope, $q, $window, plansService) {

//    //Variables
//    $scope.plans = [];
//    $scope.title = "";
//    $scope.names = ["Emil", "Tobias", "Linus"];

//    //Get plans available to the selected organisation
//    $scope.getPlans = function (organisationId) {
//        plansService.getOrganisationPlans(organisationId).then(function (response) {
//            $scope.plans = response.data;
//        }, (error) => {
//            $scope.title = "Oops... something went wrong";
//        });
//    };

//    ////Get list of available organisations
//    //$scope.populateOrganisationDropdown = function () {
//    //    plansService.getOrganisationDropDown().then(function (response) {
//    //        $scope.organisations = response.data;
//    //    }, (error) => {
//    //        $scope.title = "Oops... something went wrong";
//    //    });
//    //};

//    //$scope.populateOrganisationDropdown();

//});

app.controller('plansCtrl', function ($scope, $http, sharedService) {

    //Variables
    $scope.organisationId = 1;
    $scope.plans = [];
    $scope.selectedRow = null;


    //Get list of organisations to populate drop down
    $http.get("/api/organisations").then(function (response) {
        $scope.organisations = response.data;
    });

        //Get plans available to the selected organisation
    //$scope.getPlans = function () {
    //    //Get organisation from session storage set in dashboard dropdown select
    //    var organisationId = sessionStorage.getItem("organisation");
    //    //Get department plans from database
    //    $http.get("/api/organisations/" + organisationId + "/plans").then(function (response) {
    //        $scope.plans = response.data;
    //    });
    //};

    function getPlans(orgId) {
        //Get department plans from database
        $http.get("/api/organisations/" + orgId + "/plans").then(function (response) {
            $scope.plans = response.data;
        });
    }


    $scope.setClickedRow = function (index, departmentPlanId) { 
        $scope.selectedRow = index;
        $scope.departmentPlanId = departmentPlanId;
        showCrudActions(true);
    }

    function showCrudActions(isShown) {
        //toggle between show and hide actions
        if (isShown) {
            $scope.showActions = true;
            $scope.showAdd = false;
        } else {
            $scope.showActions = false;
            $scope.showAdd = true;
        }
    }

    $scope.$on('handlePublish', function () {
        getPlans(sharedService.organisationId);
    });

});