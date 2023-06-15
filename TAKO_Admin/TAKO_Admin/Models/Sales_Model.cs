using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKO_Admin.Models
{
    internal class Sales_Model
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Qr { get; set; }
    }
}
