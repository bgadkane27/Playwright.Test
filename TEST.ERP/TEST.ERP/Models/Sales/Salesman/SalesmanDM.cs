using System;
using System.Collections.Generic;
using System.Text;
using TEST.ERP.Models.Common.Master;

namespace TEST.ERP.Models.Sales.Salesman
{   
    #region Salesman Header
    public class SalesmanHeaderDM : HeaderDM
    {
        public SalesmanOtherDM Other { get; set; }
    }
    #endregion

    #region Salesman Other Section
    public class SalesmanOtherDM
    {
        public string Type { get; set; }
        public string SalesCommissionInPercent { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Extension { get; set; }
        public string Mobile { get; set; }
    }
    #endregion

    #region Update Salesman
    public class UpdateSalesmanDM : SalesmanHeaderDM
    {
    }
    #endregion

    #region Salesman Master Root
    public class SalesmanDM : MasterDM<SalesmanHeaderDM>
    {
        public new UpdateSalesmanDM Update { get; set; }
    }
    #endregion
}
