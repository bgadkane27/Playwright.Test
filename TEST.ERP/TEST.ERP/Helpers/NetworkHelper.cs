using Microsoft.Playwright;
using System.Text.Json;

namespace TEST.ERP.Helpers
{
    public class NetworkHelper
    {
        private readonly IPage _page;

        public NetworkHelper(IPage page)
        {
            _page = page;
        }

        // ------------------------------------------------------------
        // 1. Listen to all network events
        // ------------------------------------------------------------

        public void StartListening()
        {
            _page.Request += (_, request) =>
            {
                Console.WriteLine($"➡️ Request: {request.Method} {request.Url}");
            };

            _page.Response += async (_, response) =>
            {
                Console.WriteLine($"⬅️ Response: {response.Status} {response.Url}");
            };
        }

        // ------------------------------------------------------------
        // 2. Wait for a specific network call
        // ------------------------------------------------------------

        public async Task<IResponse> WaitForApi(string partialUrl, int statusCode = 200)
        {
            return await _page.WaitForResponseAsync(
                resp => resp.Url.Contains(partialUrl) && resp.Status == statusCode
            );
        }

        // ------------------------------------------------------------
        // 3. Capture request & response body
        // ------------------------------------------------------------

        public async Task PrintRequestAndResponse(IResponse response)
        {
            string responseBody = await response.TextAsync();

            var request = response.Request;
            string requestBody = request.PostData;

            Console.WriteLine("\n--- REQUEST BODY ---");
            Console.WriteLine(requestBody);

            Console.WriteLine("\n--- RESPONSE BODY ---");
            Console.WriteLine(responseBody);
        }

        // ------------------------------------------------------------
        // 4. Assert API success with JSON key
        // ------------------------------------------------------------

        public async Task AssertApiJsonKey(
            string partialUrl,
            string jsonKey,
            string expectedValue)
        {
            var response = await WaitForApi(partialUrl);

            string jsonText = await response.TextAsync();
            var json = JsonDocument.Parse(jsonText);

            string actualValue = json.RootElement.GetProperty(jsonKey).ToString();

            Assert.That(actualValue, Is.EqualTo(expectedValue),
                $"Expected API JSON key '{jsonKey}' to be '{expectedValue}', but got '{actualValue}'.");
        }

        // ------------------------------------------------------------
        // 5. Assert success message in API response
        // ------------------------------------------------------------

        public async Task AssertApiContains(
            string partialUrl,
            string textToContain)
        {
            var response = await WaitForApi(partialUrl);

            string responseBody = await response.TextAsync();

            Assert.That(responseBody, Does.Contain(textToContain),
                $"API response did not contain expected text: {textToContain}");
        }

        // ------------------------------------------------------------
        // 6. Wait for API + UI click combined (best practice)
        // ------------------------------------------------------------

        public async Task<IResponse> ClickAndWaitForApi(
            ILocator button,
            string partialUrl,
            int statusCode = 200)
        {
            var waitForRequest = WaitForApi(partialUrl, statusCode);
            await button.ClickAsync();
            return await waitForRequest;
        }

        // ------------------------------------------------------------
        // 7. Log a specific API request
        // ------------------------------------------------------------

        public async Task LogApi(string partialUrl)
        {
            var response = await WaitForApi(partialUrl);

            Console.WriteLine("=== API LOG ===");
            Console.WriteLine($"URL: {response.Url}");
            Console.WriteLine($"Status: {response.Status}");

            await PrintRequestAndResponse(response);
        }
    }
}
