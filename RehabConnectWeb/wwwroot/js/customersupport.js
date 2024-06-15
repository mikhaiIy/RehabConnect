var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/customersupport/getall' },
        "columns": [
            { data: 'csName', "width": "25%" },
            { data: 'csic', "width": "15%" },
            { data: 'csAge', "width": "10%" },
            { data: 'csSex', "width": "10%" },
            { data: 'csReligion', "width": "10%" },
            { data: 'csNationality', "width": "10%" },
            { data: 'csPhoneNum', "width": "15%" },
            { data: 'csAddress', "width": "20%" },
            { data: 'csEmail', "width": "20%" },
            {
                data: 'csid',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/customersupport/upsert?csid=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/customersupport/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
