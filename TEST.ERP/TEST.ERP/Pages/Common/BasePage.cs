using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.ERP.Pages.Common
{
    public class BasePage
    {
        private readonly IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
        }

        #region Locators
        private const string userName = "input[name='Username']";
        private const string passWord = "input[name='Password']";
        private const string loginBtn = ".login-btn";
        #endregion

        #region Actions
        public async Task GotoUrl(string url)
        {
            await _page.GotoAsync(url);
        }
        public async Task FillUsername(string username)
        {
            await _page.Locator(userName).FillAsync(username);
        }
        public async Task FillPassword(string password)
        {
            await _page.Locator(passWord).FillAsync(password);
        }
        public async Task ClickOnLogin()
        {
            await _page.Locator(loginBtn).ClickAsync();
        }
        #endregion
    }
}
