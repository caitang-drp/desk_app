//create by stone 2017-02-07：用于表示不锈钢货品
using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.DataAccess.DataDAO;

namespace LocalERP.DataAccess.Data
{
    public class ProductStainless : Product
    {
        private int quantityPerPiece;

        public int QuantityPerPiece
        {
            get { return quantityPerPiece; }
            set { quantityPerPiece = value; }
        }

        public ProductStainless() { }

        public ProductStainless(string serial, string name, int categoryID, double pricePurchase, double priceSell, string unit, int quantityPerPiece, string comment)
            : base(serial, name, categoryID, pricePurchase, priceSell, unit, comment)
        {
            this.quantityPerPiece = quantityPerPiece;
        }

        // 重新计算商品的平均成本价格
        public void recal_product_stainless_purchase_price()
        {
            // 获取审核通过的订单
            List<ProductCirculation> reviewed_all_bill = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            // 获取商品的平均成本价格
            Dictionary<int, double> product_average_price_map =
                ProductStainlessCirculationRecordDao.getInstance().get_product_moving_weighted_average_method_cost(reviewed_all_bill);

            foreach (KeyValuePair<int, double> pair in product_average_price_map)
            {
                int product_id = pair.Key;
                double purchase_price = pair.Value;

                ProductStainlessDao.getInstance().update_purchase_price_by_id(product_id, purchase_price);
            }
        }
    }
}
