using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarService(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        public void DeleteWorkingTime(int timeslotId)
        {
            _calendarRepository.DeleteWorkingTime(timeslotId);
        }

        public TimeSlotVM GetWorkingTimeById(int timeslotId)
        {
            return _calendarRepository.GetWorkingTimeById(timeslotId).ToVM();
        }

        public void UpdateWorkingTime(TimeSlotVM timeSlot)
        {
            var workingTimeToUpdate = _calendarRepository.GetWorkingTimeById(timeSlot.Id);

            if (workingTimeToUpdate == null)
            {
                throw new Exception("Registered time not found");
            }

            var workingTime = timeSlot.ToWorkingTimeModel();

            workingTimeToUpdate.DateFrom = workingTime.DateFrom;
            workingTimeToUpdate.DateTo = workingTime.DateTo;
            workingTimeToUpdate.DayOfWeek = workingTime.DayOfWeek;
            workingTimeToUpdate.TimeInterval = workingTime.TimeInterval;

            _calendarRepository.UpdateWorkingTime(workingTimeToUpdate);
        }

        public bool CheckForOverlappingWorkingTimes(TimeSlotVM workingTime)
        {
            return _calendarRepository.CheckForOverlappingWorkingTimes(workingTime.ToWorkingTimeModel());
        }

        public TimeSlotVM GetRegisteredTimeById(int timeslotId)
        {
            return _calendarRepository.GetRegisteredTimeById(timeslotId).ToVM();
        }

        public void UpdateRegisteredTime(TimeSlotVM timeSlot)
        {
            var registeredTimeToUpdate = _calendarRepository.GetRegisteredTimeById(timeSlot.Id);

            if (registeredTimeToUpdate == null)
            {
                throw new Exception("Registered time not found");
            }

            var registeredTime = timeSlot.ToRegisteredTimeModel();

            registeredTimeToUpdate.DateFrom = registeredTime.DateFrom;
            registeredTimeToUpdate.DateTo = registeredTime.DateTo;
            registeredTimeToUpdate.Description = registeredTime.Description;
            registeredTimeToUpdate.DayOfWeek = registeredTime.DayOfWeek;
            registeredTimeToUpdate.TimeInterval = registeredTime.TimeInterval;

            _calendarRepository.UpdateRegisteredTime(registeredTimeToUpdate);
        }

        public void DeleteRegisteredTime(int timeslotId)
        {
            _calendarRepository.DeleteRegisteredTime(timeslotId);
        }

        public bool CheckForOverlappingRegisteredTimes(string userId, TimeSlotVM timeToCheck)
        {
            return _calendarRepository.CheckForOverlappingRegisteredTimes(userId, timeToCheck.ToRegisteredTimeModel());
        }

        public void AddRegisteredTime(TimeSlotVM timeSlot)
        {
            _calendarRepository.AddRegisteredTime(timeSlot.ToRegisteredTimeModel());
        }

        public List<TimeSlotVM> GetRegisteredTimesByUserId(string currentUserId)
        {
            return _calendarRepository.GetRegisteredTimesByUserId(currentUserId).Select(x => x.ToVM()).ToList();
        }

        public List<TimeSlotVM> GetWorkingTimesByUserId(string userId)
        {
            return _calendarRepository.GetWorkingTimesByUserId(userId).Select(x => x.ToVM()).ToList();
        }

        public void AddWorkingTime(TimeSlotVM timeSlotVM)
        {
            _calendarRepository.AddWorkingTime(timeSlotVM.ToWorkingTimeModel());
        }
    }
}
