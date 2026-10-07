(function () {
    "use strict";

    angular.module("mpiApp").controller("DashboardController", ["ApiService", "$rootScope", function (ApiService, $rootScope) {
        var vm = this;

        vm.loading = true;
        vm.dashboard = {};
        vm.error = "";

        ApiService.get("Dashboard").then(function (response) {
            vm.dashboard = response.data;
            vm.selectedJobId = response.data.selectedJobId;
        }, function (response) {
            vm.error = response.data && response.data.message || "Unable to load the dashboard.";
        }).finally(function () {
            vm.loading = false;
        });

        vm.selectJob = function () {
            if (!vm.selectedJobId) {
                return;
            }

            ApiService.post("SelectJob", { jobId: vm.selectedJobId }).then(function () {
                $rootScope.$broadcast("app:notice", "Job selected.");
                return ApiService.get("Dashboard").then(function (response) {
                    vm.dashboard = response.data;
                    vm.selectedJobId = response.data.selectedJobId;
                });
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to select the job.";
            });
        };
    }]);
}());