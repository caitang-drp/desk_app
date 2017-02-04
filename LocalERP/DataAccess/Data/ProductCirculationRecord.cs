using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductCirculationRecord
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private Product product;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        //stone: this is for reports, not reasonable
        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        private int totalNum;

        public int TotalNum
        {
            get { return totalNum; }
            set { totalNum = value; }
        }

        private int circulationID;

        public int CirculationID
        {
            get { return circulationID; }
            set { circulationID = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private List<ProductCirculationSKURecord> skuRecords;

        public List<ProductCirculationSKURecord> SkuRecords
        {
            get { return skuRecords; }
            set { skuRecords = value; }
        }

        private string numText;

        public string NumText
        {
            get { return numText; }
            set { numText = value; }
        }

        public string getTxt()
        {
            StringBuilder sb = new StringBuilder();
            int last = -1;
            string line = "";
            foreach (ProductCirculationSKURecord record in skuRecords)
            {
                if (last >= 0 && last != record.ProductSKU.Color.Id)
                {
                    sb.AppendLine(line);
                    line = "";
                }
                line += string.Format(" {0}/{1}:{2,-5}", record.ProductSKU.Color.Name, record.ProductSKU.Size.Name, record.Num);
                last = record.ProductSKU.Color.Id;
            }
            if (!string.IsNullOrEmpty(line))
                sb.Append(line);
            return sb.ToString();
        }

        public int[] getSKUIDs()
        {
            if(skuRecords == null || skuRecords.Count == 0)
                return null;

            int [] ids = new int[skuRecords.Count];
            for (int i = 0; i < skuRecords.Count; i++)
                ids[i] = skuRecords[i].ProductSKUID;
            return ids;
        }
    }

    /*
    public class ProductCirculationRecords {

        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private List<ProductCirculationRecord> records;

        public List<ProductCirculationRecord> Records
        {
            get { return records; }
            set { records = value; }
        }
        private int totalNum;

        public int TotalNum
        {
            get { return totalNum; }
            set { totalNum = value; }
        }

        public string getTxt()
        {
            StringBuilder sb = new StringBuilder();
            int last = -1;
            string line = "";
            foreach (ProductCirculationRecord record in records)
            {
                if (last >= 0 && last != record.ProductSKU.Color.Id)
                {
                    sb.AppendLine(line);
                    line = "";
                }
                line += string.Format(" {0}/{1}:{2,-5}",record.ProductSKU.Color.Name, record.ProductSKU.Size.Name, record.TotalNum);
                last = record.ProductSKU.Color.Id;
            }
            if (!string.IsNullOrEmpty(line))
                sb.Append(line);
            return sb.ToString();
        }
    }*/
}
