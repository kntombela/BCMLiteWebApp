app.controller('organisationsCtrl', function ($scope, $http, sharedService) {

    //Variables
    //$scope.organisationId = 1;

    //Get list of organisations to populate drop down
    $http.get("/api/organisations").then(function (response) {
        $scope.organisations = response.data;
    });

    //Get organisation's departments
    $scope.getDepartments = function (organisationId) { 
        $http.get("/api/organisations/" + organisationId + "/departments").then(function (response) {
            $scope.departments = response.data;
        });
    };

    //$scope.setSessionOrganisation = function (organsationId) {
    //    sessionStorage.setItem("organisation", organsationId);
    //};


    $scope.setSessionOrganisation = function (orgId) {
        sessionStorage.setItem("organisation", organsationId);
        sharedService.prepForPublish(orgId);
    };

    $scope.$on('handlePublish', function () {
        $scope.organisationId = sharedService.organisationId;
    });

});