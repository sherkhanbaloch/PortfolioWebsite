using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioWebsite.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string SkillName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
