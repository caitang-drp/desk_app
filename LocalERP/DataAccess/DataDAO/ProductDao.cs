using LocalERP.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LocalERP.DataAccess.DataDAO
{
    public abstract class ProductDao
    {
        public abstract int FindNumByID(int ID);
        public abstract int UpdateNum(int id, int num);
    }
}
