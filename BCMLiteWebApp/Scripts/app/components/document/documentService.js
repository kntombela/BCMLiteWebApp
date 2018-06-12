testApp.service("documentService", function ($http) {

    //Get all documents
    this.getDocuments = function (processId) {
        return $http.get("/api/processes/" + processId + "/documents");
    };
});