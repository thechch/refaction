using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace refactor_me
{
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
    }
}