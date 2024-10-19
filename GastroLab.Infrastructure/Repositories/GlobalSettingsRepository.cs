using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Repositories
{
    public class GlobalSettingsRepository : IGlobalSettingsRepository
    {
        private readonly GastroLabDbContext _context;

        public GlobalSettingsRepository(GastroLabDbContext context)
        {
            _context = context;
        }

        public GlobalSettings GetSettings()
        {
            return _context.GlobalSettings.Include(g => g.Address).FirstOrDefault();
        }

        public void UpdateSettings(GlobalSettings globalSettings)
        {
            var globalSettingsToUpdate = _context.GlobalSettings.Include(g => g.Address).FirstOrDefault();
            if (globalSettingsToUpdate != null)
            {
                globalSettingsToUpdate.RestaurantName = globalSettings.RestaurantName;
                globalSettingsToUpdate.Address.City = globalSettings.Address.City;
                globalSettingsToUpdate.Address.Street = globalSettings.Address.Street;
                globalSettingsToUpdate.Address.PostCode = globalSettings.Address.PostCode;
                globalSettingsToUpdate.Address.HouseNumber = globalSettings.Address.HouseNumber;
                globalSettingsToUpdate.Address.FlatNumber = globalSettings.Address.FlatNumber;
                globalSettingsToUpdate.DefaultDeliveryWaitingTime = globalSettings.DefaultDeliveryWaitingTime;
                globalSettingsToUpdate.DefaultDineInWaitingTime = globalSettings.DefaultDineInWaitingTime;

                _context.GlobalSettings.Update(globalSettingsToUpdate);
                _context.SaveChanges();
            }
        }
    }
}
