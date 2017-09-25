using System.ComponentModel.DataAnnotations;

namespace PragmaticTalks.Model
{
    public class TalkCreation
    {
        [Required]
        [MaxLength(37)]
        public string Title { get; set; }

        public string[] Tags { get; set; }

        public string Language { get; set; }
    }
}
