using System;
using System.Collections.Generic;
using System.Text;

namespace LocalERP.DataAccess.Data
{
    public class CharactorValue
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int charactorId;

        public int CharactorId
        {
            get { return charactorId; }
            set { charactorId = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
