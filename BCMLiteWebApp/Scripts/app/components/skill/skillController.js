testApp.controller('skillCtrl', function ($scope, $rootScope, $http, $routeParams, skillService, navService) {

    //Variables
    $scope.tableTitle = "Skills";
    $scope.skills = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.skill = {
        skillID: '',
        description: '',
        rto: '',
        processID: ''
    };

    //Get skill list
    $scope.getSkillList = function () {
        getSkillList();
    }

    /**********************************HELPERS***************************************/
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
});
