/**
 *  Form Wizard
 */

'use strict';

$(function () {
  const select2 = $('.select2'),
    selectPicker = $('.selectpicker');

  // Bootstrap select
  if (selectPicker.length) {
    selectPicker.selectpicker();
  }

  // select2
  if (select2.length) {
    select2.each(function () {
      var $this = $(this);
      $this.wrap('<div class="position-relative"></div>');
      $this.select2({
        placeholder: 'Select value',
        dropdownParent: $this.parent()
      });
    });
  }
});

(function () {

  document.addEventListener('DOMContentLoaded', function () {
    var activeStepId = stepIdToActivate; // Replace with the actual active step ID

    var activeStep = document.getElementById('step-' + activeStepId);
    if (activeStep) {
      activeStep.classList.add('active-step');

      // Apply styles to icon and label
      var icon = activeStep.querySelector('.bs-stepper-icon svg');
      var label1 = activeStep.querySelector('.label1');
      var label2 = activeStep.querySelector('.label2');
      var label3 = activeStep.querySelector('.label3');
      if (icon) {
        icon.style.fill = '#7367f0';
      }
      if (label1) {
        label1.style.color = '#7367f0';
      }
      if (label2) {
        label2.style.color = '#7367f0';
      }
      if (label3) {
        label3.style.color = '#7367f0';
        label3.textContent = 'Ongoing';
      }
    }

    for(let i = activeStepId-1; i >= 0; i--){
      var stepBefore = document.getElementById('step-' + i);
      if (stepBefore) {
        stepBefore.classList.add('step-before');

        // Apply styles to icon and label
        var iconB = stepBefore.querySelector('.bs-stepper-icon svg');
        var label1B = stepBefore.querySelector('.label1');
        var label2B = stepBefore.querySelector('.label2');
        var label3B = stepBefore.querySelector('.label3');
        if (iconB) {
          iconB.style.fill = '#579357';
        }
        if (label1B) {
          label1B.style.color = '#579357';
        }
        if (label2B) {
          label2B.style.color = '#579357';
        }
        if (label3B) {
          label3B.style.color = '#579357';
          label3B.textContent = 'Completed';
        }
      }
    }

  });
})();
