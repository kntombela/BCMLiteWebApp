testApp.controller('navCtrl', function ($scope, $http, sharedService) {

    //Variable
    $scope.appTitle = "BCMLite App";

    //Get list of organisations to populate drop down
    $http.get("/api/organisations").then(function (response) {
        $scope.organisations = response.data;
    });

    //Check to see if session storage is null if not get all departments
    //if (sessionStorage.length != 0) {
    //    $scope.organisationId = sessionStorage.getItem("organisation");
    //}

    $scope.onOrganisationSelected = function (organisationId) {
        sharedService.prepForPublish(organisationId);
    };

    $scope.$on('organisationSelected', function () {
        $scope.organisationId = sharedService.organisationId;
    });

});