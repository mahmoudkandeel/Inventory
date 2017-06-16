var datachart = {
    labels: [],
    datasets: [
        {
            label: "Orders Count",
            backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
            borderWidth: 1,
            data: []
        }
    ]
};

$.getJSON("/Home/GetCustomerOrdersData/",
    function (data) {
        $.each(data,
            function (i, item) {
                datachart.labels.push(item.Name);
                datachart.datasets[0].data.push(item.OrderCount);
            });
        var ctx = document.getElementById("CustomerOrdersChart").getContext("2d");

        var myBarChart = new Chart(ctx,
            {
                type: 'bar',
                data: datachart,
                options: {
                    legend: { display: false },
                    title: {
                        display: false,
                        text: 'Customer Orders'
                    }
                }
            });
    });