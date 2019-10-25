namespace Transavia.Web.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string Email { get; set; }

    }
}
