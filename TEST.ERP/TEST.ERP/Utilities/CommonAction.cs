using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TEST.ERP.Tests.Common;

namespace TEST.ERP.Utilities
{
    public class CommonAction
    {
        private readonly IPage _page;

        public CommonAction(IPage page)
        {
            _page = page;
        }

        #region Locators

        #region Modules
        private const string changeModule = "//div[@title='Change module']";
        private const string accounting = "//span[normalize-space()='Accounting']";
        private const string sales = "//span[normalize-space()='Sales']";
        private const string purchase = "//span[normalize-space()='Purchase']";
        private const string inventory = "//span[normalize-space()='Inventory']";
        #endregion

        #region Left Menu
        private const string menuHome = "//a[@title='Home']";
        private const string menuCustomer = "//a[@title='Customer']";
        private const string menuSupplier = "//a[@title='Supplier']";
        private const string menuItem = "//a[@title='Item']";
        private const string menuPriceList = "//a[@title='Price List']";
        private const string menuExpand = "//div[@class='sidebar-header' and @title='Open sidebar']";
        private const string menuTransactions = "//div[contains(@class, 'nav-group-caption') and normalize-space(text())='Transactions']";
        #endregion

        #region Left Menu Tools
        private const string reports = "//a[@title='Reports']";
        private const string analytics = "//a[@title='Analytics']";
        private const string periodicProcess = "//a[@title='Periodic Process']";
        private const string setups = "//a[@title='Setups']";
        #endregion

        #region Listing Toolbar
        private const string listNew = "//li[@title='New']";
        private const string listView = "//li[@title='View']";
        private const string listEdit = "//li[@title='Edit']";
        private const string listRefresh = "//li[@title='Refresh']";
        #endregion

        #region Listing Options
        private const string dxcol3 = "//input[@aria-describedby='dx-col-3']";
        private const string dxcol4 = "//input[@aria-describedby='dx-col-4']";
        #endregion

        #region Forms/Transactions Right Menu
        private const string menu = "//img[contains(@id, 'MainMenu_DXI') and @alt='...']";
        private const string menu1 = "//img[contains(@id, 'MainMenu_DXI2') and @alt='...']";
        private const string delete = "//span[contains(@class, 'dx-vam') and text()='Delete']";
        private const string amend = "//span[contains(@class, 'dx-vam') and text()='Amend']";
        private const string ok = "//span[contains(@class, 'dx-button-text') and text()='Ok']";
        private const string yes = "//span[contains(@class, 'dx-button-text') and text()='Yes']";
        private const string cancel = "//span[contains(@class, 'dx-button-text') and text()='Cancel']";
        #endregion

        #region Transactions Top Tool Bar
        private const string save = "//span[contains(@class, 'dx-button-text') and text()='Save']";
        private const string formSave = "//span[contains(@class, 'dx-vam') and text()='Save']";
        private const string formView = "//span[contains(@class, 'dx-vam') and text()='View']";
        private const string formApprove = "//span[contains(@class, 'dx-vam') and text()='Approve']";
        private const string newInterface = "//span[contains(@class, 'dx-vam') and text()='Switch to new interface']";
        private const string oldInterface = "//span[contains(@class, 'dx-vam') and text()='Switch to old interface']";
        private const string process = "//span[contains(@class, 'dx-vam') and text()='Process']";
        private const string stockCountBatchLookup = "(//td[contains(@id, 'StockCountBatchIdLookup_B-1')])";
        private const string stockCountBatchIdLookup = "(//input[contains(@id, 'StockCountBatchIdLookup_I')])";
        private const string freezedDate = "(//input[contains(@id, 'FreezedDate_I')])";
        #endregion

        #endregion

        #region Actions

        #region General        
        public async Task ClearAndProvideValue(string selector, string value)
        {
            await _page.ClickAsync(selector);
            // CTRL + A
            await _page.PressAsync(selector, "Control+A");
            // Delete existing
            await _page.PressAsync(selector, "Delete");
            // Fill new value
            await _page.FillAsync(selector, value);
        }
        public async Task ProvideValue(string locator, string value)
        {
            // Locate the element
            var element = _page.Locator(locator);

            // Click the input
            await element.ClickAsync();

            // Select all and clear existing text
            await element.PressAsync("Control+A");
            await element.PressAsync("Delete");

            // Paste the value using Ctrl+V
            await _page.EvaluateAsync(@"value => navigator.clipboard.writeText(value)", value);
            await element.PressAsync("Control+V");
        }

        #endregion

        #region Modules
        public async Task ClickOnAccounting()
        {
            await _page.Locator(changeModule).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Accounting" }).ClickAsync();
        }
        public async Task ClickOnSales()
        {
            await _page.Locator(changeModule).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Sales" }).ClickAsync();
        }
        public async Task ClickOnPurchase()
        {
            await _page.Locator(changeModule).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Purchase" }).ClickAsync();
        }
        public async Task ClickOnInventory()
        {
            await _page.Locator(changeModule).ClickAsync();
            await _page.GetByRole(AriaRole.Link, new() { Name = "Inventory" }).ClickAsync();
        }
        #endregion

        #region Left Menu
        public async Task ClickOnMenuHome()
        {
            await _page.Locator(menuHome).ClickAsync();
        }
        public async Task ClickOnMenuCustomer()
        {
            await _page.Locator(menuCustomer).ClickAsync();
        }
        public async Task ClickOnMenuSupplier()
        {
            await _page.Locator(menuSupplier).ClickAsync();
        }
        public async Task ClickOnMenuItem()
        {
            await _page.Locator(menuItem).ClickAsync();
        }
        public async Task ClickOnMenuPriceList()
        {
            await _page.Locator(menuPriceList).ClickAsync();
        }
        public async Task ClickToExpand()
        {
            await _page.Locator(menuExpand).ClickAsync();
            await _page.WaitForTimeoutAsync(500);
        }
        public async Task ClickOnTransactions()
        {
            await _page.Locator(menuTransactions).ClickAsync();
        }
        #endregion

        #region Left Menu Tools
        public async Task ClickOnReports()
        {
            await _page.Locator(reports).ClickAsync();
        }
        public async Task ClickOnAnalytics()
        {
            await _page.Locator(analytics).ClickAsync();
        }
        public async Task ClickOnPeriodicProcess()
        {
            await _page.Locator(periodicProcess).ClickAsync();
        }
        public async Task ClickOnSetups()
        {
            await _page.Locator(setups).ClickAsync();
        }
        #endregion

        #region Listing Toolbar

        public async Task ClickOnListNew()
        {
            await _page.Locator(listNew).ClickAsync();
        }
        public async Task ClickOnListView()
        {
            await _page.Locator(listView).ClickAsync();
        }
        public async Task ClickOnListEdit()
        {
            await _page.Locator(listEdit).ClickAsync();
        }
        public async Task ClickOnListRefresh()
        {
            await _page.Locator(listRefresh).ClickAsync();
        }

        #endregion

        #region Listing Options
        public async Task ProvideMasterNameOnList(string value)
        {
            //await ClearAndProvideValue(dxcol3, value);
            await _page.Locator(dxcol3).FillAsync(value);
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ProvideMasterNameOnListCol4(string value)
        {
            //await ClearAndProvideValue(dxcol4, value);
            await _page.Locator(dxcol4).FillAsync(value);
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task SelectRowByName(string entityName)
        {
            string xpath = $"//tbody//tr[.//a[normalize-space(text())='{entityName}']]";

            var row = _page.Locator(xpath);

            if (await row.CountAsync() == 0)
                throw new Exception($"No row found with the name '{entityName}'.");

            await row.First.ScrollIntoViewIfNeededAsync();
            await row.First.ClickAsync();
        }
        public async Task SelectRowByTextCell(string text)
        {
            string xpath = $"//tbody//tr//td[normalize-space(text())='{text}']";

            var row = _page.Locator(xpath);

            if (await row.CountAsync() == 0)
                throw new Exception($"No row found with value '{text}'.");

            await row.First.ScrollIntoViewIfNeededAsync();
            await row.First.ClickAsync();
        }
        public async Task SelectRowByReference(string referenceNum)
        {
            string xpath = $"//td[@aria-describedby='dx-col-5' and normalize-space(text())='{referenceNum}']";

            var row = _page.Locator(xpath);

            if (await row.CountAsync() == 0)
                throw new Exception($"No row found with reference number '{referenceNum}'.");

            await row.First.ClickAsync();
        }
        #endregion

        #region Forms/Transactions Right Menu 
        public async Task ClickOnMenu()
        {
            await _page.Locator(menu).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnToMenu()
        {
            await _page.Locator(menu1).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnDelete()
        {
            await _page.Locator(delete).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnAmend()
        {
            await _page.Locator(amend).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnOk()
        {
            await _page.Locator(ok).ClickAsync();
            //await _page.WaitForTimeoutAsync(1500);
        }
        public async Task ClickOnYes()
        {
            await _page.Locator(yes).ClickAsync();
            //await _page.WaitForTimeoutAsync(1500);
        }
        public async Task ClickOnCancel()
        {
            await _page.Locator(cancel).ClickAsync();
            //await _page.WaitForTimeoutAsync(2000);
        }
        #endregion

        #region Transactions Top Tool Bar
        public async Task ClickOnSave()
        {
            await _page.Locator(save).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnFormSave()
        {
            await _page.Locator(formSave).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnFormView()
        {
            await _page.Locator(formView).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnFormApprove()
        {
            await _page.Locator(formApprove).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnSwitchToNewInterface()
        {
            await _page.Locator(newInterface).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnSwitchToOldInterface()
        {
            await _page.Locator(oldInterface).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task ClickOnProcess()
        {
            await _page.Locator(process).ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task SelectStockCountBatch(string item)
        {
            await _page.Locator(stockCountBatchLookup).ClickAsync();
            await _page.WaitForTimeoutAsync(500);
            await ClearAndProvideValue(stockCountBatchIdLookup, item);
            await _page.WaitForTimeoutAsync(1500);
            await SelectLookUpText(item);
        }
        public async Task ProvideFreezedDate(string date)
        {
            await _page.Locator(freezedDate).FillAsync(date);
        }
        #endregion

        #region Drop-down/LookUps 
        public async Task SelectLookUpOption(string option)
        {
            var optionLocator = _page.Locator($"//div[normalize-space(text())='{option}']");

            // Wait for options to appear
            await optionLocator.First.WaitForAsync();

            // Click the FIRST matching option
            await optionLocator.First.ClickAsync();
        }
        public async Task SelectLookUpListOption(string option)
        {
            var optionLocator = _page.Locator(".dx-item-content, .dx-list-item-content");
            await optionLocator.First.WaitForAsync();
            await optionLocator
                .Filter(new LocatorFilterOptions { HasTextString = option })
                .First
                .ClickAsync();
        }
        public async Task SelectDropDownOption(string option)
        {
            var dropdownList = _page.Locator("//tr[@class='dxgvDataRow_Office365']");
            int count = await dropdownList.CountAsync();
            await _page.WaitForTimeoutAsync(500);

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
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task SelectDropDownListOption(string option)
        {
            var dropdownList = _page.Locator("//div[@class='dx-item dx-list-item']");
            int count = await dropdownList.CountAsync();

            await _page.WaitForTimeoutAsync(500);

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
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task SelectDropDownData(string option)
        {
            var dropdownList = _page.Locator("//tr[@class='dxeListBoxItemRow_Office365']");
            int count = await dropdownList.CountAsync();

            await _page.WaitForTimeoutAsync(500);

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
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task SelectLookUpText(string option)
        {
            while (true)
            {
                var dropdownList = _page.Locator("//div[@class='lookup-text']");
                int count = await dropdownList.CountAsync();

                await _page.WaitForTimeoutAsync(500);

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
                await _page.WaitForTimeoutAsync(2000);
                await _page.ClickAsync("//img[@alt='Next']");
                await _page.WaitForTimeoutAsync(2000);
            }
        }
        public async Task SelectLookUpData(string option)
        {
            while (true)
            {
                var dropdownList = _page.Locator("//div[@class='lookup-text']");
                int count = await dropdownList.CountAsync();

                await _page.WaitForTimeoutAsync(500);

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
                await _page.WaitForTimeoutAsync(2000);
                await _page.ClickAsync("//div[contains(@id, '_NextPage')]");
                await _page.WaitForTimeoutAsync(2000);
            }
        }
        #endregion

        #region Documents         
        public async Task ClickOnDocumentType()
        {
            await _page.Locator("(//input[contains(@id, 'DocumentTypeId')])").ClickAsync();
        }
        public async Task SelectDocumentType(string data)
        {
            await _page.Locator("(//input[contains(@id, 'DocumentTypeId')])").ClickAsync();
            await _page.WaitForTimeoutAsync(500);
            await SelectLookUpListOption(data);
        }
        public async Task ProvideDocumentNumber(string documentNumber)
        {
            await _page.Locator("(//input[contains(@id, 'DocumentNumber')])").FillAsync(documentNumber);
        }
        public async Task ProvideDateOfIssue(string dateOfIssue)
        {
            await _page.Locator("(//input[contains(@id, 'DateOfIssue')])").FillAsync(dateOfIssue);
        }
        public async Task ProvideDateOfExpiry(string dateOfExpiry)
        {
            await _page.Locator("(//input[contains(@id, 'DateOfExpiry')])").FillAsync(dateOfExpiry);
            await _page.WaitForTimeoutAsync(1000);
        }
        public async Task ProvidePlaceOfIssue(string placeOfIssue)
        {
            await _page.Locator("(//input[contains(@id, 'PlaceOfIssue')])").FillAsync(placeOfIssue);
        }
        public async Task ClickOnAddAttachment()
        {
            await _page.Locator("//span[contains(@class, 'dx-button-text') and text()='Add Attachment…']").ClickAsync();
            await _page.WaitForTimeoutAsync(1500);
        }
        public async Task ClickOnSaveDocument()
        {
            await _page.Locator("(//span[normalize-space()='Save'])[2]").ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        #endregion

        #region Masters Header
        public async Task ClickOnMasterMenu()
        {
            await _page.Locator("//div[contains(@title,'Menu')]").ClickAsync();
            await _page.WaitForTimeoutAsync(1000);
        }
        public async Task ClickOnMasterDelete()
        {
            await _page.Locator("//div[contains(@title,'Delete')]").ClickAsync();
            await _page.WaitForTimeoutAsync(1000);
        }
        public async Task ProvideCode(string code)
        {
            await ClearAndProvideValue("//input[contains(@id, 'Code')]", code);
        }
        public async Task ProvideName(string name)
        {
            await ClearAndProvideValue("//input[contains(@id, 'Name')]", name);
            await _page.WaitForTimeoutAsync(1000);
        }
        public async Task ProvideNameArabic(string arabicName)
        {
            await ClearAndProvideValue("//input[contains(@id, 'NameL2')]", arabicName);
            await _page.WaitForTimeoutAsync(1000);
        }
        public async Task ProvideDescription(string description)
        {
            await _page.Locator("//textarea[contains(@id, 'Description')]").FillAsync(description);
        }
        #endregion

        #region Payment Methods
        public async Task ClickOnPaymentMethods()
        {
            await _page.Locator("//td[contains(@id, 'PaymentMethods_HC')]").ClickAsync();
            await _page.WaitForTimeoutAsync(2000);
        }
        public async Task SelectPaymentMethods(string paymentmethod)
        {
            await _page.Locator("(//td[contains(@id, '_PaymentMethodId_B-1')])").ClickAsync();
            await _page.WaitForTimeoutAsync(500);
            await ClearAndProvideValue("//input[contains(@id, '_PaymentMethodId_I')]", paymentmethod);
            await _page.WaitForTimeoutAsync(1500);
            await SelectLookUpText(paymentmethod);
        }
        public async Task SelectPaymentMethodCurrency(string currency)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[2]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_CurrencyId_B-1')])").ClickAsync();
            await _page.WaitForTimeoutAsync(500);
            await ClearAndProvideValue("//input[contains(@id, '_CurrencyId_I')]", currency);
            await _page.WaitForTimeoutAsync(1500);
            await SelectLookUpText(currency);
        }
        public async Task ProvidePaymentMethodAmountFC(string amountFC)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[3]", amountFC);
            await _page.WaitForTimeoutAsync(500);
        }
        #endregion

        #region Charges
        public async Task ClickOnCharges()
        {
            await _page.Locator("//td[contains(@id, 'Charge_HC')]").ClickAsync();
            Thread.Sleep(2000);
        }
        public async Task SelectCharge(string charge)
        {
            await _page.Locator("(//td[contains(@id, '_ChargeId_B-1')])").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(charge);
        }
        public async Task SelectAccount(string account)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[2]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_AccountId_B-1')])").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(account);
        }
        public async Task ProvideAmountFC(string amountFC)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[4]", amountFC);
            Thread.Sleep(500);
        }
        #endregion

        #region Transactions Lines
        public async Task ClickOnLineDelete()
        {
            await _page.Locator("//div[@class='dx-button-content' and .//span[text()='Delete Line']]").ClickAsync();
            Thread.Sleep(2000);
        }
        public async Task ClickOnLineNew()
        {
            await _page.Locator("//div[@class='dx-button-content' and .//span[text()='New Line']]").ClickAsync();
            Thread.Sleep(2000);
        }
        public async Task ClickOnChargeNew()
        {
            await _page.Locator("(//div[@class='dx-button-content' and .//span[text()='New Line']])[2]").ClickAsync();
            Thread.Sleep(2000);
        }
        public async Task ClickOnPaymentMethodNew()
        {
            await _page.Locator("(//div[@class='dx-button-content' and .//span[text()='New Line']])[2]").ClickAsync();
            Thread.Sleep(2000);
        }
        public async Task ClickOnLineMenu()
        {
            await _page.Locator("//div[@class='dx-button-content']/i[contains(@class, 'dx-icon-overflow')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task ClickOnLineImport()
        {
            await _page.Locator("//span[@class='dx-button-text' and text()='Import']").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task ClickOnBarcode()
        {
            await _page.Locator("(//td[contains(@id, '_ItemBarcodeId_B-1')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task SelectBarcode(string barcode)
        {
            await _page.Locator("(//td[contains(@id, '_ItemBarcodeId_B-1')])").ClickAsync();
            Thread.Sleep(700);
            await SelectLookUpText(barcode);
        }
        public async Task ClickOnItem()
        {
            await _page.Locator("(//td[contains(@id, '_ItemId_B-1')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task SelectItem(string item)
        {
            await _page.Locator("(//td[contains(@id, '_ItemId_B-1')])").ClickAsync();
            Thread.Sleep(700);
            await ClearAndProvideValue("(//input[contains(@id, '_ItemId_I')])", item);
            Thread.Sleep(1500);
            await SelectLookUpText(item);
        }
        public async Task ClickOnSize()
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[4]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_ItemSizeId_B-1')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task SelectSize(string size)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[4]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_ItemSizeId_B-1')])").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(size);
        }
        public async Task ClickOnColor()
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[5]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_ItemColorId_B-1')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task SelectColor(string color)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[5]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_ItemColorId_B-1')])").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(color);
        }
        public async Task ClickOnLineWarehouse()
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[6]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_WarehouseId_B-1')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task SelectLineWarehouse(string warehouse)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[6]").ClickAsync();
            await _page.Locator("(//td[contains(@id, '_WarehouseId_B-1')])").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(warehouse);
        }
        public async Task ClickOnUOM()
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[7]").ClickAsync();
            await _page.Locator("(//tr//td[contains(@id, '_UnitOfMeasureId_B-1')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task SelectUOM(string uom)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[7]").ClickAsync();
            await _page.Locator("(//tr//td[contains(@id, '_UnitOfMeasureId_B-1')])").ClickAsync();
            Thread.Sleep(1000);
            await SelectLookUpText(uom);
        }
        public async Task SelectUOMInPurchase(string uom)
        {
            await _page.Locator("(//div[@class='dxgBCTC dx-ellipsis'])[6]").ClickAsync();
            await _page.Locator("(//tr//td[contains(@id, '_UnitOfMeasureId_B-1')])").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(uom);
        }
        public async Task ClickOnSeprateLineMenu()
        {
            await _page.Locator("(//img[contains(@id, '_DXCBtn-1Img')])").ClickAsync();
            Thread.Sleep(500);
        }
        public async Task ProvideQty(string qty)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[8]", qty);
            Thread.Sleep(500);
        }
        public async Task ProvideQtyInPurchase(string qty)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[7]", qty);
            Thread.Sleep(500);
        }
        public async Task ProvideUnitPrice(string unitPrice)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[9]", unitPrice);
            Thread.Sleep(500);
        }
        public async Task ProvideUnitPriceInPurchase(string unitPrice)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[8]", unitPrice);
            Thread.Sleep(500);
        }
        public async Task ProvideLineDiscountInPercent(string discountInpercent)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[10]", discountInpercent);
        }
        public async Task ProvideLineDiscountInPercentInPurchase(string discountInpercent)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[9]", discountInpercent);
        }
        public async Task ProvideLineDiscountValue(string discountInValue)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[11]", discountInValue);
        }
        public async Task ProvideLineDiscountValueInPurchase(string discountInValue)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[10]", discountInValue);
        }
        public async Task ProvideDiscountInPercent(string discountInpercent)
        {
            await ClearAndProvideValue("(//input[contains(@id, '.DiscountInPercent_I')])", discountInpercent);
        }
        public async Task ProvideDiscountValue(string discountInValue)
        {
            await ClearAndProvideValue("(//input[contains(@id, '.DiscountValue_I')])", discountInValue);
        }
        public async Task ProvideBonusQtyInPurchase(string bonusqty)
        {
            await ProvideValue("(//div[@class='dxgBCTC dx-ellipsis'])[12]", bonusqty);
        }
        #endregion

        #region Transactions Header
        public async Task ClickOnCustomer()
        {
            await _page.Locator("//td[contains(@id, '.CustomerIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectCustomer(string customer)
        {
            await _page.Locator("//td[contains(@id, '.CustomerIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await ClearAndProvideValue("//input[contains(@id, '.CustomerIdLookup_I')]", customer);
            Thread.Sleep(1500);
            await SelectLookUpText(customer);
        }
        public async Task ClickOnSupplier()
        {
            await _page.Locator("//td[contains(@id, '.SupplierIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectSupplier(string supplier)
        {
            await _page.Locator("//td[contains(@id, '.SupplierIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(supplier);
        }
        public async Task ClickOnCurrency()
        {
            await _page.Locator("//td[contains(@id, '.CurrencyIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectCurrency(string currency)
        {
            await _page.Locator("//td[contains(@id, '.CurrencyIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(currency);
        }
        public async Task ClickOnBankAccount()
        {
            await _page.Locator("//td[contains(@id, '.BankAccountIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectBankAccount(string bankAccount)
        {
            await _page.Locator("//td[contains(@id, '.BankAccountIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(bankAccount);
        }
        public async Task ClickOnPriceList()
        {
            await _page.Locator("//td[contains(@id, '.PriceListIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectPriceList(string priceList)
        {
            await _page.Locator("//td[contains(@id, '.PriceListIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(priceList);
        }
        public async Task ClickOnWarehouse()
        {
            await _page.Locator("//td[contains(@id, '.WarehouseIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectWarehouse(string warehouse)
        {
            await _page.Locator("//td[contains(@id, '.WarehouseIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await ClearAndProvideValue("//input[contains(@id, '.WarehouseIdLookup_I')]", warehouse);
            Thread.Sleep(1500);
            await SelectLookUpText(warehouse);
        }
        public async Task ClickOnSalesman()
        {
            await _page.Locator("//td[contains(@id, '.SalesmanIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectSalesman(string salesman)
        {
            await _page.Locator("//td[contains(@id, '.SalesmanIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(salesman);
        }
        public async Task ClickOnPaymentTerm()
        {
            await _page.Locator("//td[contains(@id, '.PaymentTermIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectPaymentTerm(string paymentTerm)
        {
            await _page.Locator("//td[contains(@id, '.PaymentTermIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(paymentTerm);
        }
        public async Task ClickOnPaymentMethod()
        {
            await _page.Locator("//td[contains(@id, '.PaymentMethodIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectPaymentMethod(string paymentMethod)
        {
            await _page.Locator("//td[contains(@id, '.PaymentMethodIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(paymentMethod);
        }
        public async Task ClickOnProject()
        {
            await _page.Locator("//td[contains(@id, '.ProjectIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(1000);
        }
        public async Task SelectProject(string project)
        {
            await _page.Locator("//td[contains(@id, '.ProjectIdLookup_B-1')]").ClickAsync();
            Thread.Sleep(500);
            await SelectLookUpText(project);
        }
        public async Task ProvideReferenceNum(string referenceNum)
        {
            await ClearAndProvideValue("//input[contains(@id, '.ReferenceNum_I')]", referenceNum);
            Thread.Sleep(500);
        }
        public async Task ProvideEnteredAmount(string enteredAmount)
        {
            await ClearAndProvideValue("//input[contains(@id, '.EnteredAmount_I')]", enteredAmount);
        }
        public async Task ProvideExchangeRate(string exchangeRate)
        {
            await ClearAndProvideValue("//input[contains(@id, '.ExchangeRate_I')]", exchangeRate);
        }
        public async Task ProvidePartyName(string partyName)
        {
            await ClearAndProvideValue("//input[contains(@id, '.PartyName_I')]", partyName);
        }
        public async Task ProvideChequeNum(string chequeNum)
        {
            await ClearAndProvideValue("//input[contains(@id, '.ChequeNum_I')]", chequeNum);
        }
        public async Task ProvideMobileNumber(string mobileNumber)
        {
            await ClearAndProvideValue("//input[contains(@id, '.MobileNum_I')]", mobileNumber);
        }
        public async Task ProvideRemarks(string remarks)
        {
            await ClearAndProvideValue("//textarea[contains(@id, '.Description_I')]", remarks);
        }
        #endregion

        #region Validation
        public async Task ValidateSucess(string expectedMessage)
        {
            var element = _page.Locator(".dx-toast-success").First;
            string actualMessage = await element.InnerTextAsync();

            Assert.That(actualMessage, Does.Contain(expectedMessage));
        }
        public async Task ValidateMessage(string expectedMessage)
        {
            var element = _page.Locator(".dx-toast-message").First;
            string actualMessage = await element.InnerTextAsync();

            Assert.That(actualMessage, Does.Contain(expectedMessage));
        }
        public async Task ValidateSummary(string expectedMessage)
        {
            var element = _page.Locator("#ValidationSummary");
            string actualMessage = await element.InnerTextAsync();

            Assert.That(actualMessage, Does.Contain(expectedMessage));
        }
        #endregion        

        #endregion
    }
}
