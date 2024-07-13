using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Friendship
    {
        [Key]
        public int Id { get; set; }
        public int SenderId { get; set; } // İsteği gönderen kullanıcı Id
        public int ReceiverId { get; set; } // İsteği alan kullanıcı Id
        //=======================================================================================================
        
        public virtual ApplicationUser Sender { get; set; }
        //=======================================================================================================
        
        public virtual ApplicationUser Receiver { get; set; }
        //=======================================================================================================
        public DateTime RequestDate { get; set; } // İstek gönderme tarihi
        public DateTime? AcceptanceDate { get; set; } // İstek kabul tarihi
        public bool IsAccepted { get; set; } // İstek kabul edildi mi?
    }
}
