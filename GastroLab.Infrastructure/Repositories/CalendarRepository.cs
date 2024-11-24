using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly GastroLabDbContext _context;

        public CalendarRepository(GastroLabDbContext context)
        {
            _context = context;
        }

        public bool CheckForOverlappingTimes(string userId, RegisteredTime registeredTime)
        {
            return _context.RegisteredTimes
            .Any(rt =>
                rt.UserId == userId &&
                rt.Id != registeredTime.Id &&
                rt.DateFrom < registeredTime.DateTo &&
                rt.DateTo > registeredTime.DateFrom);
        }
        public void DeleteRegisteredTime(int timeslotId)
        {
            var RegisteredTimeToRemove = _context.RegisteredTimes.Find(timeslotId);

            if (RegisteredTimeToRemove != null)
            {
                _context.RegisteredTimes.Remove(RegisteredTimeToRemove);
                _context.SaveChanges();
            }
        }
        public RegisteredTime GetRegisteredTimeById(int timeslotId)
        {
            var registeredTime = _context.RegisteredTimes.Find(timeslotId);

            if (registeredTime == null)
            {
                throw new Exception("Registered time not found");
            }
            return registeredTime;
        }

        public void UpdateRegisteredTime(RegisteredTime registeredTime)
        {
            _context.RegisteredTimes.Update(registeredTime);
            _context.SaveChanges();
        }

        public void AddRegisteredTime(RegisteredTime registeredTime)
        {
            _context.RegisteredTimes.Add(registeredTime);
            _context.SaveChanges();
        }

        public List<RegisteredTime> GetRegisteredTimesByUserId(string currentUserId)
        {
            return _context.RegisteredTimes.Where(x => x.UserId == currentUserId).ToList();
        }

        public List<WorkingTime> GetWorkingTimesByUserId(string userId)
        {
            return _context.WorkingTimes.Where(wt => wt.UserId == userId).ToList();
        }

        public void AddWorkingTime(WorkingTime workingTime)
        {
            _context.WorkingTimes.Add(workingTime);
            _context.SaveChanges();
        }
    }
}
