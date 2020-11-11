using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BiaBraga.Config
{
    public class Program
    {
        private static readonly CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.RunAsync(cancelTokenSource.Token).GetAwaiter().GetResult();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void Shutdown()
        {
            cancelTokenSource.Cancel();
        }
    }
}
