using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IEmailConfigService
    {
        string EmailContato();
        string From();
    }
}
