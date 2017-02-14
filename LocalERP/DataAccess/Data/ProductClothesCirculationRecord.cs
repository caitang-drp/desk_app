using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class ProductClothesCirculationRecord : ProductCirculationRecord
    {
        private List<ProductClothesCirculationSKURecord> skuRecords;

        public List<ProductClothesCirculationSKURecord> SkuRecords
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
            foreach (ProductClothesCirculationSKURecord record in skuRecords)
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
