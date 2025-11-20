using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using TEST.ERP.Helpers;
using TEST.ERP.Models.Common.Login;
using TEST.ERP.Pages.Common;
using TEST.ERP.Utilities;

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
            playwright = await Playwright.CreateAsync();

            browser = await BrowserFactory.CreateBrowserAsync(playwright);

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
            var loginPage = new LoginPage(_page);
            #endregion

            #region Visit URL and Login
            await loginPage.GotoUrl(loginModel.Url);
            await loginPage.FillUsername(loginModel.Username);
            await loginPage.FillPassword(loginModel.Password);
            await loginPage.ClickOnLogin();
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
