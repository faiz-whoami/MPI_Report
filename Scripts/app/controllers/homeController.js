app.controller('homeController', function ($scope, homeService) {
    $scope.dashboard = {
        Jobs: [],
        SelectedJobId: null,
        SelectedJobNo: '',
        InventoryCount: 0,
        OpenCorrectiveActionCount: 0,
        DailyMeetingCount: 0,
        ChecklistCount: 0,
        MpiReportCount: 0,
        SelectJobUrl: '/Home/SelectJob',
        IndexUrl: '/Home/Index'
    };

    $scope.selectedJobId = null;
    $scope.antiForgeryToken = '';
    $scope.isLoading = true;
    $scope.loadError = '';

  homeService.getDashboard().then(function (response) {
        $scope.dashboard = angular.extend($scope.dashboard, response.data.dashboard);
        $scope.selectedJobId = $scope.dashboard.SelectedJobId;
        $scope.antiForgeryToken = response.data.antiForgeryToken || '';
        $scope.isLoading = false;
    }, function () {
        $scope.loadError = 'Unable to load the dashboard.';
        $scope.isLoading = false;
  });
    $scope.selectJob = function () {
        if (!$scope.selectedJobId) {
            return;
        }

        homeService.selectJob(
            $scope.dashboard.SelectJobUrl,
            $scope.selectedJobId,
            $scope.antiForgeryToken
        ).then(function () {
            window.location.href = $scope.dashboard.IndexUrl;
        }, function () {
            window.location.reload();
        });
    };
});