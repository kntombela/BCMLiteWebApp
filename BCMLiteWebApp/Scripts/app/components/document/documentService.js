testApp.service("documentService", function ($http) {

    //Get all documents
    this.getDocuments = function (processId) {
        return $http.get("/api/processes/" + processId + "/documents");
    };

    //Get document by id
    this.getDocumentById = function (documentId) {
        return $http.get("/api/documents/" + documentId + "/details");
    };

    //Add/Edit document
    this.addEditDocument = function (document) {
        return $http({
            method: "post",
            url: "/api/documents",
            data: document
        });
    };

    //Delete documents
    this.deleteDocuments = function (ids) {
        return $http({
            method: "post",
            url: "/api/documents/delete",
            data: ids
        });
    };

    //Import documents
    this.importDocuments = function (documents) {
        return $http({
            method: "post",
            url: "/api/documents/import",
            data: documents
        });
    };
});