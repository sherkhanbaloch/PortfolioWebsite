using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceImage { get; set; }
    }
}
