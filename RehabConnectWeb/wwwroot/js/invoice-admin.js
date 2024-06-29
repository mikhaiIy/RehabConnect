// Assuming you have already fetched parent data and populated the <select> element
const apiUrl = '/Admin/Invoice/GetParent';
const programApiUrl = '/Admin/Invoice/GetProgramList';
const subtotalElement = document.getElementById('subtotal');
const totalElement = document.getElementById('total');

fetch(apiUrl)
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(userData => {
    const parentDetails = userData.data;
    const parents = parentDetails.map(parent => {
      const fatherName = parent.fatherName || '';
      const motherName = parent.motherName || '';
      const combinedName = `${fatherName} & ${motherName}`;
      return {
        parentIds: parent.parentID,
        combinedName,
        address1: parent.fatherAddress,
        address2: `${parent.fatherPostcode} ${parent.fatherCity}, ${parent.fatherCountry}`,
        phoneNum: parent.fatherPhoneNum,
        email: parent.fatherEmail
      };
    });

    const selectElement = document.getElementById('parentSelect');
    parents.forEach(parent => {
      const optionElement = document.createElement('option');
      optionElement.value = parent.parentIds;
      optionElement.textContent = parent.combinedName;
      selectElement.appendChild(optionElement);
    });

    const nameElement = document.querySelector('.mb-1:nth-child(2)');
    const address1Element = document.querySelector('.mb-1:nth-child(3)');
    const address2Element = document.querySelector('.mb-1:nth-child(4)');
    const phoneElement = document.querySelector('.mb-1:nth-child(5)');
    const emailElement = document.querySelector('.mb-1:nth-child(6)');

    nameElement.innerText = 'Name';
    address1Element.innerText = 'Address1';
    address2Element.innerText = 'Address2';
    phoneElement.innerText = 'Phone Number';
    emailElement.innerText = 'Email';

    selectElement.addEventListener('change', () => {
      const selectedParentId = selectElement.value;
      const numericParentId = parseInt(selectedParentId, 10);

      const selectedParent = parents.find(parent => parent.parentIds === numericParentId);
      if (selectedParent) {
        nameElement.innerText = selectedParent.combinedName;
        address1Element.innerText = selectedParent.address1;
        address2Element.innerText = selectedParent.address2;
        phoneElement.innerText = selectedParent.phoneNum;
        emailElement.innerText = selectedParent.email;
      } else {
        nameElement.innerText = 'FatherName';
        address1Element.innerText = 'FAddress1';
        address2Element.innerText = 'FAddress2';
        phoneElement.innerText = 'FPhone Number';
        emailElement.innerText = 'email';
      }

      fetchProgramsForParent(numericParentId);
    });
  })
  .catch(error => {
    console.error('Error fetching parent data:', error);
  });

function fetchProgramsForParent(parentId) {
  const programApiUrl = `/Admin/Invoice/GetProgramList?parentId=${parentId}`;

  fetch(programApiUrl)
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json();
    })
    .then(programData => {
      console.log('Program Data:', programData); // Add logging here
      const { steps, programList } = programData.data;
      populateProgramSelect(programList, steps);
    })
    .catch(error => {
      console.error('Error fetching program data:', error);
    });
}

function populateProgramSelect(programs, steps) {
  if (!Array.isArray(programs)) {
    console.error('Programs is not an array:', programs);
    return;
  }
  if (!Array.isArray(steps)) {
    console.error('Steps is not an array:', steps);
    return;
  }

  const programSelectElements = document.querySelectorAll('#ProgramSelect');
  console.log('Programs:', programs); // Add logging here
  console.log('Steps:', steps); // Add logging here

  const groupedPrograms = programs.reduce((acc, program) => {
    const step = steps.find(step => step.stepId === program.stepId);
    if (step && step.combinedPricing) {
      if (!acc[program.stepId]) {
        acc[program.stepId] = [];
      }
      acc[program.stepId].push(program.programName);
    } else {
      acc[program.programID] = [program.programName]; // Store individually if not combined
    }
    return acc;
  }, {});

  programSelectElements.forEach(programSelectElement => {
    programSelectElement.innerHTML = '<option selected disabled>Select Program</option>';

    Object.keys(groupedPrograms).forEach(key => {
      const optionElement = document.createElement('option');
      optionElement.value = key;
      optionElement.textContent = groupedPrograms[key].join(', ');
      programSelectElement.appendChild(optionElement);
    });

    programSelectElement.addEventListener('change', () => {
      const selectedProgramId = programSelectElement.value;
      const selectedStep = steps.find(step => step.stepId === parseInt(selectedProgramId, 10));
      const selectedProgram = programs.find(program => program.programID === parseInt(selectedProgramId, 10));

      const priceDisplayElement = programSelectElement.closest('.repeater-wrapper').querySelector('.price-display');

      if (selectedStep && selectedStep.combinedPricing) {
        priceDisplayElement.textContent = selectedStep.priceWeekday ? `$${selectedStep.priceWeekday}` : 'Price not available';
      } else {
        priceDisplayElement.textContent = selectedProgram && selectedProgram.priceWeekday ? `$${selectedProgram.priceWeekday}` : 'Price not available';
      }

      calculateSubtotalAndTotal(programSelectElements, programs); // Pass programs here
    });

  });
}

// Modify calculateSubtotalAndTotal to accept programs as a parameter
function calculateSubtotalAndTotal(programSelectElements, programs) {
  let subtotal = 0;

  programSelectElements.forEach(programSelectElement => {
    const selectedProgramId = programSelectElement.value;
    const selectedProgram = programs.find(program => program.programID === parseInt(selectedProgramId, 10));

    if (selectedProgram && selectedProgram.priceWeekday) {
      subtotal += parseFloat(selectedProgram.priceWeekday); // Use parseFloat for accurate addition
    }
  });

  // Update subtotal display
  subtotalElement.textContent = `$${subtotal.toFixed(2)}`;

  // For simplicity, assuming no taxes or additional fees in this example
  const total = subtotal;

  // Update total display
  totalElement.textContent = `$${total.toFixed(2)}`;
}


// Initialize the repeater
$('.source-item').repeater({
  initEmpty: false,
  defaultValues: {
    'text-input': 'foo'
  },
  show: function () {
    $(this).slideDown();

    // Fetch the parent ID and populate the program select
    const parentSelect = document.getElementById('parentSelect');
    const selectedParentId = parseInt(parentSelect.value, 10);
    if (selectedParentId) {
      fetchProgramsForParent(selectedParentId);
    }
  },
  hide: function (deleteElement) {
    $(this).slideUp(deleteElement);
  },
  ready: function (setIndexes) {
    // Fetch the parent ID and populate the program select
    const parentSelect = document.getElementById('parentSelect');
    const selectedParentId = parseInt(parentSelect.value, 10);
    if (selectedParentId) {
      fetchProgramsForParent(selectedParentId);
    }
  }
});

// Ensure the program select is populated on page load
const parentSelect = document.getElementById('parentSelect');
const selectedParentId = parseInt(parentSelect.value, 10);
if (selectedParentId) {
  fetchProgramsForParent(selectedParentId);
}
