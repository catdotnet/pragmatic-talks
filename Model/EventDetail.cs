using System;
using System.Collections.Generic;

namespace PragmaticTalks.Model
{
    public class EventDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public string Url { get; set; }

        public IEnumerable<string> Talks { get; set; }

        public IEnumerable<TagItem> Tags { get; set; }

        public IEnumerable<SpeakerItem> Speakers { get; set; }
    }
}
