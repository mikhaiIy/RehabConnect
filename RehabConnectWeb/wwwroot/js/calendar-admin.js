'use strict';

let direction = 'ltr';

if (isRtl) {
  direction = 'rtl';
}

document.addEventListener('DOMContentLoaded', function () {
  (function () {
    const calendarEl = document.getElementById('calendar'),
      appCalendarSidebar = document.querySelector('.app-calendar-sidebar'),
      addEventSidebar = document.getElementById('addEventSidebar'),
      appOverlay = document.querySelector('.app-overlay'),
      calendarsColor = {
        'Consultation': 'danger',
        'Assessment': 'primary',
        'Full Development Report': 'warning',
        'Program A': 'success',
        'Program B': 'success',
        'Program C': 'success',
        'Program D': 'success',
        'Ready to School A': 'info',
        'Ready to School B': 'info'
      },
      offcanvasTitle = document.querySelector('.offcanvas-title'),
      btnToggleSidebar = document.querySelector('.btn-toggle-sidebar'),
      btnSubmit = document.querySelector('button[type="submit"]'),
      btnCancel = document.querySelector('.btn-cancel'),
      btnDeleteEvent = document.querySelector('.btn-delete-event'),
      eventTitle = document.querySelector('#eventTitle'),
      eventTitleInfo = document.querySelector('#eventTitleInfo'),
      eventRoadmap = document.querySelector('#eventRoadmap'),
      eventStep = document.querySelector('#eventStep'),
      eventProgram = document.querySelector('#eventProgram'),
      eventCapacity = document.querySelector('#capacity'),
      eventRegistered = document.querySelector('#eventRegistered'),
      eventRegisteredInfo = document.querySelector('#registered'),
      eventType = document.querySelector('#eventType'),
      eventDate = document.querySelector('#eventDate'),
      eventDateInfo = document.querySelector('#eventDateInfo'),
      eventDateInfo1 = document.querySelector('#eventDateInfo1'),
      eventTime = document.querySelector('#eventTime'),
      eventTimeInfo = document.querySelector('#eventTimeInfo'),
      eventTimeInfo1 = document.querySelector('#eventTimeInfo1'),
      eventTimeInfo2 = document.querySelector('#eventTimeInfo2'),
      eventStartDate = document.querySelector('#eventStartDate'),
      eventEndDate = document.querySelector('#eventEndDate'),
      selectAll = document.querySelector('.select-all'),
      filterInput = [].slice.call(document.querySelectorAll('.input-filter')),
      inlineCalendar = document.querySelector('.inline-calendar');

    let eventToUpdate, currentEvents = fetchEvents,
      isFormValid = false,
      inlineCalInstance;

    console.log('CurrentEvents: ', currentEvents);

    // Init event Offcanvas
    const bsAddEventSidebar = new bootstrap.Offcanvas(addEventSidebar);

    // Filter events by calender
    function selectedCalendars() {
      let selected = [],
        filterInputChecked = [].slice.call(document.querySelectorAll('.input-filter:checked'));

      filterInputChecked.forEach(item => {
        selected.push(item.getAttribute('data-value'));
      });

      return selected;
    }

    // Inline sidebar calendar (flatpicker)
    if (inlineCalendar) {
      inlineCalInstance = inlineCalendar.flatpickr({
        monthSelectorType: 'static',
        inline: true
      });
    }

    // Event click function
    function eventClick(info) {
      eventToUpdate = info.event;
      console.log('eventToUpdate>: ',eventToUpdate);
      if (eventToUpdate.url) {
        info.jsEvent.preventDefault();
        window.open(eventToUpdate.url, '_blank');
      }
      bsAddEventSidebar.show();
      // For update event set offcanvas title text: Update Event
      if (offcanvasTitle) {
        offcanvasTitle.innerHTML = 'Session Info';
      }
      btnSubmit.innerHTML = 'Update';
      btnSubmit.classList.add('btn-update-event');
      btnSubmit.classList.remove('btn-add-event');
      btnDeleteEvent.classList.remove('d-none');
      eventRoadmap.classList.add('d-none');
      eventStep.classList.add('d-none');
      eventProgram.classList.add('d-none');
      eventCapacity.value = eventToUpdate.extendedProps.capacity;
      eventCapacity.disabled = true;
      eventRegistered.classList.remove('d-none');
      eventRegisteredInfo.disabled = true;
      eventRegisteredInfo.value = eventToUpdate.extendedProps.registered;
      eventDate.classList.add('d-none');
      eventDateInfo.classList.remove('d-none');
      eventDateInfo1.value = eventToUpdate.start.toLocaleDateString();
      eventDateInfo1.disabled = true;
      eventTime.classList.add('d-none');
      eventTimeInfo.classList.remove('d-none');
      eventTimeInfo1.value = eventToUpdate.start.toLocaleTimeString();
      eventTimeInfo2.value = eventToUpdate.end.toLocaleTimeString();
      eventTimeInfo1.disabled = true;
      eventTimeInfo2.disabled = true;
      eventTitle.classList.remove('d-none');
      eventTitleInfo.value = eventToUpdate.title;
      eventTitleInfo.disabled = true ;


      eventTitle.value = eventToUpdate.title;
      start.setDate(eventToUpdate.start, true, 'Y-m-d');
      eventToUpdate.allDay === true ? (allDaySwitch.checked = true) : (allDaySwitch.checked = false);
      eventToUpdate.end !== null
        ? end.setDate(eventToUpdate.end, true, 'Y-m-d')
        : end.setDate(eventToUpdate.start, true, 'Y-m-d');
      eventLabel.val(eventToUpdate.extendedProps.calendar).trigger('change');
      eventToUpdate.extendedProps.location !== undefined
        ? (eventLocation.value = eventToUpdate.extendedProps.location)
        : null;
      eventToUpdate.extendedProps.guests !== undefined
        ? eventGuests.val(eventToUpdate.extendedProps.guests).trigger('change')
        : null;
      eventToUpdate.extendedProps.description !== undefined
        ? (eventDescription.value = eventToUpdate.extendedProps.description)
        : null;

      // // Call removeEvent function
      // btnDeleteEvent.addEventListener('click', e => {
      //   removeEvent(parseInt(eventToUpdate.id));
      //   // eventToUpdate.remove();
      //   bsAddEventSidebar.hide();
      // });
    }

    // Modify sidebar toggler
    function modifyToggler() {
      const fcSidebarToggleButton = document.querySelector('.fc-sidebarToggle-button');
      fcSidebarToggleButton.classList.remove('fc-button-primary');
      fcSidebarToggleButton.classList.add('d-lg-none', 'd-inline-block', 'ps-0');
      while (fcSidebarToggleButton.firstChild) {
        fcSidebarToggleButton.firstChild.remove();
      }
      fcSidebarToggleButton.setAttribute('data-bs-toggle', 'sidebar');
      fcSidebarToggleButton.setAttribute('data-overlay', '');
      fcSidebarToggleButton.setAttribute('data-target', '#app-calendar-sidebar');
      fcSidebarToggleButton.insertAdjacentHTML('beforeend', '<i class="ti ti-menu-2 ti-sm text-heading"></i>');
    }

    // Hide left sidebar if the right sidebar is open
    btnToggleSidebar.addEventListener('click', e => {
      if (offcanvasTitle) {
        offcanvasTitle.innerHTML = 'Add Event';
      }
      btnSubmit.innerHTML = 'Add';
      btnSubmit.classList.remove('btn-update-event');
      btnSubmit.classList.add('btn-add-event');
      btnDeleteEvent.classList.add('d-none');
      appCalendarSidebar.classList.remove('show');
      appOverlay.classList.remove('show');
      eventStartDate.value = date;
      eventEndDate.value = date;
      eventCapacity.value.remove();
      eventCapacity.disabled = false;
      eventRegistered.classList.add('d-none');

      eventRoadmap.classList.remove('d-none');
      eventStep.classList.remove('d-none');
      eventProgram.classList.remove('d-none');
      eventCapacity.value = '';
      eventCapacity.disabled = false;
      eventRegistered.classList.add('d-none');
      eventRegisteredInfo.disabled = false;
      eventRegisteredInfo.value = '';
      eventDate.classList.remove('d-none');
      eventDateInfo.classList.add('d-none');
      eventTime.classList.remove('d-none');
      eventTimeInfo.classList.add('d-none');
      eventTitle.classList.add('d-none');
    });

    // Sidebar Toggle Btn
    if (btnToggleSidebar) {
      btnToggleSidebar.addEventListener('click', e => {
        btnCancel.classList.remove('d-none');
        eventStartDate.value = date;
        eventEndDate.value = date;
        eventCapacity.value.remove();
        eventCapacity.disabled = false;
        eventRegistered.classList.add('d-none');
        eventRoadmap.classList.remove('d-none');
        eventStep.classList.remove('d-none');
        eventProgram.classList.remove('d-none');
        eventCapacity.value = '';
        eventCapacity.disabled = false;
        eventRegistered.classList.add('d-none');
        eventRegisteredInfo.disabled = false;
        eventRegisteredInfo.value = '';
        eventDate.classList.remove('d-none');
        eventDateInfo.classList.add('d-none');
        eventTime.classList.remove('d-none');
        eventTimeInfo.classList.add('d-none');
        eventTitle.classList.add('d-none');
      });
    }

    // Jump to date on sidebar(inline) calendar change
    inlineCalInstance.config.onChange.push(function (date) {
      calendar.changeView(calendar.view.type, moment(date[0]).format('YYYY-MM-DD'));
      modifyToggler();
      appCalendarSidebar.classList.remove('show');
      appOverlay.classList.remove('show');
    });



    function fetchEvents(info, successCallback) {
      // Fetch Events from API endpoint reference
      const scheduleApiUrl = '/Admin/Schedule/GetSchedule';

      $.ajax(
        {
          url: scheduleApiUrl,
          type: 'GET',
          success: function (result) {
            const events = result.events.map((event) => ({
              id: event.id,
              title: event.title,
              start: event.start,
              end: event.end,
              extendedProps : {
                calendar : event.extendedProps.calendar,
                capacity : event.extendedProps.capacity,
                registered : event.extendedProps.registered
              }
            }));

            let calendars = selectedCalendars();
            let selectedEvents = events.filter(function(event){
              return calendars.includes(event.extendedProps.calendar.toLowerCase());
            });
            successCallback(selectedEvents);
            console.log(selectedEvents);
          },
          error: function (error) {
            console.log(error);
          }
        }
      );
    }

    let calendar = new Calendar(calendarEl, {
      initialView: 'dayGridMonth',
      events: fetchEvents,
      plugins: [dayGridPlugin, interactionPlugin, listPlugin, timegridPlugin],
      editable: true,
      dragScroll: true,
      dayMaxEvents: 2,
      eventResizableFromStart: true,
      customButtons: {
        sidebarToggle: {
          text: 'Sidebar'
        }
      },
      headerToolbar: {
        start: 'sidebarToggle, prev,next, title',
        end: 'dayGridMonth,timeGridWeek,timeGridDay,listMonth'
      },
      direction: direction,
      avLinks: true,
      eventClassNames: function ({ event: calendarEvent }) {
        const colorName = calendarsColor[calendarEvent._def.title];
        // Background Color
        return ['fc-event-' + colorName]
      },
      dateClick: function (info) {
        let date = moment(info.date).format('YYYY-MM-DD');
        resetValues();
        bsAddEventSidebar.show();

        // For new event set offcanvas title text: Add Event
        if (offcanvasTitle) {
          offcanvasTitle.innerHTML = 'Add Event';
        }
        btnSubmit.innerHTML = 'Add';
        btnSubmit.classList.remove('btn-update-event');
        eventRoadmap.classList.remove('d-none');
        eventStep.classList.remove('d-none');
        eventProgram.classList.remove('d-none');
        btnSubmit.classList.add('btn-add-event');
        btnDeleteEvent.classList.add('d-none');
        eventStartDate.value = date;
        eventEndDate.value = date;
        eventCapacity.value.remove();
        eventCapacity.disabled = false;
        eventRegistered.classList.add('d-none');
      },
      eventClick: function (info) {
        eventClick(info);
      },
      datesSet: function () {
        modifyToggler();
      },
      viewDidMount: function () {
        modifyToggler();
      }
    });
    // Render
    calendar.render();
    // Modify sidebar toggler
    modifyToggler();

    // Calender filter functionality
    // ------------------------------------------------
    if (selectAll) {
      selectAll.addEventListener('click', e => {
        if (e.currentTarget.checked) {
          document.querySelectorAll('.input-filter').forEach(c => (c.checked = 1));
        } else {
          document.querySelectorAll('.input-filter').forEach(c => (c.checked = 0));
        }
        calendar.refetchEvents();
      });
    }

    if (filterInput) {
      filterInput.forEach(item => {
        item.addEventListener('click', () => {
          document.querySelectorAll('.input-filter:checked').length < document.querySelectorAll('.input-filter').length
            ? (selectAll.checked = false)
            : (selectAll.checked = true);
          calendar.refetchEvents();
        });
      });
    }

    // Jump to date on sidebar(inline) calendar change
    inlineCalInstance.config.onChange.push(function (date) {
      calendar.changeView(calendar.view.type, moment(date[0]).format('YYYY-MM-DD'));
      modifyToggler();
      appCalendarSidebar.classList.remove('show');
      appOverlay.classList.remove('show');
    });
  })();
});




