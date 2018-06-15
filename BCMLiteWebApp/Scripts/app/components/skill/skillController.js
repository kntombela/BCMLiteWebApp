testApp.controller('skillCtrl', function ($scope, $rootScope, $http, $routeParams, skillService, navService) {

    //Variables
    $scope.tableTitle = "Skills";
    $scope.skills = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.skill = {
        skillID: -1,
        description: '',
        rto: '',
        processID: ''
    };

    //Get skill list
    $scope.getSkillList = function () {
        getSkillList();
    }

    //Add new skills
    $scope.addSkill = function () {
        $scope.skill.processID = sessionStorage.processId;;
        var requestResponse = skillService.addEditSkill($scope.skill);
        Message(requestResponse);
    };

    //Edit skill
    $scope.editSkill = function () {
        getSkill($scope.skillId);
    };

    //Delete skill
    $scope.deleteSkill = function () {
        var requestResponse = skillService.deleteSkills(getSelectedItems());
        Message(requestResponse);
        //Reset selected row
        resetRowSelect();
    };

    //Import skills
    function Import(data) {
        if (sessionStorage.processId) {
            //Append selected process to import data
            for (x in data) {
                data[x].processID = sessionStorage.processId;
            }
            var requestResponse = skillService.importSkills(data);

            Message(requestResponse);

        } else {
            alert("No process selected!");
        }
    }

    /**********************************HELPERS***************************************/
    //Get skill
    function getSkill(id) {
        skillService.getSkillById(id).then(function (response) {
            $scope.skill = response.data;
        }, function () {
            alert('Error getting record');
        });
    }

    //Get skill list
    function getSkillList() {
        if ($routeParams.processId) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get skills
            skillService.getSkills($routeParams.processId).then(function (response) {
                $scope.skills = response.data;
                if (!$scope.skills.length) {
                    $scope.recordsError = "No skill sets added yet, click 'New' to begin.";
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
            getSkillList();
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

                    getSkillList();

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
    $scope.onRowClicked = function (skillId) {
        $scope.skillId = skillId;
        //Clear check boxes prior to select if items have selected
        if (getSelectedItems()) {
            $scope.checkboxes.items = {};
        }
        $scope.checkboxes.items[skillId] = true;
        showCrudActions(true);
    }

    //Reset row select
    function resetRowSelect() {
        $scope.skill = {};
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
        angular.forEach($scope.skills, function (item) {
            if ($scope.checkboxes.items[item.skillID]) {
                //Push all selected ids into array
                checked.push(item.skillID);
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
        angular.forEach($scope.skills, function (item) {
            if (angular.isDefined(item.skillID)) {
                $scope.checkboxes.items[item.skillID] = value;
            }
        });
    });

    // Watch for multiple item selects
    $scope.$watch('checkboxes.items', function (value) {
        if (!$scope.skills) {
            return;
        }
        //Count number of checked and unchecked checkboxes
        var checked = 0, unchecked = 0,
            total = $scope.skills.length;
        angular.forEach($scope.skills, function (item) {
            checked += ($scope.checkboxes.items[item.skillID]) || 0;
            unchecked += (!$scope.checkboxes.items[item.skillID]) || 0;

        });

        //IF multiple items have been selected do not show edit functionality      
        if (getSelectedItems().length > 1) {
            $scope.showEdit = false;
            $scope.showDelete = true;
        } else if (getSelectedItems().length == 1) {
            $scope.skillId = getSelectedItems()[0];
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
