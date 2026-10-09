
window.dashboardData = {
    stats: [
        {
            id: 1,
            label: 'Total Reports',
            value: 248,
            description: 'All inspection reports',
            icon: 'glyphicon-file',
            iconClass: 'kpi-blue',
            trend: 'Overview'
        },
        {
            id: 2,
            label: 'Completed',
            value: 186,
            description: 'Successfully completed',
            icon: 'glyphicon-ok-circle',
            iconClass: 'kpi-green',
            trend: '75% of total'
        },
        {
            id: 3,
            label: 'Pending',
            value: 42,
            description: 'Awaiting completion',
            icon: 'glyphicon-time',
            iconClass: 'kpi-orange',
            trend: 'Needs attention'
        },
        {
            id: 4,
            label: 'Customers',
            value: 64,
            description: 'Registered customers',
            icon: 'glyphicon-user',
            iconClass: 'kpi-purple',
            trend: 'All customers'
        }
    ],

    charts: {
        status: {
            labels: ['Completed', 'Pending', 'In Progress', 'Rejected'],
            values: [186, 42, 15, 5],
            colors: ['#16a34a', '#f59e0b', '#3b82f6', '#ef4444']
        },

        monthly: {
            labels: [
                'Jan', 'Feb', 'Mar', 'Apr',
                'May', 'Jun', 'Jul', 'Aug',
                'Sep', 'Oct', 'Nov', 'Dec'
            ],
            values: [18, 24, 20, 31, 27, 35, 29, 38, 32, 0, 0, 0]
        }
    }
};