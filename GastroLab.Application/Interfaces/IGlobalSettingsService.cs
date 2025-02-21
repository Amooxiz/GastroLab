using GastroLab.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IGlobalSettingsService
    {
        public GlobalSettingsVM GetSettings();
        public void UpdateSettings(GlobalSettingsVM globalSettingsVM);
    }
}
