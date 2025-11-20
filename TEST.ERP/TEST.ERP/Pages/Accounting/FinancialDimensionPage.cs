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
        private const string financialDimension = "text=Financial Dimension";
        #endregion

        #region Main Info
        private const string segment1 = "(//td[contains(@id, '.Segment1_B-1')])[2]";
        private const string segment2 = "(//td[contains(@id, '.Segment2_B-1')])[2]";
        private const string segment3 = "(//td[contains(@id, '.Segment3_B-1')])[2]";
        private const string segment4 = "(//td[contains(@id, '.Segment4_B-1')])[2]";
        private const string segment5 = "(//td[contains(@id, '.Segment5_B-1')])[2]";
        #endregion

        #endregion

        #region Actions

        #region Form Title
        public async Task ClickOnFinancialDimension()
        {
            await _page.GetByRole(AriaRole.Link, new() { Name = "Financial Dimension" }).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        #endregion

        #region Main Info
        public async Task SelectSegment1(string data)
        {
            await _page.Locator(segment1).ClickAsync();
            //await _page.WaitForTimeoutAsync(500);            
            //await SelectDropDownData(data);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment2(string data)
        {
            await _page.Locator(segment2).ClickAsync();
            //await _page.WaitForTimeoutAsync(500);
            //await SelectDropDownData(data);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment3(string data)
        {
            await _page.Locator(segment3).ClickAsync();
            //await _page.WaitForTimeoutAsync(500);
            //await SelectDropDownData(data);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment4(string data)
        {
            await _page.Locator(segment4).ClickAsync();
            //await _page.WaitForTimeoutAsync(500);
            //await SelectDropDownData(data);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }

        public async Task SelectSegment5(string data)
        {
            await _page.Locator(segment5).ClickAsync();
            //await _page.WaitForTimeoutAsync(500);
            //await SelectDropDownData(data);
            await _lookupHelper.SelectLookupBoxItemRow(data);
        }
        #endregion

        #region Common Utility
        public async Task SelectDropDownData(string option)
        {
            var dropdownList = _page.Locator("//tr[@class='dxeListBoxItemRow_Office365']");
            int count = await dropdownList.CountAsync();

            //await _page.WaitForTimeoutAsync(500);

            for (int i = 0; i < count; i++)
            {
                var element = dropdownList.Nth(i);
                string actualValue = (await element.InnerTextAsync()).Trim();

                if (actualValue.Contains(option))
                {
                    await element.ClickAsync();
                    break;
                }
            }
            //await _page.WaitForTimeoutAsync(2000);
        }
        #endregion

        #endregion
    }
}
