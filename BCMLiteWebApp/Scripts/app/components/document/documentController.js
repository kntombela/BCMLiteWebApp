testApp.controller('documentCtrl', function ($scope, $rootScope, $http, $routeParams, documentService, navService) {

    //Variables
    $scope.documents = [];
    $scope.newRecords = [];
    $scope.checkboxes = { 'checked': false, items: {} };
    $scope.document = {
        documentID: '',
        description: '',
        rto: '',
        processID: ''
    };

    //Get document list
    $scope.getDocumentList = function () {
        getDocumentList();
    }

    /**********************************HELPERS***************************************/
    //Get document list
    function getDocumentList() {
        if ($routeParams.processId) {
            //Show button to add new item
            $scope.showNew = true;
            //Show loader
            $scope.showLoader = true;
            //Get documents
            documentService.getDocuments($routeParams.processId).then(function (response) {
                $scope.documents = response.data;
                if (!$scope.documents.length) {
                    $scope.recordsError = "No documents added yet, click 'New' to begin.";
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
