using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Events.Services;

namespace ThAmCo.Events.Models.Events
{
    public class EventMenusVM
    {
        public EventMenusVM(int eventId, IEnumerable<MenuDto> menus, string message)
        {
            EventId = eventId;
            Menus = menus;
            Message = message;
        }

        public int EventId { get; set; }

        public IEnumerable<MenuDto> Menus { get; set; }

        public string Message { get; set; }
    }
}
