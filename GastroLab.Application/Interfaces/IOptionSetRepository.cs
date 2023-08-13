using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IOptionSetRepository
    {
        public void AddOptionSet(OptionSet optionSet);
        public void DeleteOptionSet(int optionSetId);
        public void UpdateOptionSet(OptionSet optionSet);
        public OptionSet GetOptionSet(int id);
        public IEnumerable<OptionSet> GetAllOptionSets();
        public void AddOptionToOptionSet(int optionId, int optionSetId);
        public void DeleteOptionFromOptionSet(int optionId, int optionSetId);
        public IEnumerable<Option> GetAllOptions();
        public void AddOption(Option option);
        public void DeleteOption(int optionId);
        public Option GetOption(int optionId);
    }
}
