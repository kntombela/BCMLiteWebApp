testApp.service("crudService", function ($rootScope) {

    var model = '';

    /***********Crud Actions**************/
    //Clear row select before adding new row
    this.resetRowSelect = function (model) {
        resetRowSelect(model);
    };

    //Show CRUD actions when a row is selected
    this.onRowClicked = function (modelName, model, scope) {
        this.model = modelName;
        //Get id based on model
        switch (modelName) {
            case 'process': id = 'processId';
                break;
            case 'department': id = 'departmentId';
                break;
        }
        scope.id = model[id];
        showCrudActions(true);
    };

    //Reset row select
    function resetRowSelect(model) {
        model = {};
        showCrudActions(false);
    }

    //toggle between show and hide CRUD actions
    function showCrudActions(isShown) {
        $rootScope.showEdit = isShown;
        $rootScope.showDelete = isShown;
    }

    function showMessageAlert(msg) {
        $rootScope.message = msg;
        $rootScope.showMessageAlert = true;
    }

    this.sort = function (keyname) {
        $rootScope.sortKey = keyname;   //set the sortKey to the param passed
        $rootScope.reverse = !$rootScope.reverse; //if true make it false and vice versa
    };

});