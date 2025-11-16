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
    }
}
