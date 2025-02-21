using GastroLab.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IRegisteredTimeService
    {
        public void RegisterTime(TimeSlotVM registeredTime);
    }
}
