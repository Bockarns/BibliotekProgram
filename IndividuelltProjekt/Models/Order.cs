using IndividuelltProjekt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Book_Id { get; set; }
        public int Usert_Id { get; set; }
    }
}
