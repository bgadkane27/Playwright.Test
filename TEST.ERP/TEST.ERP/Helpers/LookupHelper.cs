using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.ERP.Helpers
{
    public class LookupHelper
    {
        private readonly IPage _page;

        public LookupHelper(IPage page)
        {
            _page = page;
        }

        // ============================================================
        // 1) Select an option in the header entity lookup using pagination.
        // ============================================================
        public async Task SelectHeaderLookupText(string optionText)
        {
            // Locator for each lookup item
            var lookupItems = _page.Locator("td .lookup-text");

            bool optionFound = false;

            while (!optionFound)
            {
                // Wait for lookup list to load
                await lookupItems.First.WaitForAsync();

                // Get count
                int count = await lookupItems.CountAsync();

                for (int i = 0; i < count; i++)
                {
                    string text = await lookupItems.Nth(i).InnerTextAsync();

                    if (text.Contains(optionText, StringComparison.OrdinalIgnoreCase))
                    {
                        // Scroll into view and click
                        await lookupItems.Nth(i).ScrollIntoViewIfNeededAsync();
                        await lookupItems.Nth(i).ClickAsync();
                        await _page.WaitForTimeoutAsync(1000);
                        optionFound = true;
                        break;
                    }
                }

                if (optionFound)
                    break;

                // Locator for NEXT button
                var nextButton = _page.GetByRole(AriaRole.Button, new() { Name = "next-icon" });

                // Check if NEXT button is disabled → stop searching
                bool isNextDisabled = await nextButton.IsDisabledAsync();
                if (isNextDisabled)
                    throw new Exception($"Option '{optionText}' not found.");

                // Go to next page
                await nextButton.ClickAsync();
                await _page.WaitForTimeoutAsync(500);
            }
        }
        // ============================================================
        // 2) Select an option - Directly
        // ============================================================
        public async Task SelectLookupText(string optionText)
        {
            var locator = _page.Locator($"td .lookup-text:has-text('{optionText}')");

            // Wait for at least one match
            await locator.First.WaitForAsync();

            await locator.First.ScrollIntoViewIfNeededAsync();
            await locator.First.ClickAsync();
            await _page.WaitForTimeoutAsync(500);
        }
        // ============================================================
        // 3) Select an option in the line entity lookup using pagination.
        // ============================================================
        public async Task SelectLineLookupText(string optionText)
        {
            // Locator for each lookup item
            var lookupItems = _page.Locator("td .lookup-text");

            bool optionFound = false;

            while (!optionFound)
            {
                // Wait for lookup list to load
                await lookupItems.First.WaitForAsync();

                // Get count
                int count = await lookupItems.CountAsync();

                for (int i = 0; i < count; i++)
                {
                    string text = await lookupItems.Nth(i).InnerTextAsync();

                    if (text.Contains(optionText, StringComparison.OrdinalIgnoreCase))
                    {
                        // Scroll into view and click
                        await lookupItems.Nth(i).ScrollIntoViewIfNeededAsync();
                        await lookupItems.Nth(i).ClickAsync();
                        await _page.WaitForTimeoutAsync(1000);
                        optionFound = true;
                        break;
                    }
                }

                if (optionFound)
                    break;

                // Locator for NEXT button
                var nextButton = _page.GetByAltText("Next");

                // Check if NEXT button is disabled → stop searching
                bool isNextDisabled = await nextButton.IsDisabledAsync();
                if (isNextDisabled)
                    throw new Exception($"Option '{optionText}' not found.");

                // Go to next page
                await nextButton.ClickAsync();
                await _page.WaitForTimeoutAsync(500);
            }
        }
        // ============================================================
        // 4) Select an option using Data Row
        // ============================================================
        public async Task SelectLookupDataRow(string optionText)
        {
            // All lookup rows
            var rows = _page.Locator("tr.dxgvDataRow_Office365");

            // Ensure rows are loaded
            await rows.First.WaitForAsync();

            // Filter matching row (supports partial text)
            var match = rows.Filter(new() { HasText = optionText });

            // Validate if a matching row exists
            if (await match.CountAsync() == 0)
                throw new Exception($"Option '{optionText}' not found.");

            // Click the first matching row
            await match.First.ScrollIntoViewIfNeededAsync();
            await match.First.ClickAsync();
            await _page.WaitForTimeoutAsync(1000);
        }
        // ============================================================
        // 5) Select an option using Box Item Row
        // ============================================================
        public async Task SelectLookupBoxItemRow(string optionText)
        {
            // All lookup rows
            var rows = _page.Locator("//tr[@class='dxeListBoxItemRow_Office365']");

            // Ensure rows are loaded
            //await rows.First.WaitForAsync();

            // Filter matching row (supports partial text)
            var match = rows.Filter(new() { HasText = optionText });

            int matchCount = await match.CountAsync();

            if (matchCount == 0)
                throw new Exception($"Option '{optionText}' not found.");

            await match.First.ScrollIntoViewIfNeededAsync();
            await match.First.ClickAsync();

            await _page.WaitForTimeoutAsync(500);
        }
        // ============================================================
        // 6) Select an option using List Item Content
        // ============================================================
        public async Task SelectLookupListItem(string optionText)
        {
            // All items in the lookup list
            var items = _page.Locator(".dx-list-item .dx-item-content");

            // Ensure the list has loaded
            await items.First.WaitForAsync();

            // Filter items using text (supports partial match)
            var match = items.Filter(new() { HasText = optionText });

            int count = await match.CountAsync();

            if (count == 0)
                throw new Exception($"Option '{optionText}' not found.");

            // Click the first match
            await match.First.ScrollIntoViewIfNeededAsync();
            await match.First.ClickAsync();

            await _page.WaitForTimeoutAsync(300);
        }
        // ============================================================
        // 7) Select an option using List Item By Role
        // ============================================================
        public async Task SelectLookupListItem_ByRole(string optionText)
        {
            var listBox = _page.GetByRole(AriaRole.Listbox, new() { Name = "Items" });
            var option = listBox.GetByRole(AriaRole.Option, new() { Name = optionText });

            await option.ClickAsync();
        }
        // ============================================================
        // 8) Select an option using List Item By Aria Label
        // ============================================================
        public async Task SelectLookupListItem_ByAriaLabel(string optionText)
        {
            var list = _page.Locator("[aria-label='Items']");
            var option = list.Locator("div[role='option']", new() { HasTextString = optionText });

            await option.ClickAsync();
        }
        // ============================================================
        // 9) Select an option using List Item By CSS
        // ============================================================
        public async Task SelectLookupListItem_ByCss(string optionText)
        {
            var option = _page.Locator(".dx-list-item >> text=" + optionText);
            await option.ClickAsync();
        }
        // ============================================================
        // 10) Select an option using List Item By Scroll
        // ============================================================
        public async Task SelectLookupItemByScroll(string optionText)
        {
            // List container that actually scrolls
            var scrollContainer = _page.Locator(".dx-scrollview-content");

            // Target option inside the list
            var item = _page.GetByRole(AriaRole.Option, new() { Name = optionText });

            // Try up to 20 scroll attempts (adjust for long lists)
            for (int i = 0; i < 20; i++)
            {
                if (await item.IsVisibleAsync())
                {
                    await item.ClickAsync();
                    return;
                }
                // Scroll down by 300px
                await scrollContainer.EvaluateAsync("el => el.scrollTop += 300");

                // Small wait for rendering
                await Task.Delay(200);
            }
            throw new Exception($"Option '{optionText}' not found.");
        }
        // ============================================================
        // 11) Select an option contains provided text
        // ============================================================
        public async Task SelectLookUpOption(string optionText)
        {
            var options = _page.Locator($"//div[contains(normalize-space(), '{optionText}')]");

            // Wait until at least 1 option is available
            await options.First.WaitForAsync();

            await options.First.ClickAsync();
            await _page.WaitForTimeoutAsync(500);
        }
    }
}
