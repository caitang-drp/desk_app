using System;
using System.Collections.Generic;
using System.Text;
using LocalERP.WinForm;

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

        private double totalPrice;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private double pay;

        public double Pay
        {
            get { return pay; }
            set { pay = value; }
        }
        private double payed;

        public double Payed
        {
            get { return payed; }
            set { payed = value; }
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

        public ProductCirculation(int id, string name, DateTime sellTime, string comment, int customerID) {
            this.id = id;
            this.code = name;
            this.circulationTime = sellTime;
            this.comment = comment;
            this.customerID = customerID;
        }

        public ProductCirculation() { }

        public static string[] circulationStatusContext = new string[] {"未审核","undefined","undefined", "已审核" };

        public enum CirculationType
        {
            purchase = 1, purchaseBack = 2, sell = 3, sellBack = 4, libOverflow = 5, libLoss = 6
        }

        public static CirculationTypeConf CirculationTypeConf_Purchase = new CirculationTypeConf(ProductCirculation.CirculationType.purchase , NotifyType.PurchaseUpdate, 1, "采购入库", "CGRK", "供 应 商:", "入库时间:");
        public static CirculationTypeConf CirculationTypeConf_PurchaseBack = new CirculationTypeConf(ProductCirculation.CirculationType.purchaseBack, NotifyType.PurchaseUpdate, -1, "采购退货", "CGTH", "供 应 商:", "退货时间:");
        public static CirculationTypeConf CirculationTypeConf_Sell = new CirculationTypeConf(ProductCirculation.CirculationType.sell, NotifyType.SellUpdate, - 1, "销售出库", "XSCK", "客    户:", "出库时间:");
        public static CirculationTypeConf CirculationTypeConf_SellBack = new CirculationTypeConf(ProductCirculation.CirculationType.sellBack, NotifyType.SellUpdate, 1, "销售退货", "XSTH", "客    户:", "退货时间:");
        public static CirculationTypeConf CirculationTypeConf_LibOverflow = new CirculationTypeConf(ProductCirculation.CirculationType.libOverflow, NotifyType.LibUpdate, 1, "盘点报溢", "PDBY", "供 应 商:", "报溢时间:");
        public static CirculationTypeConf CirculationTypeConf_LibLoss = new CirculationTypeConf(ProductCirculation.CirculationType.libLoss, NotifyType.LibUpdate, -1, "盘点报损", "PDBS", "供 应 商:", "报损时间:");
        
        public static CirculationTypeConf[] CirculationTypeConfs = new CirculationTypeConf[] 
            {CirculationTypeConf_Purchase, CirculationTypeConf_PurchaseBack, CirculationTypeConf_Sell, CirculationTypeConf_SellBack, CirculationTypeConf_LibOverflow, CirculationTypeConf_LibLoss };
    }

    public class CirculationTypeConf {
        public ProductCirculation.CirculationType type;
        public NotifyType notifyType;
        public int flowType;
        
        public string name;
        public string code;
        public string customer;
        public string date;

        public CirculationTypeConf(ProductCirculation.CirculationType type, NotifyType notifyType, int flowType, string name, string code, string customer, string date) {
            this.type = type;
            this.notifyType = notifyType;
            this.flowType = flowType;
            
            this.name = name;
            this.code = code;
            
            this.customer = customer;
            this.date = date;
        }
    }
}
