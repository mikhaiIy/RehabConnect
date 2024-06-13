'use strict';

document.addEventListener('DOMContentLoaded', function (e) {
  (function () {
    let fv; // Declare fv in a scope accessible to both initialization and event listeners

    // Variables for DataTable
    var TransactionDate = $('.transaction-date');
    var DueDate = $('.due-date');
    var select2 = $('.select2');

    if (select2.length) {
      select2.each(function () {
        var $this = $(this);
        $this.wrap('<div class="position-relative"></div>').select2({
          placeholder: 'Select Status',
          dropdownParent: $this.parent()
        });

        // Add event listener to clear validation messages on input change
        $this.on('change', function () {
          fv.revalidateField('Status');
        });
      });
    }

    // Transaction Date & Due Date (flatpicker)
    if (TransactionDate) {
      TransactionDate.flatpickr({
        monthSelectorType: 'static',
        dateFormat: 'M j, Y',
        onClose: function () {
          fv.revalidateField('TransactionDate');
        }
      });
    }

    // DueDate (flatpicker)
    if (DueDate) {
      DueDate.flatpickr({
        monthSelectorType: 'static',
        dateFormat: 'M j, Y',
        onClose: function () {
          fv.revalidateField('DueDate');
        }
      });
    }

    const addTransactionForm = document.getElementById('addTransactionForm');
    if (addTransactionForm) {
      // Add New Customer Form Validation
      fv = FormValidation.formValidation(addTransactionForm, {
        fields: {
          Customer: {
            validators: {
              notEmpty: {
                message: 'Please enter Customer Name'
              }
            }
          },
          Status: {
            validators: {
              notEmpty: {
                message: 'Please select a Transaction Status'
              }
            }
          },
          Total: {
            validators: {
              notEmpty: {
                message: 'Please fill the amount'
              },
              regexp: {
                regexp: /^\d+(\.\d{1,2})?$/,
                message: 'Only 2 digits are allowed after the decimal point'
              }
            }
          },
          DueDate: {
            validators: {
              notEmpty: {
                message: 'Please select a Due Date'
              },
              callback: {
                message: 'Due Date should be equal to or later than Transaction Date',
                callback: function (input) {
                  const dueDate = input.value;
                  const transactionDate = TransactionDate.val(); // Use .val() to get the value of jQuery element

                  if (new Date(dueDate) >= new Date(transactionDate)) {
                    return true;
                  }

                  return false;
                }
              }
            }
          },
          TransactionDate: {
            validators: {
              notEmpty: {
                message: 'Please select a Transaction Date'
              }
            }
          }
        },
        plugins: {
          trigger: new FormValidation.plugins.Trigger(),
          bootstrap5: new FormValidation.plugins.Bootstrap5({
            eleValidClass: '',
            rowSelector: '.mb-3'
          }),
          submitButton: new FormValidation.plugins.SubmitButton(),

          defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
          autoFocus: new FormValidation.plugins.AutoFocus()
        }
      });
    }

    // Update transaction form validation
    const UpdateTransactionForm = document.getElementById('UpdateTransactionForm');
    if (UpdateTransactionForm) {
      fv = FormValidation.formValidation(UpdateTransactionForm, {
        fields: {
          Customer: {
            validators: {
              notEmpty: {
                message: 'Please enter Customer Name'
              }
            }
          },
          Status: {
            validators: {
              notEmpty: {
                message: 'Please select a Transaction Status'
              }
            }
          },
          Total: {
            validators: {
              notEmpty: {
                message: 'Please fill the amount'
              },
              regexp: {
                regexp: /^\d+(\.\d{1,2})?$/,
                message: 'Only 2 digits are allowed after the decimal point'
              }
            }
          },
          DueDate: {
            validators: {
              notEmpty: {
                message: 'Please select a Due Date'
              },
              callback: {
                message: 'Due Date should be equal to or later than Transaction Date',
                callback: function (input) {
                  const dueDate = input.value;
                  const transactionDate = TransactionDate.val(); // Use .val() to get the value of jQuery element

                  if (new Date(dueDate) >= new Date(transactionDate)) {
                    return true;
                  }

                  return false;
                }
              }
            }
          },
          TransactionDate: {
            validators: {
              notEmpty: {
                message: 'Please select a Transaction Date'
              }
            }
          }
        },
        plugins: {
          trigger: new FormValidation.plugins.Trigger(),
          bootstrap5: new FormValidation.plugins.Bootstrap5({
            eleValidClass: '',
            rowSelector: '.mb-3'
          }),
          submitButton: new FormValidation.plugins.SubmitButton(),

          defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
          autoFocus: new FormValidation.plugins.AutoFocus()
        }
      });
    }
  })();
});
