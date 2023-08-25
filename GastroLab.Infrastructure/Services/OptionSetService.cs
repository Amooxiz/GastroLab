using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Services
{
    public class OptionSetService : IOptionSetService
    {
        private readonly IOptionSetRepository _optionSetRepository;

        public OptionSetService(IOptionSetRepository optionSetRepository)
        {
            _optionSetRepository = optionSetRepository;
        }

        public void AddOptionSet(OptionSetVM optionSetVm)
        {
            _optionSetRepository.AddOptionSet(optionSetVm.ToModel());
        }
        public void DeleteOptionSet(int optionSetId)
        {
            _optionSetRepository.DeleteOptionSet(optionSetId);
        }
        public void UpdateOptionSet(OptionSetVM optionSet)
        {
            _optionSetRepository.UpdateOptionSet(optionSet.ToModel());
        }
        public OptionSetVM GetOptionSet(int optionSetId)
        {
            return _optionSetRepository.GetOptionSet(optionSetId).ToVM();
        }
        public IEnumerable<OptionSetVM> GetAllOptionSets()
        {
            return _optionSetRepository.GetAllOptionSets().Select(x => x.ToVM());
        }
        public void AddOptionToOptionSet(int optionId, int optionSetId)
        {
            _optionSetRepository.AddOptionToOptionSet(optionId, optionSetId);
        }
        public void DeleteOptionFromOptionSet(int optionId, int optionSetId)
        {
            _optionSetRepository.DeleteOptionFromOptionSet(optionId, optionSetId);
        }
        public IEnumerable<OptionVM> GetAllOptions()
        {
            return _optionSetRepository.GetAllOptions().Select(x => x.ToVM());
        }
        public void AddOption(OptionVM option)
        {
            _optionSetRepository.AddOption(option.ToModel());
        }
        public void DeleteOption(int optionId)
        {
            _optionSetRepository.DeleteOption(optionId);
        }
        public OptionVM GetOption(int optionId)
        {
            return _optionSetRepository.GetOption(optionId).ToVM();
        }
    }
}
