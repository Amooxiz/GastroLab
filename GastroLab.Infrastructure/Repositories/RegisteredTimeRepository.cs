using GastroLab.Application.Interfaces;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Infrastructure.Repositories
{
    public class RegisteredTimeRepository : IRegisteredTimeRepository
    {
        private readonly GastroLabDbContext _context;

        public RegisteredTimeRepository(GastroLabDbContext context)
        {
            _context = context;
        }

        public void RegisterTime(RegisteredTime registeredTime)
        {
            _context.Add(registeredTime);
            _context.SaveChanges();
        }
    }
}
