using TEST.ERP.Helpers;
using TEST.ERP.Models.Accounting.FinancialDimension;
using TEST.ERP.Pages.Accounting;
using TEST.ERP.Tests.Common;
using TEST.ERP.Utilities;

namespace TEST.ERP.Tests.Accounting
{
    [TestFixture]
    public class FinancialDimensionTest : BaseTest
    {
        #region Create Financial Dimension
        [Test, Order(1)]
        public async Task CreateFinancialDimension()
        {
            try
            {
                #region Load Test Data
                var dataFile = DataHelper.GetDataFile("Accounting", "FinancialDimension", "FinancialDimensionData");
                var financialDimensionModel = DataHelper.ConvertJsonDataModel<FinancialDimensionRoot>(dataFile);

                var records = financialDimensionModel.FinancialDimension;
                #endregion

                #region Page Objects
                var commonAction = new CommonAction(_page);
                var accountingSetupPage = new AccountingSetupPage(_page);
                var financialDimensionPage = new FinancialDimensionPage(_page);
                #endregion

                #region Navigate To Financial Dimension
                await commonAction.ClickOnAccounting();
                await commonAction.ClickOnSetups();
                await accountingSetupPage.ClickOnFinancialDimension();
                #endregion

                #region Creation Tracking
                var createdRecords = new List<string>();
                var skippedRecords = new List<string>();
                #endregion

                #region Create Financial Dimension
                foreach (var record in records)
                {
                    try
                    {
                        await commonAction.ClickOnListNew();

                        await financialDimensionPage.SelectSegment("Segment1", record.Division);
                        await financialDimensionPage.SelectSegment("Segment2", record.Department);

                        await commonAction.ClickOnFormSave();

                        #region Validation
                        await commonAction.ValidateMessage("Financial Dimension created successfully!");
                        createdRecords.Add(record.Division + " : " + record.Department);
                        #endregion

                        #region Back To Listing
                        await financialDimensionPage.ClickOnFinancialDimension();
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        skippedRecords.Add(record.Division + " : " + record.Department);
                        Console.WriteLine($"🚫 Skipped Record: {record.Division} : {record.Department} | Reason: {ex.Message}");
                    }
                }
                #endregion

                #region Summary Report
                Console.WriteLine($"====== 🧾 Financial Dimension Creation Summary =====");
                Console.WriteLine($"📄 Total Records Attempted: {records.Count}");
                Console.WriteLine($"✅ Successfully Created: {createdRecords.Count}");
                if (createdRecords.Any())
                    Console.WriteLine("✅ Created Records Name: " + string.Join(", ", createdRecords));
                Console.WriteLine($"⚠️ Skipped/Failed: {skippedRecords.Count}");
                if (skippedRecords.Any())
                    Console.WriteLine("🚫 Skipped/Failed Records Name: " + string.Join(", ", skippedRecords));
                Console.WriteLine($"\n🕒 Test Executed At: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}");
                Console.WriteLine("===========================================");
                #endregion
            }
            catch (Exception ex) { throw new Exception("Test Failed: CreateFinancialDimension | " + ex.Message, ex); }
        }
        #endregion

        #region Delete Financial Dimension
        [Test, Order(3)]
        public async Task DeleteFinancialDimension()
        {
            try
            {
                #region Load Test Data
                var dataFile = DataHelper.GetDataFile("Accounting", "FinancialDimension", "FinancialDimensionData");
                var financialDimensionModel = DataHelper.ConvertJsonDataModel<FinancialDimensionRoot>(dataFile);

                var records = financialDimensionModel.FinancialDimension;
                #endregion

                #region Page Objects
                var commonAction = new CommonAction(_page);
                var accountingSetupPage = new AccountingSetupPage(_page);
                //var financialDimensionPage = new FinancialDimensionPage(_page);
                #endregion

                #region Navigate To Financial Dimension
                await commonAction.ClickOnAccounting();
                await commonAction.ClickOnSetups();
                await accountingSetupPage.ClickOnFinancialDimension();
                #endregion

                #region Deletion/Skipped Tracking
                var deletedRecords = new List<string>();
                var skippedRecords = new List<string>();
                #endregion

                #region Delete Selected Record
                foreach (var record in records)
                {
                    try
                    {
                        await commonAction.ProvideMasterNameOnList(record.Division);
                        await commonAction.SelectRowByTextCell(record.Division);
                        await commonAction.ClickOnMenu();
                        await commonAction.ClickOnDelete();
                        await commonAction.ClickOnOk();

                        #region Validate deleted message
                        await commonAction.ValidateMessage("Record deleted successfully!");
                        #endregion

                        #region Add Deleted Recods
                        deletedRecords.Add(record.Division + " : " + record.Department);
                        await commonAction.ClickOnListRefresh();
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        skippedRecords.Add(record.Division + " : " + record.Department);
                        Console.WriteLine($"[Warning]: Error while deleting '{record.Division} : {record.Department}': {ex.Message}");
                        await commonAction.ClickOnListRefresh();
                        //continue;
                    }
                }
                #endregion

                #region Summary Report
                Console.WriteLine("==========🧾 Financial Dimension Delete Summary ==========");
                Console.WriteLine($"📄 Total Records Attempted: {records.Count}");
                Console.WriteLine($"✅ Successfully Deleted: {deletedRecords.Count}");
                if (deletedRecords.Any())
                    Console.WriteLine("🗑️ Deleted Records: " + string.Join(", ", deletedRecords));
                Console.WriteLine($"⚠️ Skipped/Failed: {skippedRecords.Count}");
                if (skippedRecords.Any())
                    Console.WriteLine("🚫 Skipped Records: " + string.Join(", ", skippedRecords));
                Console.WriteLine($"\n🕒 Test Executed At: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}");
                Console.WriteLine("======================================");
                #endregion
            }
            catch (Exception ex) { throw new Exception("Test Failed: DeleteFinancialDimension | " + ex.Message, ex); }
        }
        #endregion
    }
}
