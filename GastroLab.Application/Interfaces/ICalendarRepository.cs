using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface ICalendarRepository
    {
        public void DeleteWorkingTime(int timeslotId);
        public WorkingTime GetWorkingTimeById(int timeslotId);
        public void UpdateWorkingTime(WorkingTime workingTime);
        public bool CheckForOverlappingWorkingTimes(WorkingTime workingTime);
        public void AddRegisteredTime(RegisteredTime registeredTime);
        public void AddWorkingTime(WorkingTime workingTime);
        public bool CheckForOverlappingRegisteredTimes(string userId, RegisteredTime registeredTime);
        public void DeleteRegisteredTime(int timeslotId);
        public RegisteredTime GetRegisteredTimeById(int timeslotId);
        public List<RegisteredTime> GetRegisteredTimesByUserId(string currentUserId);
        public List<WorkingTime> GetWorkingTimesByUserId(string userId);
        public void UpdateRegisteredTime(RegisteredTime registeredTime);
    }
}
