/**
 * Form Picker
 */

'use strict';





// color picker (pickr)
// --------------------------------------------------------------------
(function () {
  const classicPicker = document.querySelector('#color-picker-classic'),
    monolithPicker = document.querySelector('#color-picker-monolith'),
    nanoPicker = document.querySelector('#color-picker-nano');

  // classic
  if (classicPicker) {
    pickr.create({
      el: classicPicker,
      theme: 'classic',
      default: 'rgba(102, 108, 232, 1)',
      swatches: [
        'rgba(102, 108, 232, 1)',
        'rgba(40, 208, 148, 1)',
        'rgba(255, 73, 97, 1)',
        'rgba(255, 145, 73, 1)',
        'rgba(30, 159, 242, 1)'
      ],
      components: {
        // Main components
        preview: true,
        opacity: true,
        hue: true,

        // Input / output Options
        interaction: {
          hex: true,
          rgba: true,
          hsla: true,
          hsva: true,
          cmyk: true,
          input: true,
          clear: true,
          save: true
        }
      }
    });
  }

  // monolith
  if (monolithPicker) {
    pickr.create({
      el: monolithPicker,
      theme: 'monolith',
      default: 'rgba(40, 208, 148, 1)',
      swatches: [
        'rgba(102, 108, 232, 1)',
        'rgba(40, 208, 148, 1)',
        'rgba(255, 73, 97, 1)',
        'rgba(255, 145, 73, 1)',
        'rgba(30, 159, 242, 1)'
      ],
      components: {
        // Main components
        preview: true,
        opacity: true,
        hue: true,

        // Input / output Options
        interaction: {
          hex: true,
          rgba: true,
          hsla: true,
          hsva: true,
          cmyk: true,
          input: true,
          clear: true,
          save: true
        }
      }
    });
  }

  // nano
  if (nanoPicker) {
    pickr.create({
      el: nanoPicker,
      theme: 'nano',
      default: 'rgba(255, 73, 97, 1)',
      swatches: [
        'rgba(102, 108, 232, 1)',
        'rgba(40, 208, 148, 1)',
        'rgba(255, 73, 97, 1)',
        'rgba(255, 145, 73, 1)',
        'rgba(30, 159, 242, 1)'
      ],
      components: {
        // Main components
        preview: true,
        opacity: true,
        hue: true,

        // Input / output Options
        interaction: {
          hex: true,
          rgba: true,
          hsla: true,
          hsva: true,
          cmyk: true,
          input: true,
          clear: true,
          save: true
        }
      }
    });
  }
}


)();
