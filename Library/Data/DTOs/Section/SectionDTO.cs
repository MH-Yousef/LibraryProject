using Data.DTOs.Book;
using Data.DTOs.Category;
using Data.DTOs.Shlef;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Section
{
    public class SectionDTO : BaseDTO
    {
        public string Name { get; set; }
        public ICollection<BookDTO> Books { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }
        public ICollection<ShelfDTO> Shelves { get; set; }
    }
}
