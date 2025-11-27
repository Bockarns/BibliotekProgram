using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt.Models
{
    public class Book
    {
        public int ISBN { get; set; }
        public string Auther { get; set; }
        public string Title { get; set; }
        public bool Avaliable { get; set; }
        public Book()
        {
            if (Auther != null)
                Auther = "";
            if (Title != null)
                Title = "";
        }
    }
}
