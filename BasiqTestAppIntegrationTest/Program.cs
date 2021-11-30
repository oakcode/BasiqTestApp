using BasiqTestAppIntegrationTest.TestCases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasiqTestAppIntegrationTest
{
    public class Program
    {
        private static TestCaseRunner testCaseRunner = new TestCaseRunner();
        public static void Main(string[] args)
        {
            Task.Run(() => CreateHostBuilder(args).Build().Run());
            Console.WriteLine("Press any key to start testing");
            Console.ReadKey();
            testCaseRunner.RunTests();
            testCaseRunner.PrintResults();
            Console.ReadKey();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:8081/");
                });
    }
}
