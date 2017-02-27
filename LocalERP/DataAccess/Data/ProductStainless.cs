//create by stone 2017-02-07�����ڱ�ʾ����ֻ�Ʒ
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

        // ���¼�����Ʒ��ƽ���ɱ��۸�
        public void recal_product_stainless_purchase_price()
        {
            // ��ȡ���ͨ���Ķ���
            List<ProductCirculation> reviewed_all_bill = ProductStainlessCirculationDao.getInstance().get_reviewed_bill();
            // ��ȡ��Ʒ��ƽ���ɱ��۸�
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
