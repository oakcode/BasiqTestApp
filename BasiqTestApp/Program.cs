using BasiqTestApp.DataAccess;
using BasiqTestApp.Model;
using BasiqTestApp.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasiqTestApp
{
    class Program
    {
        static Manager manager = new Manager();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to average spending calculator.");

            Settings.ParseArguments(args);

            if(Settings.ShowHelp)
            {
                Console.WriteLine("k|key - Basiq access api key");
                Console.WriteLine("l|loop - Run calculation in the loop");
                Console.WriteLine("u|url - Basiq api address");
                Console.WriteLine("s|server - Start web server");
                return;
            }

            if(string.IsNullOrEmpty(Settings.AccessApiKey))
            {
                Console.WriteLine("k|key - Basiq access api key is mandatory");
                return;
            }

            if (Settings.StartWebServer)
            {
                CreateHostBuilder(args).Build().Run();
            }
            else
            {
                int c = 1;
                do
                {
                    Console.Write("Please enter user id [Empty for default]: ");
                    string userId = Console.ReadLine();
                    if (string.IsNullOrEmpty(userId))
                    {
                        userId = Settings.DefaultUserId;
                    }

                    int code = 0;
                    string codeAsString = string.Empty;
                    do
                    {
                        Console.Write("Please enter category code: ");
                        codeAsString = Console.ReadLine();
                    } while (!int.TryParse(codeAsString, out code));

                    float average = await manager.GetAverageValueAsync(userId, code);
                    Console.WriteLine($"Average spending per category '{code}': {average}");

                    Console.WriteLine("Press <Enter> to run again or 'Ctrl->C' to exit");
                    c = Console.Read();
                    Console.Clear();
                } while (Settings.Loop && c != 0);

                Console.ReadKey();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}