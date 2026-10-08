app.controller('accountController', function ($scope, accountService) {
    $scope.model = {
        username: '',
        password: '',
        rememberMe: false
    };

    $scope.errorMessage = '';
    $scope.isSubmitting = false;

    $scope.login = function () {
        $scope.errorMessage = '';
        $scope.isSubmitting = true;

        var antiForgeryToken = document.querySelector(
            'input[name="__RequestVerificationToken"]'
        );

       var result = accountService.login(
            $scope.model.username,
            $scope.model.password,
            $scope.model.rememberMe,
            antiForgeryToken ? antiForgeryToken.value : ''
        ).then(function (response) {
            if (response.data.success) {
                window.location.href = response.data.redirectUrl;
                return;
            }

            $scope.errorMessage = response.data.errors && response.data.errors.length
                ? response.data.errors[0]
                : 'Unable to sign in.';
        }).catch(function () {
            $scope.errorMessage = 'Unable to sign in. Please try again.';
        }).finally(function () {
            $scope.isSubmitting = false;
        });

        console.log('accoutController result', JSON.stringify(result, null, 2));
    };
});