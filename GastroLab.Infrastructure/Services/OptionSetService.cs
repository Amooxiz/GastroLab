using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.Extensions.Options;
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

        public List<OptionSetVM> GetUsedOptionSetsByIds(List<int> optionSetIds)
        {
            return _optionSetRepository.GetUsedOptionSetsByIds(optionSetIds).Select(x => x.ToVM()).ToList();
        }
        public void DeleteGlobalOptionSets(List<int> optionSetIds)
        {
            _optionSetRepository.DeleteGlobalOptionSets(optionSetIds);
        }

        public List<OptionSetVM> GetGlobalOptionSets()
        {
            return _optionSetRepository.GetGlobalOptionSets().Select(opt => opt.ToVM()).ToList();
        }

        public OptionSetVM CreateGlobalOptionSet(OptionSetVM optionSetVm)
        {
            return _optionSetRepository.CreateGlobalOptionSet(optionSetVm.ToModel()).ToVM();
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

        public void UpdateOption(OptionVM option)
        {
            _optionSetRepository.UpdateOption(option.ToModel());
        }

        public void AddOptionToOptionSet(int optionId, int optionSetId, decimal price)
        {
            var optionSetOption = new OptionSetOption
            {
                OptionSetId = optionSetId,
                OptionId = optionId,
                Price = price
            };
            _optionSetRepository.AddOptionToOptionSet(optionSetOption);
        }
        public void UpdateOptionSetOption(int optionId, int optionSetId, decimal price)
        {
            _optionSetRepository.UpdateOptionSetOption(optionId, optionSetId, price);
        }
        public void RemoveOption(int id, int optionSetId)
        {
            _optionSetRepository.RemoveOption(id, optionSetId);
        }
    }
}
