﻿using System.ComponentModel.DataAnnotations;

namespace PortfolioWebsite.ViewModel
{
    public class ProjectVM
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        [DataType(DataType.Url)]
        public string ProjectLink { get; set; }
        public IFormFile? ProjectImage { get; set; }

        public int CategoryId { get; set; }
    }
}
