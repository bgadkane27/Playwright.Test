using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace TEST.ERP.Utilities
{
    public static class BrowserFactory
    {
        public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var browserName = config["Browser"]?.ToLower() ?? "chrome";

            return browserName switch
            {
                "chrome" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Channel = "chrome",
                    Headless = false,
                    SlowMo = 200
                }),

                "edge" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Channel = "msedge",
                    Headless = false,
                    SlowMo = 200
                }),

                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false,
                    SlowMo = 200
                }),

                _ => throw new Exception($"Unsupported browser: {browserName}")
            };
        }
    }
}
