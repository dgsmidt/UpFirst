using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upfirst.Services
{
    public class EmailConfigService : IEmailConfigService
    {
        private readonly UpFirstDbContext _dbContext;
        public EmailConfigService(UpFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string EmailContato()
        {
            string emailContato = _dbContext.Configuracoes.Select(c => c.EmailContato).FirstOrDefault();

            return emailContato;
            //throw new NotImplementedException();
        }

        public string From()
        {
            string from = _dbContext.Configuracoes.Select(c => c.Titulo).FirstOrDefault();

            return from;
            //throw new NotImplementedException();
        }
    }
}
