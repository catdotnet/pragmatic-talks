namespace PragmaticTalks.Model
{
    public class Speaker
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsAdministrator { get; set; }

        public int TalksCounter { get; set; }
    }
}