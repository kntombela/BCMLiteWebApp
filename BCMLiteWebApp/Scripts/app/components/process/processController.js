﻿testApp.controller('processCtrl', function ($scope, $http, $routeParams, processService, navService) {

    //Variables
    $scope.tableTitle = "Processes";
    $scope.processes = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.process = {
        processId: -1,
        name: '',
        description: '',
        criticalTime: '',
        sop: '',
        sla: '',
        rto: '',
        mbco: '',
        operationalImpact: '',
        financialImpact: '',
        staffCompliment: '',
        staffCompDesc: '',
        revisedOpsLevel: '',
        revisedOpsLevelDesc: '',
        remoteWorking: '',
        siteDependent: '',
        location: '',
        departmentID: ''
    };

    //Get processes
    //$scope.getProcesses = function () {

    //    if ($routeParams.id) {
    //        processService.getProcesses($routeParams.id).then(function (response) {
    //            $scope.processes = response.data;
    //            if (!$scope.processes.length) {
    //                $scope.recordsError = "No processes added yet, click 'New' to begin.";
    //            }
    //            else {
    //                $scope.recordsError = "";
    //            }
    //        }).finally(function () {
    //            //Close loader when data has been loaded
    //            $scope.showLoader = false;
    //            });
    //    }
    //}

    //Get processes on load
    $scope.getProcessList = function () {
        getProcessList();
    }

    //Add new process
    $scope.addProcess = function () {
        $scope.process.departmentID = sessionStorage.departmentId; //Check for null value
        var requestResponse = processService.addEditProcess($scope.process);
        Message(requestResponse);
    };

    //Get processes
    function getProcessList() {
        if ($routeParams.id) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get processes
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

    //Helper function to call api asynchronously
    function Message(requestResponse) {
        requestResponse.then(function successCallback(response) {
            //Repopulate page with refreshed list
            getProcessList();
            //Close popup window
            $('#addEditProcess').modal('hide');
            //Show success message
            showMessageAlert(response.data.message)
            //Flag new row
            $scope.newProcess = response.data.id;
        }, function errorCallback(response) {
            //Show error message
            showMessageAlert("Something went wrong, please try again or contact your administrator if the problem persists.");
        });
    }

    /***********Crud Actions**************/
    //Clear form before adding new process
    $scope.resetRowSelect = function () {
        resetRowSelect();
    }

    //Show CRUD actions when a row is selected
    $scope.onRowClicked = function (processId) {
        $scope.processId = processId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[processId] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.process = {};
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
        angular.forEach($scope.processes, function (item) {
            if ($scope.checkboxes.items[item.processID]) {
                //Push all selected ids into array
                checked.push(item.processID);
            }
        });

        return checked;
    }

    function showMessageAlert(msg) {
        $scope.message = msg;
        $scope.showMessageAlert = true;
    }

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }

    //watch for check all checkbox
    $scope.$watch('checkboxes.checked', function (value) {
        angular.forEach($scope.processes, function (item) {
            if (angular.isDefined(item.processID)) {
                $scope.checkboxes.items[item.processID] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.processes) {
            return;
        }
        var checked = 0, unchecked = 0,
            total = $scope.processes.length;
        angular.forEach($scope.processes, function (item) {
            checked += ($scope.checkboxes.items[item.processID]) || 0;
            unchecked += (!$scope.checkboxes.items[item.processID]) || 0;
        });
        //IF multiple items have been selected do not show edit functionality      
        if (getSelectedItems().length > 1) {
            $scope.showEdit = false;
            $scope.showDelete = true;
        } else if (getSelectedItems().length == 1) {
            $scope.showDelete = true;
        } else {
            showCrudActions(false);
        }
        if ((unchecked == 0) || (checked == 0)) {
            $scope.checkboxes.checked = (checked == total);
        }
        // grayed checkbox
        angular.element(document.getElementById("select_all")).prop("indeterminate", (checked != 0 && unchecked != 0));
    }, true);

    //Clear processGeneral form
    $scope.clearProcessGeneral = function () {
        $scope.process.name = '';
        $scope.process.description = '';
        $scope.process.location = '';
    }

});