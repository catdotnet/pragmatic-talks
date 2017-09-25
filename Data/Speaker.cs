using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PragmaticTalks.Data
{
    public class Speaker : IdentityUser
    {
        public string DisplayName { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsBlocked { get; set; }

        public int TalksCounter { get; set; }

        public IEnumerable<Talk> Talks { get; set; }
    }
}