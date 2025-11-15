using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.ERP.Models.Accounting.FinancialDimension
{
    #region Root
    public class FinancialDimensionRoot
    {
        public List<FinancialDimensionDM> FinancialDimension { get; set; }
        public List<DeleteDM> Delete { get; set; }
    }
    #endregion    

    #region Financial Dimension
    public class FinancialDimensionDM
    {
        public string Division { get; set; }
        public string Department { get; set; }
    }
    #endregion   

    #region Delete
    public class DeleteDM : FinancialDimensionDM
    {

    }
    #endregion
}
