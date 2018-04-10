testApp.controller('organogramCtrl', function ($scope, $http, sharedService, organogramService) {

    //Variables
    $scope.departments = [];
    $scope.isNew = false;
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.department = {
        departmentId: '',
        name: '',
        description: '',
        revenueGenerating: false,
        revenue: '',
        organisationID: ''
    };

    //Get all departments on load
    getDepartments();

    //Handle organisation dropdown select event
    $scope.$on('organisationSelected', function () {
        //Show loader
        $scope.showLoader = true;
        getDepartments(sharedService.organisationId);
        resetRowSelect();
    });

    //Add new departments
    $scope.addDepartment = function () {
        $scope.department.organisationID = sharedService.organisationId;
        var requestResponse = organogramService.addEditDepartment($scope.department);
        Message(requestResponse);
    };

    //Edit existing department
    $scope.editDepartment = function () {
        organogramService.getDepartmentById($scope.departmentId).then(function (department) {
            $scope.department = department.data;
        },
            function () {
                alert('Error getting record');
            });
    }

    //Delete department
    $scope.deleteDepartment = function () {
        //Call delete function of service
        var requestResponse = organogramService.deleteMultipleDepartments(getSelectedItems());
        Message(requestResponse);

        resetRowSelect();
    };

    //Clear form before adding new department
    $scope.resetRowSelect = function () {
        resetRowSelect();
    }

    //Show CRUD actions when a row is selected
    $scope.onRowClicked = function (departmentId) {
        $scope.departmentId = departmentId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[departmentId] = true;
        showCrudActions(true);
    }

    //Get departments
    function getDepartments() {
        if (sharedService.organisationId != null) {
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get departments
            organogramService.getDepartments(sharedService.organisationId).then(function (response) {
                //$scope.organisationId = organisationId;
                $scope.departments = response.data;
                if (!$scope.departments.length) {
                    $scope.recordsError = "No departments added yet, click 'New' to begin.";
                }
                else {
                    $scope.recordsError = "";
                }
            }).finally(function () {
                //Close loader when data has been loaded
                $scope.showLoader = false;
            });
        } else {
            $scope.showNew = false;
        }
    }

    //Helper function to call api asynchronously
    function Message(requestResponse) {
        requestResponse.then(function successCallback(response) {
            getDepartments();
            $('#addEditDepartment').modal('hide');
            // this callback will be called asynchronously
            // when the response is available
        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
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

    //Get number of selected items
    function getSelectedItems() {
        //Get list of checked items
        var checked = [];
        angular.forEach($scope.departments, function (item) {
            if ($scope.checkboxes.items[item.departmentID]) {
                //Push all selected ids into array
                checked.push(item.departmentID);
            }
        });

        return checked;
    }

    //watch for check all checkbox
    $scope.$watch('checkboxes.checked', function (value) {
        angular.forEach($scope.departments, function (item) {
            if (angular.isDefined(item.departmentID)) {
                $scope.checkboxes.items[item.departmentID] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.departments) {
            return;
        }
        var checked = 0, unchecked = 0,
            total = $scope.departments.length;
        angular.forEach($scope.departments, function (item) {
            checked += ($scope.checkboxes.items[item.departmentID]) || 0;
            unchecked += (!$scope.checkboxes.items[item.departmentID]) || 0;
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

});