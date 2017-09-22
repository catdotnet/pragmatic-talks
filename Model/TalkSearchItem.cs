using System;

namespace PragmaticTalks.Model
{
    public class TalkSearchItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateCreation { get; set; }

        public bool IsSelected { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAssignedToEvent { get; set; }

        public string SpeakerName { get; set; }
    }
}
