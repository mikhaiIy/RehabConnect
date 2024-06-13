'use strict';

document.addEventListener('DOMContentLoaded', function (e) {
  (function () {
    const deleteButtons = document.querySelectorAll('.delete-transaction');
    deleteButtons.forEach(deleteButton => {
      deleteButton.addEventListener('click', function (e) {
        e.preventDefault();
        const userName = this.getAttribute('data-transaction-username');
        Swal.fire({
          title: 'Delete Transaction?',
          html: `<p class="text-danger">Are you sure you want to delete transaction of ?<br> <span class="fw-medium text-body">${userName}</span></p>`,
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'Delete',
          cancelButtonText: 'Cancel',
          customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-secondary'
          }
        }).then(result => {
          if (result.isConfirmed) {
            window.location.href = this.getAttribute('href'); //redirect to href
          } else {
            Swal.fire({
              title: 'Cancelled',
              html: `<p>Did not delete <span class="fw-medium text-primary">${userName}</span> Transaction!</p>`,
              icon: 'error',
              confirmButtonText: 'Ok',
              customClass: {
                confirmButton: 'btn btn-success'
              }
            });
          }
        });
      });
    });
  })();
});
