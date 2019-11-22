using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThAmCo.Events.Models.Events
{
    public class EventToEditVM
    {
        public EventToEditVM()
        {

        }
        public EventToEditVM(int id, string title, DateTime date, TimeSpan? duration, string typeId, SelectList venues)
        {
            Id = id;
            Title = title;
            Date = date;
            Duration = duration;
            TypeId = typeId;
            Venues = venues;
        }

        public EventToEditVM(EventVM @event, SelectList venues)
        {
            Id = @event.Id;
            Title = @event.Title;
            Date = @event.Date;
            Duration = @event.Duration;
            TypeId = @event.TypeId;
            Venues = venues;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan? Duration { get; set; }

        public string TypeId { get; set; }

        public SelectList Venues { get; set; }
    }
}
