var chartdata = {
    labels: [],
    datasets: [
        {
            label: "Category Worth",
            backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
            borderWidth: 1,
            data: []
        }
    ]
};

$.getJSON("/Home/GetCategoryWorthData/",
    function(data) {
        $.each(data,
            function(i, item) {
                chartdata.labels.push(item.Name);
                chartdata.datasets[0].data.push(item.CategoryWorth);
            });
        var ctx = document.getElementById("CategoryWorth").getContext("2d");

        var myBarChart = new Chart(ctx,
            {
                type: 'pie',
                data: chartdata,
                options: {
                    title: {
                        display: false,
                        text: 'Category Worth'
                    }
                }
            });
    });