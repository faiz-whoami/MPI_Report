(function () {
    "use strict";

    angular.module("mpiApp").controller("ReportController", ["$timeout", "ApiService", function ($timeout, ApiService) {
        var vm = this;
        var config = window.mpiAppConfig || {};

        vm.reports = [];
        vm.selectedId = "";
        vm.source = "ef";
        vm.loading = true;
        vm.generating = false;
        vm.generated = false;
        vm.error = "";
        vm.fetchTime = "N/A";

        ApiService.get("Reports").then(function (response) {
            vm.reports = response.data;
        }, function () {
            vm.error = "Unable to load inspection reports.";
        }).finally(function () {
            vm.loading = false;
        });

        vm.generate = function () {
            if (!vm.selectedId) {
                return;
            }

            vm.error = "";
            vm.generating = true;
            vm.generated = false;
            var reportWindow = window.open("about:blank", "_blank");
            var target = config.reportUrl + "?id=" + encodeURIComponent(vm.selectedId) + "&Sp=" + (vm.source === "sp");

            ApiService.download(target).then(function (response) {
                vm.fetchTime = response.headers("X-Data-Fetch-Time-Ms") || "N/A";
                vm.generated = true;
                var objectUrl = window.URL.createObjectURL(response.data);
                if (reportWindow && !reportWindow.closed) {
                    reportWindow.location = objectUrl;
                } else {
                    var link = document.createElement("a");
                    link.href = objectUrl;
                    link.download = "MPIInspectionReport.pdf";
                    link.click();
                }
                $timeout(function () {
                    window.URL.revokeObjectURL(objectUrl);
                }, 60000);
            }, function () {
                if (reportWindow && !reportWindow.closed) {
                    reportWindow.close();
                }
                vm.error = "Unable to generate the report.";
            }).finally(function () {
                vm.generating = false;
            });
        };
    }]);
}());