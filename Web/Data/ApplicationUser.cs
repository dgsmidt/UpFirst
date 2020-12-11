using Microsoft.AspNetCore.Identity;

namespace Web.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string Nome { get; set; }
        public string WhatsApp { get; set; }

    }
}
