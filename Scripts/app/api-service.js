(function () {
    "use strict";

    angular.module("mpiApp")
        .config(["$httpProvider", function ($httpProvider) {
            $httpProvider.interceptors.push("AuthenticationInterceptor");
        }])
        .factory("AuthenticationInterceptor", ["$q", "$rootScope", function ($q, $rootScope) {
            return {
                responseError: function (response) {
                    if (response.status === 401) {
                        $rootScope.$broadcast("auth:expired");
                    } else if (response.status === 409) {
                        $rootScope.$broadcast("job:required", response.data && response.data.message);
                    }

                    return $q.reject(response);
                }
            };
        }])
        .factory("ApiService", ["$http", "$q", function ($http, $q) {
        var config = window.mpiAppConfig || {};
        var requestToken = "";
        var tokenPromise = null;

        function url(action) {
            return config.apiRoot + action;
        }

        function post(action, model) {
            return initialize().then(function () {
                return sendPost(action, model, false);
            });
        }

        function sendPost(action, model, retried) {
            var data = angular.copy(model || {});
            angular.forEach(data, function (value, key) {
                if (value instanceof Date) {
                    data[key] = [value.getFullYear(), ("0" + (value.getMonth() + 1)).slice(-2), ("0" + value.getDate()).slice(-2)].join("-");
                }
            });

            data.__RequestVerificationToken = requestToken;
            return $http.post(url(action), $.param(data), {
                headers: { "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8" }
            }).then(null, function (response) {
                if (!retried && response.status === 400 && response.data && response.data.code === "anti_forgery") {
                    requestToken = "";
                    tokenPromise = null;
                    return initialize().then(function () {
                        return sendPost(action, model, true);
                    });
                }

                return $q.reject(response);
            });
        }

        function initialize() {
            if (!tokenPromise) {
                tokenPromise = $http.get(url("AntiForgeryToken")).then(function (response) {
                    requestToken = response.data.token;
                    return response;
                }, function (response) {
                    tokenPromise = null;
                    return $q.reject(response);
                });
            }

            return tokenPromise;
        }

        return {
            get: function (action, params) {
                return $http.get(url(action), { params: params || {} });
            },
            initialize: initialize,
            clearToken: function () {
                requestToken = "";
                tokenPromise = null;
            },
            post: post,
            download: function (targetUrl) {
                return $http.get(targetUrl, { responseType: "blob" });
            }
        };
    }]);
}());