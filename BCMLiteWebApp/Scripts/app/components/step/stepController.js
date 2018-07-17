testApp.controller('stepCtrl', function ($scope, $rootScope, $http, $routeParams, stepService, navService, sharedService, defaultStepService) {

    //Variables
    $scope.pageTitle = 'Plan Steps';
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.steps = [];
    $scope.newRecords = [];
    $scope.step = {
        stepID: -1,
        departmentPlanID: '',
        number: '',
        title: '',
        summary: '',
        detail: ''
    };
    var departmentPlanId;

    setDepartmentPlanId();

    //Get step list when departmentPlanId is available in route params
    if ($routeParams.departmentPlanId) {
        getStepList();
    }

    //Copy steps on department drop down select
    $scope.$on('departmentPlanIdSet', function () {
        copyStepList();
    });

    //Add new step
    $scope.addStep = function () {
        if (departmentPlanId) {
            $scope.step.departmentPlanID = departmentPlanId;
            var requestResponse = stepService.addEditStep($scope.step);
            Message(requestResponse);
        }
    };

    //Edit step
    $scope.editStep = function (stepId) {
        getStep(stepId);
    };

    //Delete step
    $scope.deleteStep = function () {
        var requestResponse = stepService.deleteStep(getSelectedItems());
        Message(requestResponse);
        //Reset selected row
        resetRowSelect();
    };

    //Handle file import
    $scope.loadFile = function (files) {

        $scope.$apply(function () {

            $scope.selectedFile = files[0];

        });

    };

    $scope.handleFile = function () {
        var file = $scope.selectedFile;

        if (file) {

            var reader = new FileReader();

            reader.onload = function (e) {

                var data = e.target.result;

                var workbook = XLSX.read(data, { type: 'binary' });

                var first_sheet_name = workbook.SheetNames[0];

                var dataObjects = XLSX.utils.sheet_to_json(workbook.Sheets[first_sheet_name]);

                if (dataObjects.length > 0) {

                    Import(dataObjects);

                    getStepList();

                } else {
                    $scope.msg = "Error : Something Wrong !";
                }
            }
            reader.onerror = function (ex) {
            }
            reader.readAsBinaryString(file);
        }
    };

    /**********************************HELPERS***************************************/
    //Get step
    function getStep(id) {
        stepService.getStepById(id).then(function (response) {
            $scope.step = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

    function copyStepList() {
        if ($routeParams.planId && sharedService.departmentPlanId != null) {
            stepService.copyDefaultSteps($routeParams.planId, sharedService.departmentPlanId).then(function (response) {
                getStepList();
            });
        }
    }

    //Get step list
    function getStepList() {
        //Clear step list before load
        $scope.steps = [];

        //Set departmentPlanId depending on create or edit modes
        setDepartmentPlanId();

        //Show button to add new item
        $scope.showNew = true;
        //Show loader
        $scope.showLoader = true;
        //Get steps
        stepService.getSteps(departmentPlanId).then(function (response) {
            $scope.steps = response.data;
            if (!$scope.steps.length) {
                $scope.recordsError = "No steps added yet, click 'New' to begin.";
            }
            else {
                $scope.recordsError = "";
            }
        }).finally(function () {
            //Close loader when data has been loaded
            $scope.showLoader = false;
        });
    }

    function setDepartmentPlanId() {
        if (sharedService.departmentPlanId || $routeParams.departmentPlanId) {
            if (sharedService.departmentPlanId) {
                departmentPlanId = sharedService.departmentPlanId;
            } else {
                departmentPlanId = $routeParams.departmentPlanId;
            }
        }
    }

    //Get step list
    //function getStepList(departmentPlanId) {
    //    //Show button to add new item
    //    $scope.showNew = true;
    //    //Show loader
    //    $scope.showLoader = true;
    //    //Get steps
    //    stepService.getSteps(departmentPlanId).then(function (response) {
    //        $scope.steps = response.data;
    //        if (!$scope.steps.length) {
    //            $scope.recordsError = "No steps added yet, click 'New' to begin.";
    //        }
    //        else {
    //            $scope.recordsError = "";
    //        }
    //    }).finally(function () {
    //        //Close loader when data has been loaded
    //        $scope.showLoader = false;
    //    });
    //}

    //Helper function to call api asynchronously
    function Message(requestResponse) {
        requestResponse.then(function successCallback(response) {
            //Repopulate page with refreshed list
            getStepList();
            //Close popup window
            $('#addEditModal').modal('hide');
            //Show success message
            showMessageAlert(response.data.message);
            //Flag new rows
            if (response.data.ids) {
                $scope.newRecords = response.data.ids;
            }

        }, function errorCallback(response) {
            //Show error message
            showMessageAlert('Something went wrong, please try again or contact your administrator if the problem persists.');
        });
    }

    //Import steps
    function Import(data) {
        if (sessionStorage.planId) {
            //Append selected plan to import data
            for (x in data) {
                data[x].planId = sessionStorage.planId;
            }
            var requestResponse = stepService.importSteps(data);

            Message(requestResponse);

        } else {
            alert("No plan selected!");
        }
    }

    //Flag new rows
    $scope.isNewRow = function (id) {
        var flag = false;
        if ($scope.newRecords) {
            flag = $scope.newRecords.includes(id);
        }
        return flag;
    };

    /**********************************CRUD ACTIONS**********************************/
    //Clear form before adding new process
    $scope.resetRowSelect = function () {
        resetRowSelect();
    };

    //Show CRUD actions when a row is selected
    $scope.onRowClicked = function (stepId) {
        $scope.stepId = stepId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[stepId] = true;
        showCrudActions(true);
    };

    //Reset row select
    function resetRowSelect() {
        $scope.step = {};
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
        angular.forEach($scope.steps, function (item) {
            if ($scope.checkboxes.items[item.stepID]) {
                //Push all selected ids into array
                checked.push(item.stepID);
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
    };

    //watch for check all checkbox
    $scope.$watch('checkboxes.checked', function (value) {
        angular.forEach($scope.steps, function (item) {
            if (angular.isDefined(item.stepID)) {
                $scope.checkboxes.items[item.stepID] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.steps) {
            return;
        }
        //Count number of checked and unchecked checkboxes
        var checked = 0, unchecked = 0,
            total = $scope.steps.length;
        angular.forEach($scope.steps, function (item) {
            checked += ($scope.checkboxes.items[item.stepID]) || 0;
            unchecked += (!$scope.checkboxes.items[item.stepID]) || 0;

        });

        //IF multiple items have been selected do not show edit functionality      
        if (getSelectedItems().length > 1) {
            $scope.showEdit = false;
            $scope.showDelete = true;
        } else if (getSelectedItems().length == 1) {
            $scope.stepId = getSelectedItems()[0];
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