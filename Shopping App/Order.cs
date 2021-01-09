using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_App
{
    class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string OrderDate{ get; set; }
    }

    class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProdId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Pname { get; set; }
    }
}
