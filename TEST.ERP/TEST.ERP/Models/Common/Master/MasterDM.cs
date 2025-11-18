using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.ERP.Models.Common.Master
{
    #region Entity
    public class EntityDM
    {
        public string Name { get; set; }
    }
    #endregion

    #region Features
    public class FeaturesDM
    {
        #region General
        public bool AllowCodeManual { get; set; }
        #endregion

        #region Company
        public bool Taxation { get; set; }
        #endregion

        #region Warehouse
        public bool SkipNegativeStockCheck { get; set; }
        #endregion

        // Only one value should be set to true at a time
        #region Price List
        public bool AllItemsWithBaseUOM { get; set; }
        public bool SelectedAllGroup { get; set; }
        public bool SelectedGroup { get; set; }
        public bool SelectedAllCategory { get; set; }
        public bool SelectedCategory { get; set; }
        public bool SelectedAllBrand { get; set; }
        public bool SelectedBrand { get; set; }
        #endregion
    }
    #endregion

    #region Master Header
    public class HeaderDM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
    }
    #endregion

    #region Validate Master
    public class ValidateDM
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    #endregion

    #region Update Master
    public class UpdateDM : HeaderDM
    {
    }
    #endregion

    #region Delete Master
    public class DeleteDM
    {
        public string Name { get; set; }
    }
    #endregion

    #region Master Base
    public class MasterDM<MasterHeader>
    {
        public EntityDM Entity { get; set; }
        public FeaturesDM Features { get; set; }
        public List<MasterHeader> Master { get; set; }
        public ValidateDM Validate { get; set; }
        public UpdateDM Update { get; set; }
        public List<DeleteDM> Delete { get; set; }
    }
    #endregion
}
