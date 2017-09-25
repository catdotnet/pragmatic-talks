namespace PragmaticTalks.Model
{
    public class TalkListItem
    {
        public string Title { get; set; }

        public bool IsSelected { get; set; }

        public TagItem[] Tags { get; set; }

        public string Language { get; set; }
    }
}
