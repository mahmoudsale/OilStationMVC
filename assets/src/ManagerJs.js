var Manager = {
    GetAllData: function (ServiceUrl, Parameters) {
        var Obj;
        var ServiceUrl = ServiceUrl;
        if (Parameters) {
            window.Manager.GetApi(ServiceUrl, OnSuccess, OnFailed, Parameters);
        }
        else {
            window.Manager.GetApiWithoutParameter(ServiceUrl, OnSuccess, OnFailed);
        }

        function OnSuccess(JsonData) {
            Obj = JsonData;
        }
        function OnFailed(Error) {
            //alert(Error.status);
        }
        return Obj;
    },

    GetApi: function (ServiceUrl, SuccessCallBack, ErrorCallBack, Parameters) {
        $.ajax({
            type: 'GET',
            data: jQuery.param(Parameters),
            url: ServiceUrl,
            async: false,
            dataType: 'json',
            success: SuccessCallBack,
            error: ErrorCallBack
        });
    },

    GetApiWithoutParameter: function (ServiceUrl, SuccessCallBack, ErrorCallBack, Parameters) {
        $.ajax({
            type: 'GET',
            url: ServiceUrl,
            async: false,
            dataType: 'json',
            success: SuccessCallBack,
            error: ErrorCallBack
        });
    },

    DrawBarChart: function (ChartId,ServiceUrl) {
        var areaChartData;
        areaChartData = Manager.GetAllData(ServiceUrl, null);
        var barChartCanvas = $('#'+ChartId).get(0).getContext('2d')
        var barChartData = jQuery.extend(true, {}, areaChartData)
        var temp0 = areaChartData.datasets[0]
        var temp1 = areaChartData.datasets[1]
        barChartData.datasets[0] = temp1
        barChartData.datasets[1] = temp0
        barChartData.labels = areaChartData.labels
        var barChartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            datasetFill: false
        }

        var barChart = new Chart(barChartCanvas, {
            type: 'bar',
            data: barChartData,
            options: barChartOptions, 
        })
    },

    //can change type of chart to pie-line-bar-doune chart
    DrawPieChrt: function (ChartId,ChartType,ServiceUrl){
 
        var donutData1 = {
            labels: [
                'Chrome',
                'IE',
                'FireFox',
                'Safari',
                'Opera',
                'Navigator',
            ],
            datasets: [
                {
                    data: [700, 500, 400, 600, 300, 100],
                    backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'],
                }
            ]
        }

        var donutData = Manager.GetAllData(ServiceUrl, null);
        console.log(donutData);
        var donutOptions = {
            maintainAspectRatio: false,
            responsive: true,
        }
        
        // Get context with jQuery - using jQuery's .get() method.
        var pieChartCanvas = $('#' + ChartId).get(0).getContext('2d')
        var pieData = donutData;
        var pieOptions = {
            maintainAspectRatio: false,
            responsive: true,
        }
        //Create pie or douhnut chart
        // You can switch between pie and douhnut using the method below.
        var pieChart = new Chart(pieChartCanvas, {
            //type: 'bar',
            type: ChartType, 
            data: pieData,
            options: pieOptions
        })
    },

    

}
