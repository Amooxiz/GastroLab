using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class RegisteredTimeService : IRegisteredTimeService
    {
        private readonly IRegisteredTimeRepository _registeredTimeRepository;

        public RegisteredTimeService(IRegisteredTimeRepository registeredTimeRepository)
        {
            _registeredTimeRepository = registeredTimeRepository;
        }

        public void RegisterTime(TimeSlotVM registeredTime)
        {
            _registeredTimeRepository.RegisterTime(registeredTime.ToRegisteredTimeModel());
        }
    }
}
