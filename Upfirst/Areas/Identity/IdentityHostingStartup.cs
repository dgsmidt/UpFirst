﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Upfirst.Areas.Identity.IdentityHostingStartup))]
namespace Upfirst.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}