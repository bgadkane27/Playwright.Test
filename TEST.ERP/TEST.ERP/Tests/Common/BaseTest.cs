using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using TEST.ERP.Helpers;
using TEST.ERP.Models.Common.Login;
using TEST.ERP.Pages.Common;

namespace TEST.ERP.Tests.Common
{
    public abstract class BaseTest
    {
        protected IPlaywright playwright;
        protected IBrowser browser;
        protected IBrowserContext context;
        protected IPage _page;

        #region SetUp
        [SetUp]
        public async Task SetUp()
        {
            playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 200,
                Channel = "chrome"   
            });

            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null
            });

            _page = await context.NewPageAsync();
            
            await Login();
        }
        #endregion

        #region Login
        private async Task Login()
        {
            #region Load Test Data
            var dataFile = DataHelper.GetDataFile("Common", "Login", "LoginData");
            var loginModel = DataHelper.ConvertJsonDataModel<LoginDM>(dataFile);
            #endregion

            #region Page Objects
            var basePage = new BasePage(_page);
            #endregion

            #region Visit URL and Login
            await basePage.GotoUrl(loginModel.Url);
            await basePage.FillUsername(loginModel.Username);
            await basePage.FillPassword(loginModel.Password);
            await basePage.ClickOnLogin();
            #endregion
        }
        #endregion

        #region TearDown
        [TearDown]
        public async Task TearDown()
        {
            await browser.CloseAsync();
        }
        #endregion
    }
}
