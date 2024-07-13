using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Review
{
    public class ReviewDTO : BaseDTO
    {
        public string Content { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int type { get; set; }
    }
}
