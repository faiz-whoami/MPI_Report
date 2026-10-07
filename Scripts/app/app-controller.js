(function () {
    "use strict";

    angular.module("mpiApp").controller("AppController", ["$location", "$rootScope", "ApiService", function ($location, $rootScope, ApiService) {
        var vm = this;

        vm.username = "";
        vm.notice = "";
        vm.authenticated = false;

        ApiService.initialize();

        if ($location.path() !== "/login") {
            ApiService.get("SessionInfo").then(function (response) {
                vm.username = response.data.username;
                vm.authenticated = true;
            });
        }

        $rootScope.$on("app:notice", function (event, message) {
            vm.notice = message || "";
        });

        $rootScope.$on("auth:signedin", function (event, username) {
            vm.username = username || "";
            vm.authenticated = true;
        });

        $rootScope.$on("auth:expired", function () {
            vm.authenticated = false;
            vm.username = "";
            ApiService.clearToken();
            ApiService.initialize();
            $location.search({});
            $location.path("/login");
        });

        $rootScope.$on("job:required", function (event, message) {
            vm.notice = message || "Select a client / rig / job first.";
            $location.path("/dashboard");
        });

        vm.logout = function () {
            ApiService.post("Logout").then(function () {
                vm.authenticated = false;
                vm.username = "";
                ApiService.clearToken();
                ApiService.initialize();
                $location.path("/login");
            });
        };
    }]);
}());