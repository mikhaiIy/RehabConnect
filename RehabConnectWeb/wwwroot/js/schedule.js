var dataTable;

$(document).ready(function () {
  loadDataTable();
});

function loadDataTable() {
  dataTable = $('#tblData').DataTable({
    "ajax": { url: '/admin/schedule/getall' },
    "columns": [
      { "data": 'date', "width": '20%' },
      { "data": 'startTime', "width": '20%' },
      { "data": 'endTime', "width": '20%' },
      { "data": 'duration', "width": '20%' },
      {
        "data": 'scheduleID',
        "render": function (data) {
          return `
            <div class="text-center">
              <a onclick="deleteConfirmation('${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                <i class="bi bi-trash-fill"></i> Delete
              </a>
            </div>`;
        },
        "width": "20%"
      }
    ]
  });
}

function deleteConfirmation(id) {
  Swal.fire({
    title: "Are you sure?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!"
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        type: "POST",
        url: '/Admin/Schedule/Delete/' + id,
        success: function (data) {
          if (data.success) {
            Swal.fire({
              title: "Deleted!",
              text: "Slots have been deleted.",
              icon: "success"
            });
            dataTable.ajax.reload();
          } else {
            Swal.fire({
              title: "Error!",
              text: data.message,
              icon: "error"
            });
          }
        },
        error: function () {
          Swal.fire({
            title: "Error!",
            text: "Failed to delete schedule.",
            icon: "error"
          });
        }
      });
    }
  });
}
