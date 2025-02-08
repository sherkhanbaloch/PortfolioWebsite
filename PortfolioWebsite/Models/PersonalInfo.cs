using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class PersonalInfo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string HomeImage { get; set; }
        public string AboutImage { get; set; }
        public string Description { get; set; }
        public string CVLink { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string LinkedInURL { get; set; }
        public string LinkedInTitle { get; set; }
        public string InstagramURL { get; set; }
        public string InstagramTitle { get; set; }
        public string GitHubURL { get; set; }
        public string GitHubTitle { get; set; }
    }
}
