// API CALLS

// -- Get Filtered Program <- Steps <- Roadmap -- START
const apiUrl = '/Admin/Schedule/GetProgram';

fetch(apiUrl)
  .then(response => {
    if (!response.ok) {
      throw new Error('Response was not ok');
    }
    return response.json();
  })
  .then(programData => {
    const roadmapDetails = programData.data.roadmapList;
    const stepDetails = programData.data.stepList;
    const programDetails = programData.data.programList;


    // Extract roadmaps, steps, and programs
    const roadmaps = roadmapDetails.map(roadmap => {
      roadmapId= roadmap.roadmapId;
      roadmapName= roadmap.name;
      return {roadmapId, roadmapName}
    });

    const steps = stepDetails.map(step => {
      stepId= step.stepId;
      roadmapId= step.roadmapId;
      stepName= step.title;
      return {stepId, roadmapId, stepName}
    });

    const programs = programDetails.map(program => {
      programId= program.programID;
      programName= program.programName;
      stepId= program.stepId;
      return {stepId, programId, programName}
    });

    // checkdata
    console.log(roadmaps);
    console.log(steps);
    console.log(programs);

    // Get the <select> elements
    const roadmapElement = document.getElementById('roadmapSelect');
    const stepElement = document.getElementById('stepSelect');
    const programElement = document.getElementById('programSelect');

    // Populate the roadmap options
    roadmaps.forEach(roadmap => {
      const option = document.createElement('option');
      option.value = roadmap.roadmapId;
      option.textContent = roadmap.roadmapName;
      roadmapElement.appendChild(option);
    });

    // Event listener for roadmap selection
    roadmapElement.addEventListener('change', () => {
      const selectedRoadmapId = parseInt(roadmapElement.value, 10); // Get the selected roadmap ID
      const filteredSteps = steps.filter(step => step.roadmapId === selectedRoadmapId);

      // check data
      console.log('filtered step:', filteredSteps);

      // Clear existing step options
      stepElement.innerHTML = ''; // Remove existing options

      // Populate stepSelect dropdown with filtered steps
      filteredSteps.forEach(step => {
        const option = document.createElement('option');
        option.value = step.stepId; // Set the value to the step ID
        option.textContent = step.stepName; // Set the displayed text to the step name
        stepElement.appendChild(option); // Append the option to the select element
      });
    });

    // Event listener for step selection
    stepElement.addEventListener('change', () => {
      const selectedStepId = parseInt(stepElement.value, 10); // Get the selected step ID
      const filteredPrograms = programs.filter(program => program.stepId === selectedStepId);

      // Clear existing program options
      programElement.innerHTML = ''; // Remove existing options

      // Populate programSelect dropdown with filtered programs
      filteredPrograms.forEach(program => {
        const option = document.createElement('option');
        option.value = program.programId; // Set the value to the program ID
        option.textContent = program.programName; // Set the displayed text to the program name
        programElement.appendChild(option); // Append the option to the select element
      });
    });
  })
  .catch(error => {
    console.error('Error:', error);
  });
// -- Get Filtered Program <- Steps <- Roadmap -- END

// -- Start & End Date Time Selecter -- START
(function () {
  // Flat Picker
  // --------------------------------------------------------------------
  const
    flatpickrMulti = document.querySelector('#flatpickr-multi'),
    flatpickrMulti1 = document.querySelector('#flatpickr-multi1')



  // Multi Date Select
  if (flatpickrMulti) {
    flatpickrMulti.flatpickr({
      inline: true,
      allowInput: false,
      mode: 'range',
      minDate: 'today',
      altInput: true,
      altFormat: 'j F Y',
      dateFormat: 'Y-m-d',
    });
  }


})();
// -- Start & End Date Time Selecter -- END
