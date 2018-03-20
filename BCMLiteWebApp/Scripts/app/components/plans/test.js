var app = angular.module('myApp', []);
app.controller('organisationsCtrl', function ($scope, $http) {
    $http.get("/api/organisations").then(function (response) {
        $scope.myData = response.data;
    });
});