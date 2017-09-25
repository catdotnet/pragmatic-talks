using System;
using System.Collections.Generic;

namespace PragmaticTalks.Data
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public string Url { get; set; }

        public ICollection<Talk> Talks { get; set; }
    }
}
