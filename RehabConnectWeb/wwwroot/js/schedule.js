/**
 * DataTables Advanced (jquery)
 */

'use strict';

$(function () {
  var dt_ajax_table = $('.datatables-ajax'),
    startDateEle = $('.start_date'),
    endDateEle = $('.end_date');

  // Datepicker for advanced filter
  var rangePickr = $('.flatpickr-range'),
    dateFormat = 'MM/DD/YYYY';

  if (rangePickr.length) {
    rangePickr.flatpickr({
      mode: 'range',
      dateFormat: 'm/d/Y',
      onClose: function (selectedDates) {
        var startDate = '',
          endDate = '';
        if (selectedDates[0] !== undefined) {
          startDate = moment(selectedDates[0]).format(dateFormat);
          startDateEle.val(startDate);
        }
        if (selectedDates[1] !== undefined) {
          endDate = moment(selectedDates[1]).format(dateFormat);
          endDateEle.val(endDate);
        }
        dt_ajax_table.DataTable().draw();
      }
    });
  }

  // Filter column-wise function
  function filterColumn(i, val) {
    if (i === 5) { // Assuming date column index is 5
      var startDate = startDateEle.val(),
        endDate = endDateEle.val();
      if (startDate !== '' && endDate !== '') {
        dt_ajax_table.DataTable().column(i).search(startDate + '|' + endDate, true, false).draw();
      }
    } else {
      dt_ajax_table.DataTable().column(i).search(val).draw();
    }
  }

  // Ajax Sourced Server-side for advanced filter table
  if (dt_ajax_table.length) {
    var dt_ajax = dt_ajax_table.DataTable({
      processing: true,
      serverSide: true,
      ajax: {
        url: '/Admin/Schedule/GetAll'
      },
      columns: [
        { data: 'date' },
        { data: 'startTime' },
        { data: 'endTime' },
        { data: 'duration' },
        { data: null }
      ],
      columnDefs: [
        {
          targets: -1,
          title: 'Action',
          orderable: false,
          searchable: false,
          render: function (data, type, full, meta) {
            return (
              '<div class="d-inline-block">' +
              '<a href="javascript:;" class="btn btn-sm btn-icon dropdown-toggle hide-arrow" data-bs-toggle="dropdown"><i class="text-primary ti ti-dots-vertical"></i></a>' +
              '<ul class="dropdown-menu dropdown-menu-end m-0">' +
              '<li><a href="javascript:;" class="dropdown-item">Details</a></li>' +
              '<li><a href="javascript:;" class="dropdown-item">Archive</a></li>' +
              '<div class="dropdown-divider"></div>' +
              '<li><a href="javascript:;" class="dropdown-item text-danger delete-record">Delete</a></li>' +
              '</ul>' +
              '</div>' +
              '<a href="javascript:;" class="btn btn-sm btn-icon item-edit"><i class="text-primary ti ti-pencil"></i></a>'
            );
          }
        }
      ],
      order: [[0, 'asc']], // Default sorting by date in ascending order
      dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>><"table-responsive"t><"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>'
    });
  }

  // Reset form control sizes after table initialization
  setTimeout(() => {
    $('.dataTables_filter .form-control').removeClass('form-control-sm');
    $('.dataTables_length .form-select').removeClass('form-select-sm');
  }, 200);
});

