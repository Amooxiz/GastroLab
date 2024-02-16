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
        public int? AddressId { get; set; }
        public virtual ICollection<Address>? Address { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<WorkingTime>? WorkingTimes { get; set; }
        public virtual ICollection<RegisteredTime>? RegisteredTimes { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }
    }
}
