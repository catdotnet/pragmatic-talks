using System.Collections.Generic;

namespace PragmaticTalks.Model
{
    public class TalkItem
    {
        public string Title { get; set; }

        public string Language { get; set; }

        public IEnumerable<TagItem> Tags { get; set; }
    }
}