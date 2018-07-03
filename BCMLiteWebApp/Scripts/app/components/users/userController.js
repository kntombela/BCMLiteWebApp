testApp.controller('userCtrl', function ($scope, $rootScope, $http, $routeParams, userService, navService, sharedService) {

    //Variables
    $scope.tableTitle = "Users";
    $scope.users = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.user = {}; //TODO: Update

    //Get user list
    getUserList();


    //Add new users
    $scope.addUser = function () {
        $scope.user.processID = sessionStorage.processId;;
        var requestResponse = userService.addEditUser($scope.user);
        Message(requestResponse);
    };

    //Edit user
    $scope.editUser = function () {
        getUser($scope.userId);
    };

    //Delete user
    $scope.deleteUser = function () {
        var requestResponse = userService.deleteUsers(getSelectedItems());
        Message(requestResponse);
        //Reset selected row
        resetRowSelect();
    };

    $scope.setSelectedPlanOwner = function (userId) {
        sharedService.setPlanOwner(userId);
    };

    //Import users
    function Import(data) {
        if (sessionStorage.processId) {
            //Append selected process to import data
            for (x in data) {
                data[x].processID = sessionStorage.processId;
            }
            var requestResponse = userService.importUsers(data);

            Message(requestResponse);

        } else {
            alert("No process selected!");
        }
    }

    //Get user count
    $scope.getUserCount = function () {
        userService.getUsers($routeParams.processId).then(function (response) {
            if (response.data.length) {
                $scope.userCount = response.data.length;
            }
            else {
                $scope.userCount = 0;
            }
        });
    };

    /**********************************HELPERS***************************************/
    //Get user
    function getUser(id) {
        userService.getUserById(id).then(function (response) {
            $scope.user = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

    //Get user list
    function getUserList() {
        if (sharedService.organisationId) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get users
            userService.getUsers(sharedService.organisationId).then(function (response) {
                $scope.users = response.data;
                if (!$scope.users.length) {
                    $scope.recordsError = "No users added yet, click 'New' to begin.";
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
            getUserList();
            //Close popup window
            $('#addEditModal').modal('hide');
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

    //Handle file import
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

                if (dataObjects.length > 0) {

                    Import(dataObjects);

                    getUserList();

                } else {
                    $scope.msg = "Error : Something Wrong !";
                }
            }
            reader.onerror = function (ex) {
            }
            reader.readAsBinaryString(file);
        }
    }

    /**********************************CRUD ACTIONS**********************************/
    //Clear form before adding new process
    $scope.resetRowSelect = function () {
        resetRowSelect();
    }

    //Show CRUD actions when a row is selected
    $scope.onRowClicked = function (userId) {
        $scope.userId = userId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[userId] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.user = {};
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
        angular.forEach($scope.users, function (item) {
            if ($scope.checkboxes.items[item.userID]) {
                //Push all selected ids into array
                checked.push(item.userID);
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
        angular.forEach($scope.users, function (item) {
            if (angular.isDefined(item.userID)) {
                $scope.checkboxes.items[item.userID] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.users) {
            return;
        }
        //Count number of checked and unchecked checkboxes
        var checked = 0, unchecked = 0,
            total = $scope.users.length;
        angular.forEach($scope.users, function (item) {
            checked += ($scope.checkboxes.items[item.userID]) || 0;
            unchecked += (!$scope.checkboxes.items[item.userID]) || 0;

        });

        //IF multiple items have been selected do not show edit functionality      
        if (getSelectedItems().length > 1) {
            $scope.showEdit = false;
            $scope.showDelete = true;
        } else if (getSelectedItems().length == 1) {
            $scope.userId = getSelectedItems()[0];
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
