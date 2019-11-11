using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.ViewModels.Events
{
    public class EditEventVM
    {
        public EditEventVM()
        {

        }
        public EditEventVM(int id, string title, TimeSpan duration)
        {
            Id = id;
            Title = title;
            Duration = duration;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan? Duration { get; set; }
    }
}
