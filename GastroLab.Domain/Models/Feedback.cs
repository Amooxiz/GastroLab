using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public string? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
