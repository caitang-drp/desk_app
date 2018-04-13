using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;
using LocalERP.DataAccess.Utility;

namespace LocalERP.UiDataProxy
{
    public static class CategoryTableName {
        public const string ProductStainlessCategory = "ProductStainlessCategory";
        public const string CustomerCategory = "CustomerCategory";
    }

    public static class CategoryItemTypeConfs {
        public static CategoryItemTypeConf CategoryItemType_ProductStainless = new CategoryItemTypeConf(UpdateType.ProductCategoryUpdate, UpdateType.ProductUpdate, "ProductStainless", CategoryTableName.ProductStainlessCategory, "货品名称");
        public static CategoryItemTypeConf CategoryItemType_Customer = new CategoryItemTypeConf(UpdateType.CustomerCategoryUpdate, UpdateType.CustomerUpdate, "", CategoryTableName.CustomerCategory, "往来单位");
        //QueryLib还未重构，主要用于服装
        public static CategoryItemTypeConf CategoryItemType_QueryLib = new CategoryItemTypeConf(UpdateType.ProductCategoryUpdate, UpdateType.ProductUpdate, "", null, "");

    }

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
    }
}
