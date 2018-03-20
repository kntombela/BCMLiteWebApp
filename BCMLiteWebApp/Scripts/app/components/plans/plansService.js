app.service("plansService", function ($http) {

    //Get Organisation Plans
    this.getOrganisationPlans = function (organisationId) {
        return $http.get("/api/organisations/" + organisationId + "/plans");
    }

    //Get List of organisations
    this.getOrganisationDropDown = function () {
        return $http.get("/api/organisations");
    }

    ////Add Organisation
    //this.addNewOrganisation = function (organisation) {
    //    return $http({
    //        method: "post",
    //        url: "api/organisation",
    //        data: organisation
    //    });
    //}

    ////Update Organisation
    //this.updateOrganisation = function (quote) {
    //    return $http({
    //        method: "post",
    //        url: "api/QuotesViewModelApi",
    //        data: JSON.stringify(quote),
    //        dataType: "json"
    //    });
    //}
    ////Delete Organisation
    //this.deleteOrganisation = function (id) {
    //    return $http({
    //        method: "delete",
    //        url: "api/QuotesViewModelApi/" + id
    //    });
    //}
});  