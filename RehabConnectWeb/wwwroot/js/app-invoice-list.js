/**
 * App Invoice List (jquery)
 */

'use strict';

$(function () {
  // Variable declaration for table
  var dt_invoice_table = $('.invoice-list-table');

  // Invoice datatable
  if (dt_invoice_table.length) {
    var dt_invoice = dt_invoice_table.DataTable({
      ajax: {
        url: '/Admin/Invoice/GetInvoices', // Endpoint to fetch invoice data
        type: 'GET',
        dataType: 'json',
        dataSrc: function (json) {
          return json.data.invoice;
        }
      },
      columns: [
        { data: null }, // For the expand/collapse control
        { data: 'invoiceID' }, // Invoice ID
        { data: 'parentDetail.fatherName' }, // Father's Name
        { data: 'parentDetail.motherName' }, // Mother's Name
        { data: 'totalAmount' }, // Total amount
        { data: 'dateIssued' }, // Issued date
        { data: 'parentDetail.householdIncome' }, // Household Income
        { data: 'totalAmount' }, // Total amount for the tooltip
        { data: null } // Actions
      ],
      columnDefs: [
        {
          // For Responsive
          className: 'control',
          responsivePriority: 2,
          searchable: false,
          targets: 0,
          render: function () {
            return '';
          }
        },
        {
          // Invoice ID
          targets: 1,
          render: function (data, type, full) {
            var invoice_id = full['invoiceID'];
            return '<a href="/Admin/Invoice/Preview/' + invoice_id + '">#' + invoice_id + '</a>';
          }
        },
        {
          // Father's Name
          targets: 2,
          render: function (data, type, full) {
            return full['parentDetail']['fatherName'];
          }
        },
        {
          // Mother's Name
          targets: 3,
          render: function (data, type, full) {
            return full['parentDetail']['motherName'];
          }
        },
        {
          // Total Invoice Amount
          targets: 4,
          render: function (data, type, full) {
            var total = full['totalAmount'];
            return '<span class="d-none">' + total + '</span>$' + total;
          }
        },
        {
          // Issued Date
          targets: 5,
          render: function (data, type, full) {
            var issued_date = new Date(full['dateIssued']);
            return '<span class="d-none">' + moment(issued_date).format('YYYYMMDD') + '</span>' + moment(issued_date).format('DD MMM YYYY');
          }
        },
        {
          // Household Income
          targets: 6,
          orderable: false,
          render: function (data, type, full) {
            var householdIncome = full['parentDetail']['householdIncome'];
            return '<span class="d-none">' + householdIncome + '</span>$' + householdIncome;
          }
        },
        {
          // Total Amount for Tooltip
          targets: 7,
          visible: false,
          render: function (data, type, full) {
            var totalAmount = full['totalAmount'];
            var due_date = moment(full['dateIssued']).format('DD MMM YYYY');
            var balance = full['parentDetail']['householdIncome'];
            var roleBadgeObj = {
              Sent: '<span class="badge badge-center rounded-pill bg-label-secondary w-px-30 h-px-30"><i class="ti ti-circle-check ti-sm"></i></span>',
              Draft: '<span class="badge badge-center rounded-pill bg-label-primary w-px-30 h-px-30"><i class="ti ti-device-floppy ti-sm"></i></span>',
              'Past Due': '<span class="badge badge-center rounded-pill bg-label-danger w-px-30 h-px-30"><i class="ti ti-info-circle ti-sm"></i></span>',
              'Partial Payment': '<span class="badge badge-center rounded-pill bg-label-success w-px-30 h-px-30"><i class="ti ti-circle-half-2 ti-sm"></i></span>',
              Paid: '<span class="badge badge-center rounded-pill bg-label-warning w-px-30 h-px-30"><i class="ti ti-chart-pie ti-sm"></i></span>',
              Downloaded: '<span class="badge badge-center rounded-pill bg-label-info w-px-30 h-px-30"><i class="ti ti-arrow-down-circle ti-sm"></i></span>'
            };
            return (
              "<span data-bs-toggle='tooltip' data-bs-html='true' title='<span>" +
              totalAmount +
              '<br> <span class="fw-medium">Balance:</span> ' +
              balance +
              '<br> <span class="fw-medium">Due Date:</span> ' +
              due_date +
              "</span>'>" +
              roleBadgeObj[totalAmount] +
              '</span>'
            );
          }
        },
        {
          // Actions
          targets: -1,
          title: 'Actions',
          searchable: false,
          orderable: false,
          render: function (data, type, full) {
            return (
              '<div class="d-flex align-items-center">' +
              '<a href="javascript:;" data-bs-toggle="tooltip" class="text-body" data-bs-placement="top" title="Send Mail"><i class="ti ti-mail mx-2 ti-sm"></i></a>' +
              '<a href="/Admin/Invoice/Preview/' + full['invoiceID'] + '" data-bs-toggle="tooltip" class="text-body" data-bs-placement="top" title="Preview Invoice"><i class="ti ti-eye mx-2 ti-sm"></i></a>' +
              '<div class="dropdown">' +
              '<a href="javascript:;" class="btn dropdown-toggle hide-arrow text-body p-0" data-bs-toggle="dropdown"><i class="ti ti-dots-vertical ti-sm"></i></a>' +
              '<div class="dropdown-menu dropdown-menu-end">' +
              '<a href="/Admin/Invoice/Download/' + full['invoiceID'] + '" class="dropdown-item">Download</a>' +
              '<a href="/Admin/Invoice/Edit/' + full['invoiceID'] + '" class="dropdown-item">Edit</a>' +
              '<a href="/Admin/Invoice/Duplicate/' + full['invoiceID'] + '" class="dropdown-item">Duplicate</a>' +
              '<div class="dropdown-divider"></div>' +
              '<a href="javascript:;" class="dropdown-item delete-record text-danger">Delete</a>' +
              '</div>' +
              '</div>' +
              '</div>'
            );
          }
        }
      ],
      order: [[1, 'desc']],
      dom:
        '<"row mx-1"' +
        '<"col-12 col-md-6 d-flex align-items-center justify-content-center justify-content-md-start gap-2"l<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start mt-md-0 mt-3"B>>' +
        '<"col-12 col-md-6 d-flex align-items-center justify-content-end flex-column flex-md-row pe-3 gap-md-3"f<"invoice_status mb-3 mb-md-0">>' +
        '>t' +
        '<"row mx-2"' +
        '<"col-sm-12 col-md-6"i>' +
        '<"col-sm-12 col-md-6"p>' +
        '>',
      language: {
        sLengthMenu: 'Show _MENU_',
        search: '',
        searchPlaceholder: 'Search Invoice',
        paginate: {
          previous: '&nbsp;',
          next: '&nbsp;'
        }
      },
      // Buttons with Dropdown
      buttons: [
        {
          text: '<i class="ti ti-plus me-md-1"></i><span class="d-md-inline-block d-none">Create Invoice</span>',
          className: 'btn btn-primary waves-effect waves-light',
          action: function () {
            window.location = '/Admin/Invoice/Add';
          }
        }
      ],
      // For responsive popup
      responsive: {
        details: {
          display: $.fn.dataTable.Responsive.display.modal({
            header: function (row) {
              var data = row.data();
              return 'Details of ' + data['parentDetail.fatherName'] + ' ' + data['parentDetail.motherName'];
            }
          }),
          type: 'column',
          renderer: function (api, rowIdx, columns) {
            var data = $.map(columns, function (col, i) {
              return col.title !== '' // ? Do not show row in modal popup if title is blank (for check box)
                ? '<tr data-dt-row="' +
                col.rowIndex +
                '" data-dt-column="' +
                col.columnIndex +
                '">' +
                '<td>' +
                col.title +
                ':' +
                '</td> ' +
                '<td>' +
                col.data +
                '</td>' +
                '</tr>'
                : '';
            }).join('');

            return data ? $('<table class="table"/><tbody />').append(data) : false;
          }
        }
      },
      initComplete: function () {
        // Adding role filter once table initialized
        this.api()
          .columns(7)
          .every(function () {
            var column = this;
            var select = $(
              '<select id="UserRole" class="form-select"><option value=""> Select Status </option></select>'
            )
              .appendTo('.invoice_status')
              .on('change', function () {
                var val = $.fn.dataTable.util.escapeRegex($(this).val());
                column.search(val ? '^' + val + '$' : '', true, false).draw();
              });

            column
              .data()
              .unique()
              .sort()
              .each(function (d, j) {
                select.append('<option value="' + d + '" class="text-capitalize">' + d + '</option>');
              });
          });
      }
    });
  }

  // On each datatable draw, initialize tooltip
  dt_invoice_table.on('draw.dt', function () {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
      return new bootstrap.Tooltip(tooltipTriggerEl, {
        boundary: document.body
      });
    });
  });

  // Delete Record
  $('.invoice-list-table tbody').on('click', '.delete-record', function () {
    dt_invoice.row($(this).parents('tr')).remove().draw();
  });

  // Filter form control to default size
  // ? setTimeout used for multilingual table initialization
  setTimeout(() => {
    $('.dataTables_filter .form-control').removeClass('form-control-sm');
    $('.dataTables_length .form-select').removeClass('form-select-sm');
  }, 300);
});
