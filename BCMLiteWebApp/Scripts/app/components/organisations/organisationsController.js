var app = angular.module('myApp', []);
app.controller('organisationsCtrl', function ($scope, $http) {

    //Variables
    $scope.organisationId = 1;

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


});