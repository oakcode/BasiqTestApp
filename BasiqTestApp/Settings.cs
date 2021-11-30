using Mono.Options;
using System;
using System.Collections.Generic;

namespace BasiqTestApp
{
    internal class Settings
    {
        private static bool showHelp = false;
        private static string url = "https://au-api.basiq.io";
        private static bool loop = false;
        private static bool startWebServer = false;

        public static string DefaultUserId = "3d882406-6faa-45b3-a266-c8f19d2a4388";

        public static bool ShowHelp { get { return showHelp; } set { showHelp = value; } }
        public static bool Loop { get { return loop; } set { loop = value; } }
        public static bool StartWebServer { get { return startWebServer; } set { startWebServer = value; } }
        public static string Url { get { return url; } set { url = value; } }
        public static string AccessApiKey { get; set; }

        public static void ParseArguments(string[] args)
        {
            var p = new OptionSet()
                {
                    { "k|key=", "Basiq access api key",
                       v => AccessApiKey = v },
                    { "u|url=", "Basiq default URL",
                       v => url = v },
                    { "l|loop",
                       "Repete in the loop",
                        v => loop = true },
                    { "s|server",
                       "Start web server",
                        v => startWebServer = true },
                    { "h|help",  "show this message and exit",
                       v => showHelp = v != null },
                };

            List<string> extra = null;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine("Failed to parse options: " + e.Message);
                Console.WriteLine(@"Run 'dotnet .\BasiqTestApp.dll -help' for more details");
                return;
            }
        }
    }
}
