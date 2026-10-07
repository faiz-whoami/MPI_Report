(function () {
    "use strict";

    var config = window.mpiAppConfig || {};
    var template = function (name) {
        return config.templateRoot + name + ".html";
    };

    angular.module("mpiApp", ["ngRoute"])
        .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {
            $locationProvider.hashPrefix("");
            $routeProvider
                .when("/login", {
                    templateUrl: template("login"),
                    controller: "LoginController",
                    controllerAs: "vm"
                })
                .when("/dashboard", {
                    templateUrl: template("dashboard"),
                    controller: "DashboardController",
                    controllerAs: "vm"
                })
                .when("/inventory", {
                    templateUrl: template("inventory-list"),
                    controller: "InventoryController",
                    controllerAs: "vm"
                })
                .when("/inventory/create", {
                    templateUrl: template("inventory-form"),
                    controller: "InventoryController",
                    controllerAs: "vm"
                })
                .when("/checklists", {
                    templateUrl: template("checklist-list"),
                    controller: "ChecklistController",
                    controllerAs: "vm"
                })
                .when("/checklists/create", {
                    templateUrl: template("checklist-form"),
                    controller: "ChecklistController",
                    controllerAs: "vm"
                })
                .when("/corrective-actions", {
                    templateUrl: template("corrective-action-list"),
                    controller: "CorrectiveActionController",
                    controllerAs: "vm"
                })
                .when("/corrective-actions/create", {
                    templateUrl: template("corrective-action-form"),
                    controller: "CorrectiveActionController",
                    controllerAs: "vm"
                })
                .when("/daily-meetings", {
                    templateUrl: template("daily-meeting-list"),
                    controller: "DailyMeetingController",
                    controllerAs: "vm"
                })
                .when("/daily-meetings/create", {
                    templateUrl: template("daily-meeting-form"),
                    controller: "DailyMeetingController",
                    controllerAs: "vm"
                })
                .when("/reports", {
                    templateUrl: template("reports"),
                    controller: "ReportController",
                    controllerAs: "vm"
                })
                .when("/customers", {
                    templateUrl: template("customer-list"),
                    controller: "CustomerController",
                    controllerAs: "vm"
                })
                .when("/customers/create", {
                    templateUrl: template("customer-form"),
                    controller: "CustomerController",
                    controllerAs: "vm"
                })
                .when("/customers/:id", {
                    templateUrl: template("customer-details"),
                    controller: "CustomerController",
                    controllerAs: "vm"
                })
                .when("/customers/:id/edit", {
                    templateUrl: template("customer-form"),
                    controller: "CustomerController",
                    controllerAs: "vm"
                })
                .otherwise({ redirectTo: "/dashboard" });
        }])
        .run(["$location", function ($location) {
            if (!$location.url()) {
                $location.path(config.initialRoute || "/dashboard");
            }
        }]);
}());