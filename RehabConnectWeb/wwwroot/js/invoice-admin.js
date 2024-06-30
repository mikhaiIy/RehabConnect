$(document).ready(function () {
  const apiUrl = '/Admin/Invoice/GetParent';
  const programApiUrl = '/Admin/Invoice/GetProgramList';

  // Select elements
  const parentSelect = document.getElementById('parentSelect');
  const parentIDInput = document.getElementById('ParentID');
  const programSelect = document.querySelector('.program-select');
  const priceDisplay = document.querySelector('.price-display');
  const priceInput = document.querySelector('.invoice-item-price');
  const subtotalDisplay = document.getElementById('subtotal-display');
  const subtotalInput = document.getElementById('subtotal-input');
  const totalDisplay = document.getElementById('total-display');
  const totalInput = document.getElementById('total-input');

  // Fetch parent data and populate the select element
  fetch(apiUrl)
    .then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json();
    })
    .then(userData => {
      const parents = userData.data;

      parents.forEach(parent => {
        const optionElement = document.createElement('option');
        optionElement.value = parent.parentID;
        optionElement.textContent = `${parent.fatherName || ''} & ${parent.motherName || ''}`;
        parentSelect.appendChild(optionElement);
      });

      // Event listener for parent selection change
      parentSelect.addEventListener('change', () => {
        const selectedParentId = parseInt(parentSelect.value, 10);
        const selectedParent = parents.find(parent => parent.parentID === selectedParentId);

        if (selectedParent) {
          document.querySelector('.mb-1:nth-child(2)').innerText = selectedParent.fatherName + " & " + selectedParent.motherName;
          document.querySelector('.mb-1:nth-child(3)').innerText = selectedParent.fatherAddress;
          document.querySelector('.mb-1:nth-child(4)').innerText = `${selectedParent.fatherPostcode} ${selectedParent.fatherCity}, ${selectedParent.fatherCountry}`;
          document.querySelector('.mb-1:nth-child(5)').innerText = selectedParent.fatherPhoneNum;
          document.querySelector('.mb-1:nth-child(6)').innerText = selectedParent.fatherEmail;

          parentIDInput.value = selectedParentId;

          fetchProgramsForParent(selectedParentId); // Fetch programs for the selected parent
        } else {
          resetDisplayValues(); // Reset display values if no parent is selected
          parentIDInput.value = '';
        }
      });
    })
    .catch(error => {
      console.error('Error fetching parent data:', error);
    });

  // Function to fetch programs for a selected parent
  function fetchProgramsForParent(parentId) {
    const url = `${programApiUrl}?parentId=${parentId}`;

    fetch(url)
      .then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then(programData => {
        const programs = programData.data.programList;
        const steps = programData.data.steps;

        populateProgramSelect(programs, steps);
      })
      .catch(error => {
        console.error('Error fetching program data:', error);
      });
  }

  // Function to populate the program select dropdown
  function populateProgramSelect(programs, steps) {
    programSelect.innerHTML = '<option selected disabled>Select Program</option>';

    const addedPrograms = new Set();

    programs.forEach(program => {
      const selectedStep = steps.find(step => step.stepId === program.stepId);

      if (selectedStep && selectedStep.combinedPricing) {
        const combinedPrograms = programs.filter(p => p.stepId === selectedStep.stepId).map(p => p.programName).join(' & ');

        if (!addedPrograms.has(combinedPrograms)) {
          const optionElement = document.createElement('option');
          optionElement.value = selectedStep.stepId; // Use stepId or another appropriate value as the ID
          optionElement.textContent = combinedPrograms; // Display combined program names
          programSelect.appendChild(optionElement);

          addedPrograms.add(combinedPrograms);
        }
      } else {
        if (!addedPrograms.has(program.programName)) {
          const optionElement = document.createElement('option');
          optionElement.value = program.programID; // Use programID as the ID
          optionElement.textContent = program.programName; // Display each program name separately
          programSelect.appendChild(optionElement);

          addedPrograms.add(program.programName);
        }
      }
    });

    programSelect.addEventListener('change', () => {
      const selectedProgramID = programSelect.value;
      const selectedProgram = programs.find(program => program.programID === parseInt(selectedProgramID));
      const selectedStep = steps.find(step => step.stepId === selectedProgram.stepId);

      if (selectedProgram) {
        let price = 'Price not available';

        if (selectedStep && selectedStep.combinedPricing) {
          price = selectedStep.priceWeekday ? `$${selectedStep.priceWeekday.toFixed(2)}` : 'Price not available';
          priceInput.value = selectedStep.priceWeekday ? selectedStep.priceWeekday.toFixed(2) : '';
        } else {
          price = selectedProgram.priceWeekday ? `$${selectedProgram.priceWeekday.toFixed(2)}` : 'Price not available';
          priceInput.value = selectedProgram.priceWeekday ? selectedProgram.priceWeekday.toFixed(2) : '';
        }

        priceDisplay.textContent = price;
        calculateSubtotalAndTotal();
      } else {
        priceDisplay.textContent = 'Price not available';
        priceInput.value = '';
        calculateSubtotalAndTotal();
      }
    });

  }

  // Function to calculate subtotal and total based on selected programs
  function calculateSubtotalAndTotal() {
    let subtotal = parseFloat(priceInput.value);

    if (!isNaN(subtotal)) { // Ensure subtotal is a valid number
      // Update subtotal display
      subtotalDisplay.innerText = `$${subtotal.toFixed(2)}`;
      subtotalInput.value = subtotal.toFixed(2); // Optional: Store subtotal in hidden input field

      // Update total display (assuming no taxes or additional fees for simplicity)
      totalDisplay.innerText = `$${subtotal.toFixed(2)}`;
      totalInput.value = subtotal.toFixed(2); // Optional: Store total in hidden input field
    } else {
      // Display default or error message for subtotal and total
      subtotalDisplay.innerText = 'Subtotal not available';
      totalDisplay.innerText = 'Total not available';
      subtotalInput.value = '';
      totalInput.value = '';
    }
  }


  // Function to reset display values when no parent is selected
  function resetDisplayValues() {
    document.querySelector('.mb-1:nth-child(2)').innerText = '';
    document.querySelector('.mb-1:nth-child(3)').innerText = '';
    document.querySelector('.mb-1:nth-child(4)').innerText = '';
    document.querySelector('.mb-1:nth-child(5)').innerText = '';
    document.querySelector('.mb-1:nth-child(6)').innerText = '';
  }
});
