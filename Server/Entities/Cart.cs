using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Cart
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Qte {  get; set; }
    }
}
