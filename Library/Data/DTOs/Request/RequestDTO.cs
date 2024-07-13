using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.Request
{
    public  class RequestDTO
    {
        public int SenderId { get; set; }
        public int TargetUserId { get; set; }
        public string fullName { get; set; }
        public string Image { get; set; }
    }
}
