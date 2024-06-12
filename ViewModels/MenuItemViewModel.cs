using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace asp.net_1.ViewModels
{
    public class MenuItemViewModel
    {
     
        public string Name { get; set; }

  
        public string Description { get; set; }

   
        public decimal Price { get; set; }

        public IFormFile Image { get; set; }
    }
}
