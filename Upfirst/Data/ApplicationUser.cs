using Microsoft.AspNetCore.Identity;

namespace Upfirst.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string Nome { get; set; }
        public string WhatsApp { get; set; }

    }
}
