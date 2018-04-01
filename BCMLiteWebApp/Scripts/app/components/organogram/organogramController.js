testApp.controller('organogramCtrl', function ($scope, $http, sharedService, organogramService) {

    //Variables
    $scope.departments = [];
    $scope.selectedRow = null;
    $scope.isNew = false;
    $scope.department = {
        departmentId: '',
        name: '',
        description: '',
        revenueGenerating: false,
        revenue: '',
        organisationID: ''
    };

    //Check to see if session storage is null if not get all departments
    if (sharedService.organisationId != null) {
        getDepartments(sharedService.organisationId);
    }  

    //Handle organisation dropdown select event
    $scope.$on('organisationSelected', function () {
        getDepartments(sharedService.organisationId);
        resetRowSelect();
    });

    //Add new departments
    $scope.addDepartment = function (organisationId) {
        $scope.department.organisationID = organisationId;
        var requestResponse = organogramService.addEditDepartment($scope.department);
        Message(requestResponse, organisationId);
    };

    //Edit existing department
    $scope.editDepartment = function (departmentId) {
        organogramService.getDepartmentById(departmentId).then(function (department) {
            $scope.department = department.data;
        },
            function () {
                alert('Error getting record');
            });
    }

    //Delete department
    $scope.deleteDepartment = function (departmentId, organisationId) {
        var requestResponse = organogramService.deleteDepartment(departmentId);
        Message(requestResponse, organisationId);
        resetRowSelect();
    };

    //Get departments
    function getDepartments(organisationId) {
        organogramService.getDepartments(organisationId).then(function (response) {
            $scope.organisationId = organisationId;
            $scope.departments = response.data;
        });
    }

    function Message(requestResponse, organisationId) {
        requestResponse.then(function successCallback(response) {
            getDepartments(organisationId);
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
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.selectedRow = null;
        $scope.department = {};
        showCrudActions(false);
    }

   //toggle between show and hide actions
    function showCrudActions(isShown) {
        $scope.showActions = isShown;
    }

});