using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeAreFootball.Data;
using WeAreFootball.Data.Models;

[assembly: HostingStartup(typeof(AspNetCoreTemplate.Web.Areas.Identity.IdentityHostingStartup))]

namespace AspNetCoreTemplate.Web.Areas.Identity
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