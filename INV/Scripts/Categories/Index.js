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
        function () {
            $.ajax({
                url: "/Categories/Delete/" + id,
                    type: "POST"
                })
                .done(function (data) {
                    sweetAlert({
                            title: "Deleted!",
                            text: "Your Item was successfully deleted!",
                            type: "success"
                        },
                        function () {
                            window.location.href = '/Categories';
                        });
                })
                .error(function (data) {
                    swal("Oops", "We couldn't connect to the server!", "error");
                });
        });
}

$(document).ready(function () {
    var table = $('#myTable').DataTable({
        dom: 'Bfrtlp',
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "aoColumnDefs":
        [
            { "bSortable": false, "aTargets": [-1] }
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
});


