using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_App
{
    class Inventory
    {
        public int ProdId { get; set; }
        public string Pname { get; set; }
        public int InStock { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int SoldQty { get; set; }
    }
}
