using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.ERP.Pages.Accounting
{
    public class AccountingSetupPage
    {
        private readonly IPage _page;

        public AccountingSetupPage(IPage page)
        {
            _page = page;
        }

        #region Locators
        private const string financialDimension = "text=Financial Dimension";
        #endregion

        #region Actions       
        public async Task ClickOnFinancialDimension()
        {
            await _page.ClickAsync(financialDimension);
        }
        #endregion
    }
}
