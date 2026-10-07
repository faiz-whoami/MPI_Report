app.controller('homeController', function ($scope, homeService) {
    $scope.dashboard = window.dashboardData || {
        Jobs: [],
        SelectedJobId: null,
        SelectedJobNo: '',
        InventoryCount: 0,
        OpenCorrectiveActionCount: 0,
        DailyMeetingCount: 0,
        ChecklistCount: 0,
        MpiReportCount: 0,
        SelectJobUrl: '/Home/SelectJob'
    };

    $scope.selectedJobId = $scope.dashboard.SelectedJobId;
    $scope.antiForgeryToken = $scope.dashboard.AntiForgeryToken || '';

    $scope.selectJob = function () {
        if (!$scope.selectedJobId) {
            return;
        }

        homeService.selectJob(
            $scope.dashboard.SelectJobUrl,
            $scope.selectedJobId,
            $scope.antiForgeryToken
        ).then(function () {
            window.location.href = $scope.dashboard.IndexUrl || '/Home/Index';
        }, function () {
            window.location.reload();
        });
    };
});