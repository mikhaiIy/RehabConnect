var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/therapist/getall' },
        "columns": [
            { data: 'therapistName', "width": "25%" },
            { data: 'therapistIC', "width": "15%" },
            { data: 'therapistAge', "width": "10%" },
            { data: 'therapistSex', "width": "10%" },
            { data: 'therapistReligion', "width": "10%" },
            { data: 'therapistNationality', "width": "10%" },
            { data: 'therapistPhoneNum', "width": "15%" },
            { data: 'therapistAddress', "width": "20%" },
            { data: 'therapistEmail', "width": "20%" },
            {
                data: 'therapistID',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/therapist/upsert?therapistID=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/admin/therapist/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`;
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
