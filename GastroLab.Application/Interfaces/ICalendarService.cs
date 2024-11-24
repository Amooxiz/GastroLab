using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface ICalendarService
    {
        public List<TimeSlotVM> GetWorkingTimesByUserId(string userId);
        public void AddWorkingTime(TimeSlotVM timeSlotVM);
        public List<TimeSlotVM> GetRegisteredTimesByUserId(string currentUserId);
        public void AddRegisteredTime(TimeSlotVM timeSlot);
        public TimeSlotVM GetRegisteredTimeById(int timeslotId);
        public void UpdateRegisteredTime(TimeSlotVM timeSlot);
        public void DeleteRegisteredTime(int timeslotId);
        public bool CheckForOverlappingTimes(string userId, TimeSlotVM timeToCheck);
    }
}
