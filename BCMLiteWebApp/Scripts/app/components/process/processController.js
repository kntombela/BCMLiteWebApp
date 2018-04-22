testApp.controller('processCtrl', function ($scope, $http, $routeParams, processService) {

    //Variables
    $scope.pageTitle = "Processes";
    $scope.processes = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.process = {
        processID: '',
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
        location: ''
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
    $scope.addProcess = function (departmentId) {
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
        $scope.checkboxes.items[process] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.department = {};
        $scope.checkboxes = { 'checked': false, items: {} };
        showCrudActions(false);
    }

    //toggle between show and hide CRUD actions
    function showCrudActions(isShown) {
        $scope.showEdit = isShown;
        $scope.showDelete = isShown;
    }

    function showMessageAlert(msg) {
        $scope.message = msg;
        $scope.showMessageAlert = true;
    }

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }

    $scope.redirectToCreate = function () {
        window.location.href = '/#/processes/create';
    };

});