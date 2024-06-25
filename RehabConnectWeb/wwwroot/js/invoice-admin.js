// Assuming you have already fetched parent data and populated the <select> element
const apiUrl = '/Admin/Invoice/GetParent';

fetch(apiUrl)
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(userData => {
    const parentDetails = userData.data; // Assuming 'data' is the key in your response
    const parents = parentDetails.map(parent => {
      const fatherName = parent.fatherName || '';
      const motherName = parent.motherName || '';
      const combinedName = `${fatherName} & ${motherName}`;
      const parentIds = parent.parentID;
      const address1 = parent.fatherAddress;
      const postcode = parent.fatherPostcode;
      const city = parent.fatherCity;
      const country = parent.fatherCountry;
      const address2 = `${postcode} ${city}, ${country}`;
      const phoneNum = parent.fatherPhoneNum;
      const email = parent.fatherEmail;
      return { parentIds, combinedName, address1, address2, phoneNum, email };
    });

    // Now you have an array of objects with parentId and combinedName
    console.log(parents); // Optional: You can log it here if needed

    // Get the <select> element
    const selectElement = document.getElementById('parentSelect');

    // Populate the options
    parents.forEach(parent => {
      const optionElement = document.createElement('option');
      optionElement.value = parent.parentIds;
      optionElement.textContent = parent.combinedName;
      selectElement.appendChild(optionElement);
    });

    // Get references to the address elements
    const nameElement = document.querySelector('.mb-1:nth-child(2)');
    const address1Element = document.querySelector('.mb-1:nth-child(3)');
    const address2Element = document.querySelector('.mb-1:nth-child(4)');
    const phoneElement = document.querySelector('.mb-1:nth-child(5)');
    const emailElement = document.querySelector('.mb-1:nth-child(6)');

    // Set default text for address elements
    nameElement.innerText = 'Name';
    address1Element.innerText = 'Address1';
    address2Element.innerText = 'Address2';
    phoneElement.innerText = 'Phone Number';
    emailElement.innerText = 'Email';

    // Listen for changes in the <select> element
    selectElement.addEventListener('change', () => {
      const selectedParentId = selectElement.value;
      console.log('Selected Parent ID:', selectedParentId); // Log the selected ID

      // Convert selectedParentId to a number (if needed)
      const numericParentId = parseInt(selectedParentId, 10); // Assuming parentIds are integers

      const selectedParent = parents.find(parent => parent.parentIds === numericParentId);

      console.log('Selected Parent:', selectedParent); // Log the selected parent object


      // Update address elements
      if (selectedParent) {
        nameElement.innerText = selectedParent.combinedName;
        address1Element.innerText = selectedParent.address1;
        address2Element.innerText = selectedParent.address2;
        phoneElement.innerText = selectedParent.phoneNum;
        emailElement.innerText = selectedParent.email;
      } else {
        // Handle the case when no parent is selected (optional)
        nameElement.innerText = 'FatherName';
        address1Element.innerText = 'FAddress1';
        address2Element.innerText = 'FAddress2';
        phoneElement.innerText = 'FPhone Number';
        emailElement.innerText = 'email';
      }

    });
  })
  .catch(error => {
    console.error('Error:', error);
  });
