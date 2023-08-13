using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IOptionSetService
    {
        public void AddOptionSet(OptionSetVM optionSet);
        public void DeleteOptionSet(int optionSetId);
        public void UpdateOptionSet(OptionSetVM optionSet);
        public OptionSetVM GetOptionSet(int optionSetId);
        public IEnumerable<OptionSetVM> GetAllOptionSets();
        public void AddOptionToOptionSet(int optionId, int optionSetId);
        public void DeleteOptionFromOptionSet(int optionId, int optionSetId);
        public IEnumerable<OptionVM> GetAllOptions();
        public void AddOption(OptionVM option);
        public void DeleteOption(int optionId);
        public OptionVM GetOption(int optionId);
    }
}
