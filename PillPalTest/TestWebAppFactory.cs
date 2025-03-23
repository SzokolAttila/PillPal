using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PillPalAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest
{
    public class TestWebAppFactory<Webapp> : WebApplicationFactory<Webapp> where Webapp : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string dbName = Guid.NewGuid().ToString(); // generate new DB name for each test
            builder.UseSetting("testing", "true");
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<DatabaseContext>(options =>
                { 
                    options.UseInMemoryDatabase(dbName); // use the same DB for all request in a test
                });
            });
        }

    }
}
