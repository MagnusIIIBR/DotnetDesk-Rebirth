var popup, dataTable;
var entity = 'Product';
var apiurl = '/api/' + entity;

$(document).ready(function () {
        var organizationId = $('#organizationId').val();
        dataTable = $('#grid').DataTable({
                "dom": 'Bfrtip',
                "ajax": {
                        "url": apiurl + '/' + organizationId,
                        "type": 'GET',
                        "datatype": 'json'
                },
                "buttons": {
                        buttons: [
                                {
                                        text: "Add New Product",
                                        action: function (e, dt, node, config) {
                                                ShowPopup(`/${entity}/AddEdit?org=${organizationId}`);
                                        }
                                }
                        ],
                        dom: {
                                button: {
                                        tag: "button",
                                        className: "btn btn-primary"
                                },
                                buttonLiner: {
                                        tag: null
                                }
                        }
                },
                "columnDefs": [{ "width": "10%", "targets": 1 }],
                "columns": [
                        { "data": "productName" },
                        {
                                "data": "productId",
                                "render": function (data) {
                                        var btnDetail = "<a href='/Customer/detail?customerId=" + data + "' class='btn btn-default'><i class='fa fa-list'></i></a>";
                                        var btnEdit = "<a class='btn btn-default' style='margin-left:2px' onclick=ShowPopup('/" + entity + "/AddEdit/" + data + "')><i class='fas fa-pencil-alt'></i></a>";
                                        var btnDelete = "<a class='btn btn-danger' style='margin-left:2px' onclick=Delete('" + data + "')><i class='fa fa-trash'></i></a>";
                                        return "<div class=\"btn-group-sm\" role=\"group\" aria-label=\"Basic example\">" + btnDetail + btnEdit + btnDelete + "</div>";
                                }
                        }
                ],
                "language": {
                        "emptyTable": "no data found."
                },
                "lengthChange": false,
        });
});

function ShowPopup(url) {
        var modalId = 'modalDefault';
        var modalPlaceholder = $('#' + modalId + ' .modal-dialog .modal-content');
        $.get(url)
                .done(function (response) {
                        modalPlaceholder.html(response);
                        popup = $('#' + modalId + '').modal({
                                keyboard: false,
                                backdrop: 'static'
                        });
                });
}


function SubmitAddEdit(form) {
        $.validator.unobtrusive.parse(form);
        if ($(form).valid()) {
                var data = $(form).serializeJSON();
                data = JSON.stringify(data);
                $.ajax({
                        type: 'POST',
                        url: apiurl,
                        data: data,
                        contentType: 'application/json',
                        success: function (data) {
                                if (data.success) {
                                        popup.modal('hide');
                                        ShowMessage(data.message);
                                        dataTable.ajax.reload();
                                } else {
                                        ShowMessageError(data.message);
                                }
                        }
                });

        }
        return false;
}

function Delete(id) {
        swal({
                title: "Are you sure want to Delete?",
                text: "You will not be able to restore the data!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#dd4b39",
                confirmButtonText: "Yes, delete it!",
                closeOnConfirm: true
        }, function () {
                $.ajax({
                        type: 'DELETE',
                        url: apiurl + '/' + id,
                        success: function (data) {
                                if (data.success) {
                                        ShowMessage(data.message);
                                        dataTable.ajax.reload();
                                } else {
                                        ShowMessageError(data.message);
                                }
                        }
                });
        });


}


