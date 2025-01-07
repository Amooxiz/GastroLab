using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.DBO
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public virtual ICollection<WorkingTime>? WorkingTimes { get; set; }
        public virtual ICollection<RegisteredTime>? RegisteredTimes { get; set; }
    }
}
