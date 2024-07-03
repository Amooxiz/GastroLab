using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
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
    public class OptionSetRepository : IOptionSetRepository
    {
        private readonly GastroLabDbContext _context;

        public OptionSetRepository(GastroLabDbContext context)
        {
            _context = context;
        }

        public void AddOptionSet(OptionSet optionSet)
        {
            _context.OptionSets.Add(optionSet);
            _context.SaveChanges();
        }
        public void DeleteOptionSet(int optionSetId)
        {
            var optionSetToRemove = _context.OptionSets.Find(optionSetId);

            if (optionSetToRemove == null)
            {
                throw new Exception("Cannot find OptionSet with Id = " + optionSetId);
            }
            _context.OptionSets.Remove(optionSetToRemove);
            _context.SaveChanges();
        }
        public void UpdateOptionSet(OptionSet optionSet)
        {
            _context.OptionSets.Update(optionSet);
            _context.SaveChanges();
        }
        public OptionSet GetOptionSet(int optionSetId)
        {
            var optionSet = _context.OptionSets.Where(x => x.Id == optionSetId)?
                .Include(x => x.OptionSetOptions)
                .ThenInclude(x => x.Option)
                .FirstOrDefault();

            if (optionSet == null)
            {
                throw new Exception("Cannot find OptionSet with Id = " + optionSetId);
            }
            return optionSet;
        }
        public IEnumerable<OptionSet> GetAllOptionSets()
        {
            return _context.OptionSets
                .Include(x => x.OptionSetOptions)
                .ThenInclude(x => x.Option);
        }
        
        public void DeleteOptionFromOptionSet(int optionId, int optionSetId)
        {
            var optionSetOption = _context.OptionSetOptions.FirstOrDefault(x => x.OptionId == optionId && x.OptionSetId == optionSetId);

            if (optionSetOption == null)
            {
                throw new Exception("OptionSetOption not found");
            }
            
            _context.OptionSetOptions.Remove(optionSetOption);
            _context.SaveChanges();
        }
        public IEnumerable<Option> GetAllOptions()
        {
            return _context.Options;
        }
        public void AddOption(Option option)
        {
            _context.Options.Add(option);
            _context.SaveChanges();
        }
        public void DeleteOption(int optionId)
        {
            var option = _context.Options.Find(optionId);

            if (option == null)
            {
                throw new Exception("Cannot find Option with Id = " + optionId);
            }
            _context.Options.Remove(option);
            _context.SaveChanges();
        }
        public Option GetOption(int optionId)
        {
            var option = _context.Options.Find(optionId);

            if (option == null)
            {
                throw new Exception("Cannot find Option with Id = " + optionId);
            }
            return option;
        }
        public void UpdateOption(Option option)
        {
            _context.Options.Update(option);
            _context.SaveChanges();
        }

        public void AddOptionToOptionSet(OptionSetOption optionSetOption)
        {
            _context.OptionSetOptions.Add(optionSetOption);
            _context.SaveChanges();
        }

        public void UpdateOptionSetOption(int optionId, int optionSetId, decimal price)
        {
            var optionSetOption = _context.OptionSetOptions.Find(optionId, optionSetId);

            if (optionSetOption == null)
            {
                throw new Exception("OptionSetOption not found");
            }

            optionSetOption.Price = price;
            _context.SaveChanges();
        }

        public void RemoveOption(int id, int optionSetId)
        {
            var optionSetOption = _context.OptionSetOptions.FirstOrDefault(x => x.OptionId == id && x.OptionSetId == optionSetId);

            if (optionSetOption == null)
            {
                throw new Exception("OptionSetOption not found");
            }

            _context.OptionSetOptions.Remove(optionSetOption);
            _context.SaveChanges();
        }
    }
}
