testApp.controller('organogramCtrl', function ($scope, $http, sharedService, organogramService) {

    //Variables
    $scope.departments = [];
    $scope.isSelected = false;
    $scope.selectedRow = null;
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

         //Get list of checked items
        var checked = [];
        angular.forEach($scope.departments, function (item) {
            if ($scope.checkboxes.items[item.departmentID]) {
                //Push all selected ids into array
                checked.push(item.departmentID);
            }          
        });

        //TODO: May need to be removed if delete button is toggled by select
        if (!checked.length) {
            return;
        }
        else {

            //Call delete function of service
            var requestResponse = organogramService.deleteMultipleDepartments(checked);
            Message(requestResponse);
        }

        resetRowSelect();
    };

    //Get departments
    function getDepartments() {
        if (sharedService.organisationId != null) {
            //Show loader
            $scope.showLoader = true;
            //Get departments
            organogramService.getDepartments(sharedService.organisationId).then(function (response) {
                //$scope.organisationId = organisationId;
                $scope.departments = response.data;
            }).finally(function () {
                //Close loader when data has been loaded
                $scope.showLoader = false;
            });
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

    //Clear form before adding new department
    $scope.resetRowSelect = function () {
        resetRowSelect();
    }

    //Show CRUD actions when a row is selected
    $scope.onRowClicked = function (index, departmentId) {
        $scope.selectedRow = index;
        $scope.departmentId = departmentId;
        //Clear check boxes prior to select
        if ($scope.checkboxes) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[departmentId] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.selectedRow = null;
        $scope.department = {};
        $scope.checkboxes = { 'checked': false, items: {} };
        showCrudActions(false);
    }

    //toggle between show and hide actions
    function showCrudActions(isShown) {
        $scope.showActions = isShown;
    }

    //watch for check all checkbox
    $scope.$watch('checkboxes.checked', function (value) {
        angular.forEach($scope.departments, function (item) {
            if (angular.isDefined(item.departmentID)) {
                $scope.checkboxes.items[item.departmentID] = value;
            }
        });
    });

    // Watch for multiple checkbox selects
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
        if ((unchecked == 0) || (checked == 0)) {
            $scope.checkboxes.checked = (checked == total);
        }
        // grayed checkbox
        angular.element(document.getElementById("select_all")).prop("indeterminate", (checked != 0 && unchecked != 0));
    }, true);

});