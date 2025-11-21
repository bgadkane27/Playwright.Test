using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using TEST.ERP.Helpers;
using TEST.ERP.Utilities;

namespace TEST.ERP.Pages.Accounting
{
    public class FinancialDimensionPage
    {
        private readonly IPage _page;
        private readonly LookupHelper _lookupHelper;

        public FinancialDimensionPage(IPage page)
        {
            _page = page;
            _lookupHelper = new LookupHelper(page);
        }

        #region Locators

        #region Form Title
        private const string financialDimension = "Financial Dimension";
        #endregion

        #region Main Info
        private const string segment1 = "(//img[contains(@id, '.Segment1_B-1Img')])";
        private const string segment1Field = "input[id*='Segment1_I']";
        private const string segment2 = "(//img[contains(@id, '.Segment2_B-1Img')])";
        private const string segment2Field = "input[id*='Segment2_I']";
        private const string segment3 = "(//img[contains(@id, '.Segment3_B-1Img')])";
        private const string segment3Field = "input[id*='Segment3_I']";
        private const string segment4 = "(//img[contains(@id, '.Segment4_B-1Img')])";
        private const string segment4Field = "input[id*='Segment4_I']";
        private const string segment5 = "(//img[contains(@id, '.Segment5_B-1Img')])";
        private const string segment5Field = "input[id*='Segment5_I']";
        #endregion

        #endregion

        #region Actions

        #region Form Title
        public async Task ClickOnFinancialDimension()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = financialDimension }).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        #endregion

        #region Main Info
        public async Task SelectSegment1(string data)
        {
            await _page.Locator(segment1).ClickAsync();
            //await _page.Locator(segment1Field).FillAsync(data);
            //await _page.WaitForTimeoutAsync(500);  
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment2(string data)
        {
            await _page.Locator(segment2).ClickAsync();
            //await _page.Locator(segment2Field).FillAsync(data);
            //await _page.WaitForTimeoutAsync(500);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment3(string data)
        {
            await _page.Locator(segment3).ClickAsync();
            //await _page.Locator(segment3Field).FillAsync(data);
            //await _page.WaitForTimeoutAsync(500);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment4(string data)
        {
            await _page.Locator(segment4).ClickAsync();
            //await _page.Locator(segment4Field).FillAsync(data);
            //await _page.WaitForTimeoutAsync(500);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment5(string data)
        {
            await _page.Locator(segment5).ClickAsync();
            //await _page.Locator(segment5Field).FillAsync(data);
            //await _page.WaitForTimeoutAsync(500);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }
        #endregion

        #endregion
    }
}
