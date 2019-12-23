using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.Models
{
    public class MenuDto
    {
        public MenuDto()
        {

        }

        public MenuDto(MenuDto menu)
        {
            MenuId = menu.MenuId;
            Name = menu.Name;
            CostPerHead = menu.CostPerHead;
            Starter = menu.Starter;
            Main = menu.Main;
            Dessert = menu.Dessert;
        }

        public MenuDto(Menu menu)
        {
            MenuId = menu.MenuId;
            Name = menu.Name;
            CostPerHead = menu.CostPerHead;
            Starter = menu.Starter;
            Main = menu.Main;
            Dessert = menu.Dessert;
        }

        public int MenuId { get; set; }

        public string Name { get; set; }

        public double CostPerHead { get; set; }

        public string Starter { get; set; }

        public string Main { get; set; }

        public string Dessert { get; set; }
    }
}
