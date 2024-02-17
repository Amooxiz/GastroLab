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


        public List<WorkingTime> GetWorkingTimesByUserId(string userId)
        {
            return _context.WorkingTimes.Where(wt => wt.UserId == userId).ToList();
        }
    }
}
