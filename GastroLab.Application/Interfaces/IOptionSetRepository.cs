using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IOptionSetRepository
    {
        public List<OptionSet> GetUsedOptionSetsByIds(List<int> optionSetIds);
        public void DeleteGlobalOptionSets(List<int> optionSetIds);
        public List<OptionSet> GetGlobalOptionSets();
        public OptionSet CreateGlobalOptionSet(OptionSet optionSet);
        public void AddOptionSet(OptionSet optionSet);
        public void DeleteOptionSet(int optionSetId);
        public void UpdateOptionSet(OptionSet optionSet);
        public OptionSet GetOptionSet(int id);
        public IEnumerable<OptionSet> GetAllOptionSets();
        public void DeleteOptionFromOptionSet(int optionId, int optionSetId);
        public IEnumerable<Option> GetAllOptions();
        public void AddOption(Option option);
        public void DeleteOption(int optionId);
        public Option GetOption(int optionId);

        public void UpdateOption(Option option);
        public void AddOptionToOptionSet(OptionSetOption optionSetOption);
        public void RemoveOption(int id, int optionSetId);
        public void UpdateOptionSetOption(int optionId, int optionSetId, decimal price);
    }
}
