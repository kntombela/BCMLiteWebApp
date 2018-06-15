testApp.service("skillService", function ($http) {

    //Get all skills
    this.getSkills = function (processId) {
        return $http.get("/api/processes/" + processId + "/skills");
    };

    //Get skill by id
    this.getSkillById = function (skillId) {
        return $http.get("/api/skills/" + skillId + "/details");
    };

    //Add/Edit skill
    this.addEditSkill = function (skill) {
        return $http({
            method: "post",
            url: "/api/skills",
            data: skill
        });
    };

    //Delete skills
    this.deleteSkills = function (ids) {
        return $http({
            method: "post",
            url: "/api/skills/delete",
            data: ids
        });
    };

    //Import skills
    this.importSkills = function (skills) {
        return $http({
            method: "post",
            url: "/api/skills/import",
            data: skills
        });
    };
});