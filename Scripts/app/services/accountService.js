app.service('accountService', function ($http) {
    this.login = function (username, password, rememberMe, antiForgeryToken) {
        var formData = new URLSearchParams();
        formData.append('Username', username);
        formData.append('Password', password);
        formData.append('RememberMe', rememberMe);
        formData.append('__RequestVerificationToken', antiForgeryToken);

        return $http({
            method: 'POST',
            url: '/Account/Login',
            data: formData.toString(),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
                'X-Requested-With': 'XMLHttpRequest'
            }
        });
    };
});