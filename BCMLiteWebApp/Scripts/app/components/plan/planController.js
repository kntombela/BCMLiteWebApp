testApp.controller('planCtrl', function ($scope, $rootScope, $http, $routeParams, planService, navService, sharedService) {

    //Variables
    $scope.pageTitle = 'Plans';
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.plans = [];
    $scope.newRecords = [];
    $scope.departmentPlan = {
        departmentPlanID: -1,
        departmentID: '',
        planID: '',
        departmentPlanInvoked: '',
    };

    //Get plan list on controller load
    getPlanList();

    //Handle organisation dropdown select event 
    $scope.$on('organisationSelected', function () {
        //Show loader
        $scope.showLoader = true;
        getPlanList();
    });

    $scope.$on('departmentSelected', function () {
        addPlan();
    });

    //Add new plan
    function addPlan () {
        if (sharedService.departmentId) {
            //Get department id attached to sharedService dropdown select
            $scope.departmentPlan.departmentID = sharedService.departmentId;
            //Get plan id from navigation route set when navigating to plans/create
            $scope.departmentPlan.planID = $routeParams.planId;
            //Call plan service to add new plan
            var requestResponse = planService.addEditPlan($scope.departmentPlan);
            //asynchronously add new plan to db using message helper
            Message(requestResponse);
        } else {
            alert("No department selected");
        }
    };

    //Edit Plan
    $scope.editPlan = function () {
        getPlan($scope.departmentPlanID);
    };

    //Delete plan
    $scope.deletePlan = function () {
        //Call plan service to delete plans
        var requestResponse = planService.deletePlans(getSelectedItems());
        //asynchronously delets plans from db using message helper
        Message(requestResponse);
        //Reset selected row
        resetRowSelect();
    };

    //Cancel plan creation
    $scope.cancelPlanCreate = function () {
        //Check if plan has been created by reading shared service departmentPlanId property
        if (sharedService.departmentPlanId) {
            //Call plan service to delete plans
            var requestResponse = planService.deletePlans(sharedService.departmentPlanId);
            //asynchronously delets plans from db using message helper
            Message(requestResponse);
        }
        
        //Return to department plan index
        $rootScope.redirectToPlansIndex();
    }

    /**********************************HELPERS***************************************/
    //Get plan
    function getPlan(id) {
        planService.getPlanById(id).then(function (response) {
            $scope.departmentPlan = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

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

    //Helper function to call api asynchronously
    function Message(requestResponse) {
        requestResponse.then(function successCallback(response) {
            //Refresh plan list
            getPlanList();
            //Show success message
            showMessageAlert(response.data.message)
            //Flag new rows
            if (response.data.ids) {
                //Set departmentPlanId using sharedService, 
                //this will broadcase the departmentPlanId when it is available due to the callback function
                sharedService.setDepartmentPlanId(response.data.ids);
            }

        }, function errorCallback(response) {
            //Show error message
            alert("Something went wrong, please try again or contact your administrator if the problem persists.");
        });
    }

    //Flag new rows
    $scope.isNewRow = function (id) {
        var flag = false
        if ($scope.newRecords) {
            flag = $scope.newRecords.includes(id);
        }
        return flag;
    }

    /**********************************CRUD ACTIONS**********************************/
    //Clear form before adding new process
    $scope.resetRowSelect = function () {
        resetRowSelect();
    }

    //Show CRUD actions when a row is selected
    $scope.onRowClicked = function (planId) {          
        $scope.planId = planId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[planId] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.plan = {};
        $scope.checkboxes = { 'checked': false, items: {} };
        showCrudActions(false);
    }

    //toggle between show and hide CRUD actions
    function showCrudActions(isShown) {
        $scope.showEdit = isShown;
        $scope.showDelete = isShown;
    }

    //Get number of selected items
    function getSelectedItems() {
        //Get list of checked items
        var checked = [];
        angular.forEach($scope.plans, function (item) {
            if ($scope.checkboxes.items[item.id]) {
                //Push all selected ids into array
                checked.push(item.id);
            }
        });

        return checked;
    }

    //Show success messages
    function showMessageAlert(msg) {
        $scope.message = msg;
        $scope.showMessageAlert = true;
    }

    //Close message alert after 3 seconds
    $scope.$watch('showMessageAlert', function (value) {
        //Hide message alert after 3 seconds
        setTimeout(function () { $scope.showMessageAlert = false; }, 3000);
    });

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }

    //watch for check all checkbox
    $scope.$watch('checkboxes.checked', function (value) {
        angular.forEach($scope.plans, function (item) {
            if (angular.isDefined(item.id)) {
                $scope.checkboxes.items[item.id] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.plans) {
            return;
        }
        //Count number of checked and unchecked checkboxes
        var checked = 0, unchecked = 0,
            total = $scope.plans.length;
        angular.forEach($scope.plans, function (item) {
            checked += ($scope.checkboxes.items[item.id]) || 0;
            unchecked += (!$scope.checkboxes.items[item.id]) || 0;

        });

        //IF multiple items have been selected do not show edit functionality      
        if (getSelectedItems().length > 1) {
            $scope.showEdit = false;
            $scope.showDelete = true;
        } else if (getSelectedItems().length == 1) {
            $scope.planId = getSelectedItems()[0];
            showCrudActions(true);
        } else {
            showCrudActions(false);
        }
        if ((unchecked == 0) || (checked == 0)) {
            $scope.checkboxes.checked = (checked == total);
        }
        // grayed checkbox
        angular.element(document.getElementById("select_all")).prop("indeterminate", (checked != 0 && unchecked != 0));
    }, true);
});