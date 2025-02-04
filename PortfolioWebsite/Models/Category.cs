using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
