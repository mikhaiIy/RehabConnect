/**
 *  Form Wizard
 */

'use strict';

$(function () {

  // Modern Wizard
  // --------------------------------------------------------------------
  const wizardModern = document.querySelector('.wizard-modern-example'),
    wizardModernBtnNextList = [].slice.call(wizardModern.querySelectorAll('.btn-next')),
    wizardModernBtnPrevList = [].slice.call(wizardModern.querySelectorAll('.btn-prev')),
    wizardModernBtnSubmit = wizardModern.querySelector('.btn-submit');
  if (typeof wizardModern !== undefined && wizardModern !== null) {
    const modernStepper = new Stepper(wizardModern, {
      linear: false
    });
    if (wizardModernBtnNextList) {
      wizardModernBtnNextList.forEach(wizardModernBtnNext => {
        wizardModernBtnNext.addEventListener('click', event => {
          modernStepper.next();
        });
      });
    }
    if (wizardModernBtnPrevList) {
      wizardModernBtnPrevList.forEach(wizardModernBtnPrev => {
        wizardModernBtnPrev.addEventListener('click', event => {
          modernStepper.previous();
        });
      });
    }
    if (wizardModernBtnSubmit) {
      wizardModernBtnSubmit.addEventListener('click', event => {
        alert('Submitted..!!');
      });
    }
  }
});
