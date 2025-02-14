namespace PortfolioWebsite.ViewModel
{
    public class UserVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public IFormFile? Image { get; set; }
    }
}
