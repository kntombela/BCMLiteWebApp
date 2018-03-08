app.controller('planCtrl', function ($scope, $q, $window, plansService) {

    //Variables
    $scope.plans = [];
    $scope.title = "";

    //Get plans available to the selected organisation
    $scope.getPlans = function (organisationId) {
        plansService.getOrganisationPlans(organisationId).then(function (response) {
            $scope.plans = response.data;
        }, (error) => {
            $scope.title = "Oops... something went wrong";
        });
    };
});