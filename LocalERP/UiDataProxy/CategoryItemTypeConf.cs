using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.Data
{

    public class CategoryItemTypeConf {
        
        public UpdateType UpdateType_Category;
        public UpdateType UpdateType_Item;

        public string CategoryName;
        public string CategoryTableName;
        public string ItemName;

        public CategoryItemTypeConf(UpdateType updateType_category, UpdateType updateType_item, string categoryName, string categoryTableName, string itemName)
        {
            this.UpdateType_Category = updateType_category;
            this.UpdateType_Item = updateType_item;

            this.CategoryName = categoryName;
            this.CategoryTableName = categoryTableName;
            this.ItemName = itemName;
        }

        public static CategoryItemTypeConf CategoryItemType_ProductStainless = new CirculationTypeConf(UpdateType.ProductCategoryUpdate, UpdateType.ProductUpdate, "ProductStainless", "ProductStainlessCategory", "»õÆ·Ãû³Æ");

    }
}
