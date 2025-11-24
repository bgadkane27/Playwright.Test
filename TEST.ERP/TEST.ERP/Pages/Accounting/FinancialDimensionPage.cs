using Microsoft.Playwright;
using TEST.ERP.Helpers;

namespace TEST.ERP.Pages.Accounting
{
    public class FinancialDimensionPage
    {
        private readonly IPage _page;
        private readonly LookupHelper _lookupHelper;

        private readonly ILocator _financialDimension;

        // Dictionary to hold all segment icon locators
        private readonly Dictionary<string, ILocator> _segments;

        // Dictionary to hold all segment input fields
        private readonly Dictionary<string, ILocator> _segmentFields;

        public FinancialDimensionPage(IPage page)
        {
            _page = page;
            _lookupHelper = new LookupHelper(page);

            #region Form Title
            _financialDimension = _page.GetByRole(AriaRole.Link, new() { Name = "Financial Dimension" });
            #endregion

            #region Main Info (Using Dictionary)
            _segments = new Dictionary<string, ILocator>
            {
                { "Segment1", page.Locator("//img[contains(@id, '.Segment1_B-1Img')]") },
                { "Segment2", page.Locator("//img[contains(@id, '.Segment2_B-1Img')]") },
                { "Segment3", page.Locator("//img[contains(@id, '.Segment3_B-1Img')]") },
                { "Segment4", page.Locator("//img[contains(@id, '.Segment4_B-1Img')]") },
                { "Segment5", page.Locator("//img[contains(@id, '.Segment5_B-1Img')]") }
            };

            _segmentFields = new Dictionary<string, ILocator>
            {
                { "Segment1", page.Locator("//input[contains(@id, 'Segment1_I')]") },
                { "Segment2", page.Locator("//input[contains(@id, 'Segment2_I')]") },
                { "Segment3", page.Locator("//input[contains(@id, 'Segment3_I')]") },
                { "Segment4", page.Locator("//input[contains(@id, 'Segment4_I')]") },
                { "Segment5", page.Locator("//input[contains(@id, 'Segment5_I')]") }
            };
            #endregion
        }

        #region Actions

        #region Form Title
        public async Task ClickOnFinancialDimension()
        {
            await _financialDimension.ClickAsync();
        }
        #endregion

        #region Main Info        
        public async Task SelectSegment(string segmentName, string value)
        {
            
            await _segments[segmentName].ClickAsync();
            //await _segmentFields[segmentName].FillAsync(value);
            //await _page.WaitForTimeoutAsync(500); 
            await _lookupHelper.SelectLookupBoxItemRow(value);
        }       
        #endregion

        #endregion
    }
}