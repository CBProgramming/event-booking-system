using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Venues.Data
{
    public class EventTypeDto
    {
        [MinLength(3), MaxLength(3)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
