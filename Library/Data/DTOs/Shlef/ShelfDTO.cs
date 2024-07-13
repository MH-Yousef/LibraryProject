using Data.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Shlef
{
    public class ShelfDTO : BaseDTO
    {
        public int ShelfNumber { get; set; }
        public int SectionId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SectionName { get; set; }
        public ICollection<BookDTO> Books { get; set; }

    }
}
