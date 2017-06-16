function deletefunc(id) {
    swal({
            title: "Are you sure?",
            text: "Are you sure that you want to delete this Item?",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "Yes, delete it!",
            confirmButtonColor: "#ec6c62"
        },
        function() {
            $.ajax({
                    url: "/Products/Delete/" + id,
                    type: "POST"
                })
                .done(function(data) {
                    sweetAlert({
                            title: "Deleted!",
                            text: "Your Item was successfully deleted!",
                            type: "success"
                        },
                        function() {
                            window.location.href = '/Products';
                        });
                })
                .error(function(data) {
                    swal("Oops", "We couldn't connect to the server!", "error");
                });
        });
}

/* Custom filtering function which will search data in column four between two values */
$.fn.dataTable.ext.search.push(
    function(settings, data, dataIndex) {
        var min = parseInt($('#min').val(), 10);
        var max = parseInt($('#max').val(), 10);
        var Price = parseFloat(data[4]) || 0; // use data for the age column

        if ((isNaN(min) && isNaN(max)) ||
            (isNaN(min) && Price <= max) ||
            (min <= Price && isNaN(max)) ||
            (min <= Price && Price <= max)) {
            return true;
        }
        return false;
    }
);

$(document).ready(function() {
    var table = $('#myTable').DataTable({
        // section responsible for footer filteration
        initComplete: function() {
            this.api().columns().every(function() {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change',
                        function() {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );

                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });

                column.data().unique().sort().each(function(d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        },
        dom: 'Bfrtlp',
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "aoColumnDefs":
        [
            { "bSortable": false, "aTargets": [-1] },
            { targets: -3, visible: false },
            { targets: -6, visible: false },
            { targets: -7, visible: false }
        ],
        buttons: [
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"></i>',
                titleAttr: 'Copy'
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i>',
                titleAttr: 'Excel'
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o"></i>',
                titleAttr: 'CSV'
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"></i>',
                titleAttr: 'PDF'
            },
            {
                extend: 'colvis'
            }
        ]
    });

    // Event listener to the two range filtering inputs to redraw on input
    $('#min, #max').keyup(function() {
        table.draw();
    });
});