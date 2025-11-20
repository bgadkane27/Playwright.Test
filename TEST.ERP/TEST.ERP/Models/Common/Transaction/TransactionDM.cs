using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace TEST.ERP.Models.Common.Transaction
{
    #region Features
    public class FeatureDM
    {
        public bool EnablePriceList { get; set; }
        public bool EnableProject { get; set; }
        public bool EnableItemSize { get; set; }
        public bool EnableItemColor { get; set; }
        public bool EnableSalesman { get; set; }
        public bool RequirePaymentMethod { get; set; }
        public bool EnableFinancialSegments { get; set; }

        public bool AllowBonusQty { get; set; }
        public bool AllowModifyItemDescription { get; set; }
        public bool ShowBarcodeOnLines { get; set; }
        public bool ShowDiscountOnLines { get; set; }
        public bool ShowWarehouseOnLines { get; set; }        
        public bool AllowPartialPayment { get; set; }
        public bool UseMultiplePaymentMethod { get; set; }
        public bool EnableDentedStock { get; set; }
        public bool ShowFinancialSegmentsOnLines { get; set; }
        public bool AllowItemsUOMModifyInTxn { get; set; }
        public bool NegativeStockCheck { get; set; }
        public bool View { get; set; }
        public bool Approve { get; set; }
    }
    #endregion

    #region Header
    public class HeaderDM
    {
        public string TxnDate { get; set; }
        public string Customer { get; set; }
        public string Supplier { get; set; }
        public string BankAccount { get; set; }
        public string EnteredAmount { get; set; }
        public string PartyName { get; set; }
        public string ChequeNum { get; set; }
        public string ReferenceNum { get; set; }
        public string Currency { get; set; }
        public string ExchangeRate { get; set; }
        public string PriceList { get; set; }
        public string Warehouse { get; set; }
        public string Salesman { get; set; }
        public string PaymentTerm { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPONum { get; set; }
        public string Project { get; set; }
        public string Remarks { get; set; }
        public string MobileNum { get; set; }
        public bool ProvideDiscountInPercent { get; set; }
        public bool ProvideDiscountValue { get; set; }
        public string DiscountInPercent { get; set; }
        public string DiscountValue { get; set; }
        public string FinancialDimension { get; set; }
        public string FromWarehouse { get; set; }
        public string ToWarehouse { get; set; }
        public string Reason { get; set; }
        public string StockCountBatch { get; set; }
        public string Collector { get; set; }

        public List<LineDM> Lines { get; set; }
        public List<ChargeDM> Charges { get; set; }
        public List<PaymentMethodDM> PaymentMethods { get; set; }
        public OtherDM Others { get; set; }
        public GeneralDM General { get; set; }
    }
    #endregion

    #region Lines
    public class LineDM
    {
        public string Barcode { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Warehouse { get; set; }
        public string UOM { get; set; }
        public string Qty { get; set; }
        public string UnitPrice { get; set; }
        public bool ProvideDiscountInPercent { get; set; }
        public bool ProvideDiscountValue { get; set; }
        public string DiscountInPercent { get; set; }
        public string DiscountValue { get; set; }
        public string GrossAmount { get; set; }
        public string BonusQty { get; set; }
        public string Remarks { get; set; }
        public string QtyPerAssembledUnit { get; set; }
        public string ItemBatch { get; set; }
        public string NegativePositive { get; set; }
        public string Value { get; set; }
        public string Account { get; set; }
        public string MainAccount { get; set; }
        public string Amount { get; set; }
        public string DrCr { get; set; }
        public string EnteredAmount { get; set; }
    }
    #endregion

    #region Import
    public class ImportDM : HeaderDM
    {
        public string FilePath { get; set; }
    }
    #endregion

    #region Charges
    public class ChargeDM
    {
        public string ReferenceNum { get; set; }
        public string Charge { get; set; }
        public string AccountFC { get; set; }
        public string AmountLC { get; set; }
    }
    #endregion

    #region Payment Methods
    public class PaymentMethodDM
    {
        public string ReferenceNum { get; set; }
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
        public string CardNum { get; set; }
        public string AmountFC { get; set; }
    }
    #endregion

    #region Other
    public class OtherDM
    {
        public string ReferenceNum { get; set; }
        public string Description { get; set; }
        public string ChequeNum { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonMobile { get; set; }
        public string ContactPersonEmail { get; set; }
    }
    #endregion

    #region General
    public class GeneralDM
    {
        public string ReferenceNum { get; set; }
        public string ExchangeRate { get; set; }
        public string PaymentTerm { get; set; }        
        public string ChequeNum { get; set; }
        public string ContactPerson { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }        
        public string Remarks { get; set; }
    }
    #endregion

    #region Deletes
    public class DeleteDM
    {
        public string ReferenceNum { get; set; }
    }
    #endregion

    #region Transaction Base Root
    public class TransactionBaseDM<TransactionHeader>
    {
        public FeatureDM Features { get; set; }
        public List<TransactionHeader> Transaction { get; set; }
        public ImportDM Import { get; set; }
        public List<DeleteDM> Deletes { get; set; }
    }
    #endregion
}
