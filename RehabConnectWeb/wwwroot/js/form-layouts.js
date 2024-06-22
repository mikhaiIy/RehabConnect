/**
 * Form Layout Vertical
 */
'use strict';

(function () {
  const phoneMaskList = document.querySelectorAll('.phone-mask'),
    creditCardMask = document.querySelector('.credit-card-mask'),
    expiryDateMask = document.querySelector('.expiry-date-mask'),
    cvvMask = document.querySelector('.cvv-code-mask'),
    datepickerList = document.querySelectorAll('.dob-picker'),
    formCheckInputPayment = document.querySelectorAll('.form-check-input-payment');

  // Phone Number
  if (phoneMaskList) {
    phoneMaskList.forEach(function (phoneMask) {
      new Cleave(phoneMask, {
        phone: true,
        phoneRegionCode: 'US'
      });
    });
  }

  // Credit Card
  if (creditCardMask) {
    new Cleave(creditCardMask, {
      creditCard: true,
      onCreditCardTypeChanged: function (type) {
        if (type != '' && type != 'unknown') {
          document.querySelector('.card-type').innerHTML =
            '<img src="' + assetsPath + 'img/icons/payments/' + type + '-cc.png" height="28"/>';
        } else {
          document.querySelector('.card-type').innerHTML = '';
        }
      }
    });
  }

  // Expiry Date Mask
  if (expiryDateMask) {
    new Cleave(expiryDateMask, {
      date: true,
      delimiter: '/',
      datePattern: ['m', 'y']
    });
  }

  // CVV
  if (cvvMask) {
    new Cleave(cvvMask, {
      numeral: true,
      numeralPositiveOnly: true
    });
  }

  // Flat Picker Birth Date
  if (datepickerList) {
    datepickerList.forEach(function (datepicker) {
      datepicker.flatpickr({
        monthSelectorType: 'static'
      });
    });
  }

  // Toggle CC Payment Method based on selected option
  if (formCheckInputPayment) {
    formCheckInputPayment.forEach(function (paymentInput) {
      paymentInput.addEventListener('change', function (e) {
        const paymentInputValue = e.target.value;
        if (paymentInputValue === 'credit-card') {
          document.querySelector('#form-credit-card').classList.remove('d-none');
        } else {
          document.querySelector('#form-credit-card').classList.add('d-none');
        }
      });
    });
  }

  // Initialize accordion functionality
  const accordionItems = document.querySelectorAll('.accordion-button');
  accordionItems.forEach(function (accordionItem) {
    accordionItem.addEventListener('click', function () {
      const panel = accordionItem.nextElementSibling;
      if (panel.style.display === "block") {
        panel.style.display = "none";
      } else {
        panel.style.display = "block";
      }
    });
  });

  // Form validation
  const form = document.querySelector('form');
  form.addEventListener('submit', function (e) {
    let valid = true;

    // Name validation
    const name = document.querySelector('[asp-for="csName"]').value;
    if (name.trim() === '') {
      valid = false;
      alert('Name is required.');
    }

    // NRIC validation
    const nric = document.querySelector('[asp-for="csic"]').value;
    const nricPattern = /^\d{6}-\d{2}-\d{4}$/;
    if (!nricPattern.test(nric)) {
      valid = false;
      alert('Invalid NRIC format. Expected format: xxxxxx-xx-xxxx');
    }

    // Age validation
    const age = document.querySelector('[asp-for="csAge"]').value;
    if (isNaN(age) || age <= 0) {
      valid = false;
      alert('Age must be a positive number.');
    }

    // Email validation
    const email = document.querySelector('[asp-for="csEmail"]').value;
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(email)) {
      valid = false;
      alert('Invalid email format.');
    }

    // Address validation
    const address = document.querySelector('[asp-for="csAddress"]').value;
    if (address.trim() === '') {
      valid = false;
      alert('Address is required.');
    }

    // Phone number validation
    const phone = document.querySelector('[asp-for="csPhoneNum"]').value;
    const phonePattern = /^\d{3} \d{3} \d{4}$/;
    if (!phonePattern.test(phone)) {
      valid = false;
      alert('Invalid phone number format. Expected format: 658 799 8941');
    }

    // Prevent form submission if validation fails
    if (!valid) {
      e.preventDefault();
    }
  });
})();

// select2 (jquery)
$(function () {
  // Form sticky actions
  var topSpacing;
  const stickyEl = $('.sticky-element');

  // Init custom option check
  window.Helpers.initCustomOptionCheck();

  // Set topSpacing if the navbar is fixed
  if (Helpers.isNavbarFixed()) {
    topSpacing = $('.layout-navbar').height() + 7;
  } else {
    topSpacing = 0;
  }

  // sticky element init (Sticky Layout)
  if (stickyEl.length) {
    stickyEl.sticky({
      topSpacing: topSpacing,
      zIndex: 9
    });
  }

  // Select2 Country
  var select2 = $('.select2');
  if (select2.length) {
    select2.each(function () {
      var $this = $(this);
      $this.wrap('<div class="position-relative"></div>').select2({
        placeholder: 'Select value',
        dropdownParent: $this.parent()
      });
    });
  }
});
