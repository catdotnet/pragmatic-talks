using System;
using System.Collections.Generic;

namespace PragmaticTalks.Data
{
    public class Talk
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateCreation { get; set; }

        public string Language { get; set; }

        public bool IsSelected { get; set; }

        public bool IsDeleted { get; set; }

        public int SpeakerTalkCount { get; set; }

        public int? EventId { get; set; }

        public Event Event { get; set; }

        public Speaker Speaker { get; set; }

        public ICollection<TalkTag> Tags { get; set; }
    }
}
