using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly IGlobalSettingsRepository _repository;

        public GlobalSettingsService(IGlobalSettingsRepository repository)
        {
            _repository = repository;
        }

        public GlobalSettingsVM GetSettings()
        {
            return _repository.GetSettings().ToVM();
        }

        public void UpdateSettings(GlobalSettingsVM globalSettingsVM)
        {
            _repository.UpdateSettings(globalSettingsVM.ToModel());
        }
    }
}
