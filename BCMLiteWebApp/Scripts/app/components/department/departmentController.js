testApp.controller('departmentCtrl', function ($scope, $http, $routeParams, sharedService, departmentService, navService) {

    //Variables
    $scope.newDepartments = [];
    $scope.pageTitle = "Departments";
    $scope.departments = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.department = {
        departmentId: '',
        name: '',
        description: '',
        revenueGenerating: false,
        revenue: '',
        organisationID: ''
    };

    //Re-populate department scope on route change
    //to make it available to next controller
    if ($routeParams.departmentId) {
        sessionStorage.departmentId = $routeParams.departmentId;
        $scope.pageTitle = "Details";
        getDepartment($routeParams.departmentId);
    }

    //Initialise Index
    $scope.getDepartmentList = function () {
        getDepartmentList();
    };


    //Handle organisation dropdown select event
    $scope.$on('organisationSelected', function () {
        //Show loader
        $scope.showLoader = true;
        getDepartmentList();
        resetRowSelect();
    });

    //Add new departments
    $scope.addDepartment = function () {
        $scope.department.organisationID = sharedService.organisationId;
        var requestResponse = departmentService.addEditDepartment($scope.department);
        Message(requestResponse);
    };

    //Edit existing department
    $scope.editDepartment = function () {
        getDepartment($scope.departmentId);
    }

    //Delete department
    $scope.deleteDepartment = function () {
        //Call delete function of service
        var requestResponse = departmentService.deleteDepartments(getSelectedItems());
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

    //Get department
    function getDepartment(id) {
        departmentService.getDepartmentById(id).then(function (department) {
            $scope.department = department.data;
        }, function () {
            alert('Error getting record');
        });
    }

    //Get departments
    function getDepartmentList() {
        if (sharedService.organisationId != null) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get departments
            departmentService.getDepartments(sharedService.organisationId).then(function (response) {
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
            //Hide add new item button
            $scope.showNew = false;
        }
    }

    //Import departments
    function Import(data) {

        if (sharedService.organisationId) {

            //Append selected organisation to import data
            for (x in data) {

                data[x].organisationID = sharedService.organisationId;

            }

            var requestResponse = departmentService.importDepartments(data);

            Message(requestResponse);

        } else {

            alert("No organisation selected!");

        }

    }

    //Helper function to call api asynchronously
    function Message(requestResponse) {
        requestResponse.then(function successCallback(response) {
            //Repopulate page with refreshed list
            getDepartmentList();
            //Close popup window
            $('#addEditDepartment').modal('hide');
            //Show success message
            showMessageAlert(response.data.message)
            //Flag new rows
            if (response.data.ids) {
                $scope.newDepartments = response.data.ids;
            }

        }, function errorCallback(response) {
            //Show error message
            showMessageAlert("Something went wrong, please try again or contact your administrator if the problem persists.");
        });
    }

    //Flag new rows
    $scope.isNewRow = function (id) {
        var flag = false
        if ($scope.newDepartments) {
            flag = $scope.newDepartments.includes(id);
        }
        return flag;
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

    function showMessageAlert(msg) {
        $scope.message = msg;
        $scope.showMessageAlert = true;
    }

    $scope.$watch('showMessageAlert', function (value) {
        //Hide message alert after 3 seconds
        setTimeout(function () { $scope.showMessageAlert = false; }, 3000);
    });

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

    //Show/hide column sorting
    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;
        $scope.reverse = !$scope.reverse;
    }

    //Import departments from excel
    $scope.loadFile = function (files) {

        $scope.$apply(function () {

            $scope.selectedFile = files[0];

        })

    }

    $scope.handleFile = function () {

        var file = $scope.selectedFile;

        if (file) {

            var reader = new FileReader();

            reader.onload = function (e) {

                var data = e.target.result;

                var workbook = XLSX.read(data, { type: 'binary' });

                var first_sheet_name = workbook.SheetNames[0];

                var dataObjects = XLSX.utils.sheet_to_json(workbook.Sheets[first_sheet_name]);

                //console.log(excelData);  

                if (dataObjects.length > 0) {

                    Import(dataObjects);

                    getDepartmentList();

                } else {

                    showMessageAlert("Error loading file");

                }

            }

            reader.onerror = function (ex) {

            }

            reader.readAsBinaryString(file);
        }
    }

});