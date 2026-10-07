(function () {
    "use strict";

    var app = angular.module("mpiApp");

    app.controller("InventoryController", ["ApiService", "$location", "$rootScope", function (ApiService, $location, $rootScope) {
        var vm = this;
        vm.item = {};
        vm.items = [];
        vm.errors = {};
        vm.error = "";
        vm.creating = $location.path() === "/inventory/create";

        if (!vm.creating) {
            ApiService.get("Inventory").then(function (response) {
                vm.items = response.data;
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to load inventory.";
            });
        }

        vm.save = function () {
            vm.errors = {};
            ApiService.post("CreateInventory", vm.item).then(function () {
                $rootScope.$broadcast("app:notice", "Inventory item added.");
                $location.path("/inventory");
            }, function (response) {
                vm.errors = response.data && response.data.errors || {};
                vm.error = response.data && response.data.message || "Unable to save the item.";
            });
        };
    }]);

    app.controller("ChecklistController", ["ApiService", "$location", "$rootScope", function (ApiService, $location, $rootScope) {
        var vm = this;
        var now = new Date();
        vm.checklist = {
            Frequency: "Daily",
            ChecklistDate: new Date(now.getFullYear(), now.getMonth(), now.getDate())
        };
        vm.items = [];
        vm.errors = {};
        vm.error = "";
        vm.creating = $location.path() === "/checklists/create";
        vm.frequencies = ["Daily", "Weekly", "Monthly", "Quarterly", "Six Month", "Yearly"];

        if (!vm.creating) {
            ApiService.get("Checklists").then(function (response) {
                vm.items = response.data;
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to load checklists.";
            });
        }

        vm.save = function () {
            vm.errors = {};
            ApiService.post("CreateChecklist", {
                Title: vm.checklist.Title,
                Frequency: vm.checklist.Frequency,
                ChecklistDate: vm.checklist.ChecklistDate,
                itemLines: vm.checklist.itemLines
            }).then(function () {
                $rootScope.$broadcast("app:notice", "Checklist created.");
                $location.path("/checklists");
            }, function (response) {
                vm.errors = response.data && response.data.errors || {};
                vm.error = response.data && response.data.message || "Unable to save the checklist.";
            });
        };
    }]);

    app.controller("CorrectiveActionController", ["ApiService", "$location", "$rootScope", function (ApiService, $location, $rootScope) {
        var vm = this;
        vm.action = { Status: "Open", Criticality: "Minor" };
        vm.items = [];
        vm.errors = {};
        vm.error = "";
        vm.creating = $location.path() === "/corrective-actions/create";
        vm.criticalities = ["Critical", "Major", "Minor", "Observation"];
        vm.statuses = ["Open", "Closed"];

        if (!vm.creating) {
            ApiService.get("CorrectiveActions").then(function (response) {
                vm.items = response.data;
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to load corrective actions.";
            });
        }

        vm.save = function () {
            vm.errors = {};
            ApiService.post("CreateCorrectiveAction", vm.action).then(function () {
                $rootScope.$broadcast("app:notice", "Corrective action created.");
                $location.path("/corrective-actions");
            }, function (response) {
                vm.errors = response.data && response.data.errors || {};
                vm.error = response.data && response.data.message || "Unable to save the corrective action.";
            });
        };
    }]);

    app.controller("DailyMeetingController", ["ApiService", "$location", "$rootScope", function (ApiService, $location, $rootScope) {
        var vm = this;
        var now = new Date();
        vm.meeting = {
            MeetingDate: new Date(now.getFullYear(), now.getMonth(), now.getDate())
        };
        vm.items = [];
        vm.errors = {};
        vm.error = "";
        vm.creating = $location.path() === "/daily-meetings/create";

        if (!vm.creating) {
            ApiService.get("DailyMeetings").then(function (response) {
                vm.items = response.data;
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to load daily meetings.";
            });
        }

        vm.save = function () {
            vm.errors = {};
            ApiService.post("CreateDailyMeeting", vm.meeting).then(function () {
                $rootScope.$broadcast("app:notice", "Daily meeting created.");
                $location.path("/daily-meetings");
            }, function (response) {
                vm.errors = response.data && response.data.errors || {};
                vm.error = response.data && response.data.message || "Unable to save the meeting.";
            });
        };
    }]);
}());