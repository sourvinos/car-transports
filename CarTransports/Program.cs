using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CarTransports
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(c => c.AddServerHeader = false)
                .Build();
    }
}
