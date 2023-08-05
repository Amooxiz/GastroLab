using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Domain.Models
{
    public enum OrderStatus
    {
        New = 0,
        OnHold = 1,
        InProgress = 2,
        Done = 3,
        Delivered = 4,
        Finished = 5,
        Canceled = 6
    }
}
