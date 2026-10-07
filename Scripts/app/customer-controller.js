(function () {
    "use strict";

    angular.module("mpiApp").controller("CustomerController", ["ApiService", "$location", "$routeParams", "$rootScope", function (ApiService, $location, $routeParams, $rootScope) {
        var vm = this;
        var path = $location.path();
        var id = parseInt($routeParams.id, 10);

        vm.customer = { IsActive: true };
        vm.customers = [];
        vm.search = "";
        vm.status = "active";
        vm.errors = {};
        vm.error = "";
        vm.isForm = path === "/customers/create" || /\/edit$/.test(path);
        vm.isDetails = !vm.isForm && !isNaN(id);
        vm.isEdit = /\/edit$/.test(path);

        vm.load = function () {
            vm.error = "";
            ApiService.get("Customers", { search: vm.search, status: vm.status }).then(function (response) {
                vm.customers = response.data;
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to load customers.";
            });
        };

        if (vm.isDetails || vm.isEdit) {
            ApiService.get("Customer", { id: id }).then(function (response) {
                vm.customer = response.data;
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to load the customer.";
            });
        } else if (!vm.isForm) {
            vm.load();
        }

        vm.save = function () {
            vm.errors = {};
            ApiService.post(vm.isEdit ? "UpdateCustomer" : "CreateCustomer", vm.customer).then(function () {
                $rootScope.$broadcast("app:notice", vm.isEdit ? "Customer updated." : "Customer created.");
                $location.path("/customers");
            }, function (response) {
                vm.errors = response.data && response.data.errors || {};
                vm.error = response.data && response.data.message || "Unable to save the customer.";
            });
        };

        vm.remove = function (customer) {
            if (!window.confirm("Delete " + customer.Name + "?")) {
                return;
            }

            ApiService.post("DeleteCustomer", { id: customer.CustomerId }).then(function () {
                vm.load();
                $rootScope.$broadcast("app:notice", "Customer deleted.");
            }, function (response) {
                vm.error = response.data && response.data.message || "Unable to delete the customer.";
            });
        };
    }]);
}());