/**
 * DataTables Advanced (jquery)
 */

'use strict';

$(function () {
  var dt_ajax_table = $('.datatables-ajax');

  if (dt_ajax_table.length) {
    dt_ajax_table.DataTable({
      processing: true,
      serverSide: true,
      ajax: {
        url: '/Admin/Schedule/GetAll',
        type: 'GET',
        data: function (d) {
          d.draw = d.draw || 1;
          d.start = d.start || 0;
          d.length = d.length || 10;
        }
      },
      columns: [
        { data: 'date' },
        { data: 'startTime' },
        { data: 'endTime' },
        { data: 'duration' },
        {
          data: null,
          render: function (data, type, full, meta) {
            return '<div class="btn-group">' +
              '<button class="btn btn-primary dropdown-toggle" data-bs-toggle="dropdown">Actions</button>' +
              '<ul class="dropdown-menu">' +
              '<li><a class="dropdown-item" href="#">Details</a></li>' +
              '<li><a class="dropdown-item" href="#">Archive</a></li>' +
              '<li><a class="dropdown-item text-danger delete-record" href="#">Delete</a></li>' +
              '</ul></div>';
          },
          orderable: false,
          searchable: false
        }
      ],
      order: [[0, 'asc']],
      dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>><"table-responsive"t><"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>'
    });
  }
});



