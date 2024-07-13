using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Section : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Shelf> Shelves { get; set; } // Bölüm içindeki raflar
        public ICollection<Category> Categories { get; set; } // Bölüm içindeki kategoriler
        public ICollection<Book> Books { get; set; } // Bölüm içindeki kitaplar
    }
}
