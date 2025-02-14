using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.ViewModel
{
    public class PersonalInfoVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public IFormFile? HomeImage { get; set; }
        public IFormFile? AboutImage { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Url)]
        public string CVLink { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Location { get; set; }

        [DataType(DataType.Url)]
        public string LinkedInURL { get; set; }
        public string LinkedInTitle { get; set; }

        [DataType(DataType.Url)]
        public string InstagramURL { get; set; }
        public string InstagramTitle { get; set; }

        [DataType(DataType.Url)]
        public string GitHubURL { get; set; }
        public string GitHubTitle { get; set; }
    }
}
