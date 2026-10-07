app.service('homeService', function ($http) {
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