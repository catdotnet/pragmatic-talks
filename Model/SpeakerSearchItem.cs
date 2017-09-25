namespace PragmaticTalks.Model
{
    public class SpeakerSearchItem
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsBlocked { get; set; }

        public int TalksCounter { get; set; }
    }
}
