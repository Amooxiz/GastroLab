using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroLab.Application.ViewModels
{
    public class ProductListVM
    {
        public IEnumerable<ProductVM> Products { get; set; }
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
