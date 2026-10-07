(function () {
    "use strict";

    angular.module("mpiApp").controller("LoginController", ["$location", "$rootScope", "ApiService", function ($location, $rootScope, ApiService) {
        var vm = this;

        function routeForReturnUrl(returnUrl) {
            var routes = {
                "/": "/dashboard",
                "/home/index": "/dashboard",
                "/inventory/index": "/inventory",
                "/inventory/create": "/inventory/create",
                "/checklist/index": "/checklists",
                "/checklist/create": "/checklists/create",
                "/correctiveaction/index": "/corrective-actions",
                "/correctiveaction/create": "/corrective-actions/create",
                "/dailymeeting/index": "/daily-meetings",
                "/dailymeeting/create": "/daily-meetings/create",
                "/report/index": "/reports",
                "/customer/index": "/customers",
                "/customer/create": "/customers/create"
            };
            var path = (returnUrl || "").split("?")[0].toLowerCase();
            var customerMatch = path.match(/^\/customer\/(details|edit)\/(\d+)$/);

            if (customerMatch) {
                return "/customers/" + customerMatch[2] + (customerMatch[1] === "edit" ? "/edit" : "");
            }

            return routes[path] || "/dashboard";
        }

        vm.credentials = { Username: "", Password: "", RememberMe: false };
        vm.returnUrl = $location.search().returnUrl || "";
        vm.error = "";
        vm.loading = false;

        ApiService.initialize();

        vm.login = function () {
            vm.loading = true;
            vm.error = "";

            ApiService.post("Login", {
                Username: vm.credentials.Username,
                Password: vm.credentials.Password,
                RememberMe: vm.credentials.RememberMe,
                returnUrl: vm.returnUrl
            }).then(function (response) {
                ApiService.clearToken();
                return ApiService.initialize().then(function () {
                    $rootScope.$broadcast("auth:signedin", vm.credentials.Username);
                    $location.search({});
                    $location.path(routeForReturnUrl(response.data.returnUrl));
                });
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to sign in.";
            }).finally(function () {
                vm.loading = false;
            });
        };
    }]);
}());