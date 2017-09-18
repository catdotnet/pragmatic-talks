using Microsoft.AspNetCore.Identity;

namespace PragmaticTalks.Data
{
    public class Speaker : IdentityUser
    {
        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsAdministrator { get; set; }

        public int TalksCounter { get; set; }
    }
}