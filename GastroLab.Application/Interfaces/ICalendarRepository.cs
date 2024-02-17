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
        public List<WorkingTime> GetWorkingTimesByUserId(string userId);
    }
}
