app.service('homeService', function ($http) {
    this.getDashboard = function () {
        return $http({
            method: 'GET',
            url: '/Home/Index',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        });
    };

    this.selectJob = function (selectJobUrl, jobId, antiForgeryToken) {
        var formData = new URLSearchParams();
        formData.append('jobId', jobId);
        formData.append('__RequestVerificationToken', antiForgeryToken);

        return $http({
            method: 'POST',
            url: selectJobUrl,
            data: formData.toString(),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            }
        });
    };
});