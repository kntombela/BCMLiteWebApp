testApp.controller('applicationCtrl', function ($scope, $rootScope, $http, $routeParams, applicationService, navService) {

    //Variables
    $scope.tableTitle = 'Applications';
    $scope.applications = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.application = {
        applicationID: -1,
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

    //Add new departments
    $scope.addApplication = function () {
        $scope.application.processID = sessionStorage.processId;;
        var requestResponse = applicationService.addEditApplication($scope.application);
        Message(requestResponse);
    };

    //Edit application
    $scope.editApplication = function () {
        getApplication($scope.applicationId);
    };

    //Delete process
    $scope.deleteApplication = function () {
        var requestResponse = applicationService.deleteApplication(getSelectedItems());
        Message(requestResponse);
        //Reset selected row
        resetRowSelect();
    };

    /**********************************HELPERS***************************************/
    //Get application
    function getApplication(id) {
        applicationService.getApplicationById(id).then(function (response) {
            $scope.application = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

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

    //Helper function to call api asynchronously
    function Message(requestResponse) {
        requestResponse.then(function successCallback(response) {
            //Repopulate page with refreshed list
            getApplicationList();
            //Show success message
            showMessageAlert(response.data.message)
            //Flag new rows
            if (response.data.ids) {
                $scope.newRecords = response.data.ids;
            }

        }, function errorCallback(response) {
            //Show error message
            showMessageAlert('Something went wrong, please try again or contact your administrator if the problem persists.');
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
    $scope.onRowClicked = function (applicationId) {
        $scope.applicationId = applicationId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[applicationId] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.application = {};
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
        angular.forEach($scope.applications, function (item) {
            if ($scope.checkboxes.items[item.applicationID]) {
                //Push all selected ids into array
                checked.push(item.applicationID);
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
        angular.forEach($scope.applications, function (item) {
            if (angular.isDefined(item.applicationID)) {
                $scope.checkboxes.items[item.applicationID] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.applications) {
            return;
        }
        //Count number of checked and unchecked checkboxes
        var checked = 0, unchecked = 0,
            total = $scope.applications.length;
        angular.forEach($scope.applications, function (item) {
            checked += ($scope.checkboxes.items[item.applicationID]) || 0;
            unchecked += (!$scope.checkboxes.items[item.applicationID]) || 0;

        });

        //IF multiple items have been selected do not show edit functionality      
        if (getSelectedItems().length > 1) {
            $scope.showEdit = false;
            $scope.showDelete = true;
        } else if (getSelectedItems().length == 1) {
            $scope.applicationId = getSelectedItems()[0];
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
