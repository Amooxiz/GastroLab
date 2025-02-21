using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IOptionSetService
    {
        public List<OptionSetVM> GetGlobalOptionSets();
        public OptionSetVM CreateGlobalOptionSet(OptionSetVM optionSetVm);
        public void AddOptionSet(OptionSetVM optionSet);
        public void DeleteOptionSet(int optionSetId);
        public void UpdateOptionSet(OptionSetVM optionSet);
        public OptionSetVM GetOptionSet(int optionSetId);
        public IEnumerable<OptionSetVM> GetAllOptionSets();
        public void DeleteOptionFromOptionSet(int optionId, int optionSetId);
        public IEnumerable<OptionVM> GetAllOptions();
        public void AddOption(OptionVM option);
        public void DeleteOption(int optionId);
        public OptionVM GetOption(int optionId);
        public void UpdateOption(OptionVM option);
        public void AddOptionToOptionSet(int optionId, int optionSetId, decimal price);
        public void UpdateOptionSetOption(int optionId, int optionSetId, decimal price);
        public void RemoveOption(int id, int optionSetId);
        public List<OptionSetVM> GetUsedOptionSetsByIds(List<int> optionSetIds);
        public void DeleteGlobalOptionSets(List<int> optionSetIds);
    }
}
