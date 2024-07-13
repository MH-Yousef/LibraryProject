using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Book
{
    public class BookDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ApplicationUserId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int ShelfId { get; set; }
        public int ShelfNumber { get; set; }
        public IFormFile file { get; set; }
    }
}
