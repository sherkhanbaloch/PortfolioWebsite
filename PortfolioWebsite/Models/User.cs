using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
    }
}
