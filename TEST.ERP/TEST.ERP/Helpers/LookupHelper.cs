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
        // 1) CLICK DROPDOWN TO OPEN IT
        // ============================================================
        private async Task OpenDropdownAsync(string dropdownSelector)
        {
            await _page.Locator(dropdownSelector).ClickAsync();
            await _page.WaitForTimeoutAsync(200); // small stabilization delay
        }

        // ============================================================
        // 2) WAIT FOR LIST TO APPEAR
        // ============================================================
        private async Task WaitForListToLoadAsync()
        {
            await _page.Locator(".dx-scrollable-content").WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible
            });
        }

        // ============================================================
        // 3) SIMPLE SELECT (Exact Match)
        // ============================================================
        public async Task SelectOptionExactAsync(string dropdownSelector, string optionText)
        {
            await OpenDropdownAsync(dropdownSelector);
            await WaitForListToLoadAsync();

            var option = _page.GetByRole(AriaRole.Option, new() { Name = optionText });
            await option.First.ClickAsync();
        }

        // ============================================================
        // 4) SIMPLE SELECT (Partial Match)
        // ============================================================
        public async Task SelectOptionContainsAsync(string dropdownSelector, string partialText)
        {
            await OpenDropdownAsync(dropdownSelector);
            await WaitForListToLoadAsync();

            var option = _page.GetByRole(AriaRole.Option)
                              .Filter(new() { HasText = partialText });

            await option.First.ClickAsync();
        }

        // ============================================================
        // 5) SELECT WITH EXISTENCE CHECK
        // ============================================================
        public async Task<bool> TrySelectOptionAsync(string dropdownSelector, string optionText)
        {
            await OpenDropdownAsync(dropdownSelector);
            await WaitForListToLoadAsync();

            var option = _page.GetByRole(AriaRole.Option, new() { Name = optionText });

            if (await option.CountAsync() == 0)
                return false;

            await option.First.ClickAsync();
            return true;
        }

        // ============================================================
        // 6) SCROLL + SELECT (Exact Match)
        // ============================================================
        public async Task ScrollAndSelectOptionAsync(string dropdownSelector, string optionText)
        {
            await OpenDropdownAsync(dropdownSelector);
            await WaitForListToLoadAsync();

            var container = _page.Locator(".dx-scrollable-content");

            for (int i = 0; i < 20; i++)
            {
                var option = _page.GetByRole(AriaRole.Option, new() { Name = optionText });

                if (await option.CountAsync() > 0)
                {
                    await option.First.ScrollIntoViewIfNeededAsync();
                    await option.First.ClickAsync();
                    return;
                }

                // Scroll down a bit
                await container.EvaluateAsync("el => el.scrollBy(0, 300)");
                await _page.WaitForTimeoutAsync(120);
            }

            throw new Exception($"Option '{optionText}' was not found after scrolling.");
        }

        // ============================================================
        // 7) SCROLL + SELECT (Partial Match)
        // ============================================================
        public async Task ScrollAndSelectOptionContainsAsync(string dropdownSelector, string partialText)
        {
            await OpenDropdownAsync(dropdownSelector);
            await WaitForListToLoadAsync();

            var container = _page.Locator(".dx-scrollable-content");

            for (int i = 0; i < 20; i++)
            {
                var option = _page.GetByRole(AriaRole.Option)
                                  .Filter(new() { HasText = partialText });

                if (await option.CountAsync() > 0)
                {
                    await option.First.ScrollIntoViewIfNeededAsync();
                    await option.First.ClickAsync();
                    return;
                }

                await container.EvaluateAsync("el => el.scrollBy(0, 300)");
                await _page.WaitForTimeoutAsync(150);
            }

            throw new Exception($"Option containing '{partialText}' was not found after scrolling.");
        }

        // ============================================================
        // 8) Select an option in the header entity lookup using pagination.
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
        // 9) Select an option - simpler
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
        // 10) Select an option in the line entity lookup using pagination.
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
        // 11) Select an option in the line entity lookup using pagination.
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

    }
}
