using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Services
{
    public class MenuDto
    {
        public MenuDto()
        {

        }

        public MenuDto(string message)
        {
            Message = message;
        }

        public int MenuId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Menu cost per guest")]
        public double CostPerHead { get; set; }

        public string Starter { get; set; }

        [Required]
        public string Main { get; set; }

        public string Dessert { get; set; }

        public string Message { get; set; }
    }
}
