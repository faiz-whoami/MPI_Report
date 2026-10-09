app.controller('homeController', function ($scope, $timeout, homeService) {

    // Dashboard data returned by the backend
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

    // Job selection
    $scope.selectedJobId = null;
    $scope.antiForgeryToken = '';

    // Loading and error states
    $scope.isLoading = true;
    $scope.loadError = '';

    // Dashboard cards
    $scope.cardList = [
        {
            title: 'Inventory',
            subtitle: 'Equipment & Materials',
            description: 'View and manage inventory items assigned to inspection jobs.',
            icon: 'glyphicon-hdd',
            iconClass: 'icon-blue',
            countKey: 'InventoryCount',
            countLabel: 'Items',
            date: 'Inventory overview',
            url: '/Inventory/Index'
        },
        {
            title: 'Checklists',
            subtitle: 'Inspection Tasks',
            description: 'Review inspection checklists and track completed inspection tasks.',
            icon: 'glyphicon-list-alt',
            iconClass: 'icon-purple',
            countKey: 'ChecklistCount',
            countLabel: 'Checklists',
            date: 'Checklist overview',
            url: '/Checklists/Index'
        },
        {
            title: 'Corrective Actions',
            subtitle: 'Issues & Follow-ups',
            description: 'Review outstanding corrective actions and monitor their progress.',
            icon: 'glyphicon-wrench',
            iconClass: 'icon-orange',
            countKey: 'OpenCorrectiveActionCount',
            countLabel: 'Open actions',
            date: 'Corrective action overview',
            url: '/CorrectiveActions/Index'
        },
    ];

    //Dashboard Data Rest
    var statusChart = null;
    var monthlyChart = null;

    // Keep existing loadDashboard() and doAction(item)
    // implementations in this controller.

    $scope.stats = window.dashboardData.stats;

    function renderCharts() {
        if (typeof Chart === 'undefined') {
            console.error('Chart.js has not been loaded.');
            return;
        }

        var statusCanvas =
            document.getElementById('statusChart');

        var monthlyCanvas =
            document.getElementById('monthlyChart');

        if (!statusCanvas || !monthlyCanvas) {
            return;
        }

        // Prevent duplicate charts after refresh.
        if (statusChart) {
            statusChart.destroy();
        }

        if (monthlyChart) {
            monthlyChart.destroy();
        }

        var statusData = window.dashboardData.charts.status;
        var monthlyData = window.dashboardData.charts.monthly;

        statusChart = new Chart(statusCanvas, {
            type: 'doughnut',

            data: {
                labels: statusData.labels,

                datasets: [{
                    data: statusData.values,
                    backgroundColor: statusData.colors,
                    borderWidth: 0,
                    hoverOffset: 6
                }]
            },

            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '68%',

                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: {
                            usePointStyle: true,
                            padding: 18
                        }
                    }
                }
            }
        });

        monthlyChart = new Chart(monthlyCanvas, {
            type: 'bar',

            data: {
                labels: monthlyData.labels,

                datasets: [{
                    label: 'Reports',
                    data: monthlyData.values,
                    backgroundColor: '#3b82f6',
                    hoverBackgroundColor: '#2563eb',
                    borderRadius: 5,
                    maxBarThickness: 30
                }]
            },

            options: {
                responsive: true,
                maintainAspectRatio: false,

                plugins: {
                    legend: {
                        display: false
                    }
                },

                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            precision: 0
                        },
                        grid: {
                            color: '#edf0f5'
                        }
                    },

                    x: {
                        grid: {
                            display: false
                        }
                    }
                }
            }
        });
    }

    $scope.refreshCharts = function () {
        $timeout(renderCharts, 0);
    };


    // Load dashboard data from the backend
    homeService.getDashboard().then(
        function (response) {
            $scope.dashboard = angular.extend(
                $scope.dashboard,
                response.data.dashboard || {}
            );

            $scope.selectedJobId = $scope.dashboard.SelectedJobId;
            $scope.antiForgeryToken =
                response.data.antiForgeryToken || '';

            $scope.isLoading = false;
            $scope.refreshCharts();
        },
        function () {
            $scope.loadError = 'Unable to load the dashboard.';
            $scope.isLoading = false;
        }
    );

    // Select a job and refresh the dashboard
    $scope.selectJob = function () {
        if (!$scope.selectedJobId) {
            return;
        }

        homeService.selectJob(
            $scope.dashboard.SelectJobUrl,
            $scope.selectedJobId,
            $scope.antiForgeryToken
        ).then(
            function () {
                window.location.href = $scope.dashboard.IndexUrl;
            },
            function () {
                $scope.loadError =
                    'Unable to select the job. Please try again.';
            }
        );
    };

    // Navigate to a card's page
    $scope.doAction = function (item) {
        if (item && item.url) {
            window.location.href = item.url;
        }
    };

    // Refresh dashboard data
    $scope.loadDashboard = function () {
        $scope.isLoading = true;
        $scope.loadError = '';

        homeService.getDashboard().then(
            function (response) {
                $scope.dashboard = angular.extend(
                    $scope.dashboard,
                    response.data.dashboard || {}
                );

                $scope.selectedJobId = $scope.dashboard.SelectedJobId;
                $scope.antiForgeryToken =
                    response.data.antiForgeryToken || '';

                $scope.isLoading = false;
                $scope.refreshCharts();
            },
            function () {
                $scope.loadError = 'Unable to load the dashboard.';
                $scope.isLoading = false;
            }
        );
    };
});
