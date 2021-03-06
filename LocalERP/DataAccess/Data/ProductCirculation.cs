using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;
using LocalERP.DataAccess.Utility;

namespace LocalERP.DataAccess.Data
{
    public class ProductCirculation
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private DateTime circulationTime;

        public DateTime CirculationTime
        {
            get { return circulationTime; }
            set { circulationTime = value; }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        
        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int customerID;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        private string customerName;

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private double total;

        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        ///
        private int totalPieces;

        public int TotalPieces
        {
            get { return totalPieces; }
            set { totalPieces = value; }
        }

        private double backFreightPerPiece;

        public double BackFreightPerPiece
        {
            get { return backFreightPerPiece; }
            set { backFreightPerPiece = value; }
        }
        /// 

        private double realTotal;

        public double RealTotal
        {
            get { return realTotal; }
            set { realTotal = value; }
        }

        private double previousArrears;

        public double PreviousArrears
        {
            get { return previousArrears; }
            set { previousArrears = value; }
        }

        private double thisPayed;

        public double ThisPayed
        {
            get { return thisPayed; }
            set { thisPayed = value; }
        }

        private double freight;

        public double Freight
        {
            get { return freight; }
            set { freight = value; }
        }

        private string oper;

        public string Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private int flowType;

        public int FlowType
        {
            get { return flowType; }
            set { flowType = value; }
        }

        private int arrearDirection;

        public int ArrearDirection
        {
            get { return arrearDirection; }
            set { arrearDirection = value; }
        }

        private string lastPayReceipt;

        public string LastPayReceipt
        {
            get { return lastPayReceipt; }
            set { lastPayReceipt = value; }
        }

        public ProductCirculation(int id, string name, DateTime sellTime, string comment, int customerID) {
            this.id = id;
            this.code = name;
            this.circulationTime = sellTime;
            this.comment = comment;
            this.customerID = customerID;
        }

        public ProductCirculation() { }

        private List<ProductCirculationRecord> records;

        public List<ProductCirculationRecord> Records
        {
            get { return records; }
            set { records = value; }
        }

        public string getFixedString(string str, int length){
            Encoding coding = Encoding.GetEncoding("gb2312");
            int dcount = 0;
            foreach (char ch in str.ToCharArray())
            {
                if (coding.GetByteCount(ch.ToString()) == 2)
                    dcount++;
            }
            string w = str.PadRight(length - dcount, ' ');
            return w;
        }

        public string getRecordsTxt()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (ProductCirculationRecord record in Records)
            {
                string name = this.getFixedString(record.ProductName, 18);
                string line = string.Format("{0}/{1,-8}/{2,-8}", name, record.Price, record.TotalNum);
                if (i < Records.Count - 1)
                    sb.AppendLine(line);
                else
                    sb.Append(line);
                i++;
            }
            return sb.ToString();
        }

        public static string[] circulationStatusContext = new string[] {"未审核","undefined","undefined", "已审核" };

        //必须跟收付一致
        public enum CirculationType
        {
            purchase = 1, purchaseBack = 2, sell = 3, sellBack = 4, libOverflow = 5, libLoss = 6, manuCost=7, manuIn = 8
        }

        public int get_circulation_type_value(CirculationType x)
        {
            return Convert.ToInt32(x);
        }

        //用于控制ProductCirculation的具体类型
        public static CirculationTypeConf CirculationTypeConf_Purchase = new CirculationTypeConf(ProductCirculation.CirculationType.purchase , UpdateType.PurchaseUpdate, UpdateType.PurchaseFinishUpdate, 1, LabelUtility.PURCHASE, "CGRK", "供应商:", "采购", 1);
        public static CirculationTypeConf CirculationTypeConf_PurchaseBack = new CirculationTypeConf(ProductCirculation.CirculationType.purchaseBack, UpdateType.PurchaseUpdate, UpdateType.PurchaseFinishUpdate, - 1, LabelUtility.PURCHASE_BACK, "CGTH", "供应商:", "退货", 1);
        public static CirculationTypeConf CirculationTypeConf_ManuCost = new CirculationTypeConf(ProductCirculation.CirculationType.manuCost, UpdateType.ManuUpdate, UpdateType.ManuFinishUpdate, -1, LabelUtility.MANU_COST, "SCXH", "供应商:", "消耗", 1);
        public static CirculationTypeConf CirculationTypeConf_ManuIn = new CirculationTypeConf(ProductCirculation.CirculationType.manuIn, UpdateType.ManuUpdate, UpdateType.ManuFinishUpdate, 1, LabelUtility.MANU_IN, "SCRK", "供应商:", "入库", 1);
        public static CirculationTypeConf CirculationTypeConf_Sell = new CirculationTypeConf(ProductCirculation.CirculationType.sell, UpdateType.SellUpdate, UpdateType.SellFinishUpdate, - 1, LabelUtility.SELL, "XSCK", "客户:", "销售", -1);
        public static CirculationTypeConf CirculationTypeConf_SellBack = new CirculationTypeConf(ProductCirculation.CirculationType.sellBack, UpdateType.SellUpdate, UpdateType.SellFinishUpdate, 1, LabelUtility.SELL_BACK, "XSTH", "客户:", "退货", -1);
        public static CirculationTypeConf CirculationTypeConf_LibOverflow = new CirculationTypeConf(ProductCirculation.CirculationType.libOverflow, UpdateType.LibUpdate, UpdateType.LibFinishUpdate, 1, LabelUtility.LIB_OVERFLOW, "PDBY", "供应商:", "报溢", 1);
        public static CirculationTypeConf CirculationTypeConf_LibLoss = new CirculationTypeConf(ProductCirculation.CirculationType.libLoss, UpdateType.LibUpdate, UpdateType.LibFinishUpdate, - 1, LabelUtility.LIB_LOSS, "PDBS", "供应商:", "报损", 1);
        
        //这里要按照type值的顺序
        public static CirculationTypeConf[] CirculationTypeConfs = new CirculationTypeConf[] { CirculationTypeConf_Purchase, CirculationTypeConf_PurchaseBack, CirculationTypeConf_Sell, CirculationTypeConf_SellBack, CirculationTypeConf_LibOverflow, CirculationTypeConf_LibLoss, CirculationTypeConf_ManuCost, CirculationTypeConf_ManuIn};
    }

    public class CirculationTypeConf {
        public ProductCirculation.CirculationType type;
        public UpdateType notifyType;
        public UpdateType finishNotifyType;
        //从我方看，表示货物的进出方向，1表示进，-1表示出
        //相应的，欠款的表示也是以我方的角度，1表示我方欠对方，-1表示对方欠我方
        public int productDirection;
        
        public string name;
        public string code;
        public string customer;
        public string business;

        public int arrearsDirection;

        public CirculationTypeConf(ProductCirculation.CirculationType type, UpdateType notifyType, UpdateType finishNotifyType, int flowType, string name, string code, string customer, string business, int arrears) {
            this.type = type;
            this.notifyType = notifyType;
            this.finishNotifyType = finishNotifyType;

            this.productDirection = flowType;
            
            this.name = name;
            this.code = code;
            
            this.customer = customer;
            this.business = business;

            this.arrearsDirection = arrears;
        }
    }
}
