using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace asp.net_1.ViewModels
{
    public class ActionFViewModel
    {
     
        public string Title { get; set; }

    
        public string Description { get; set; }

     
        public string Details { get; set; }

        public IFormFile Image { get; set; }
    }
}
