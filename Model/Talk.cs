using System;
using System.Collections.Generic;

namespace PragmaticTalks.Model
{
    public class Talk
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateCreation { get; set; }

        public bool IsSelected { get; set; }

        public bool IsDeleted { get; set; }

        public Speaker Speaker { get; set; }

        public ICollection<TalkTag> Tags { get; set; }
    }
}
