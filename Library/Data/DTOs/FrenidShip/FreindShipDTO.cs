using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs.FrenidShip
{
    public class FreindShipDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public bool IsAccepted { get; set; } 
    }
}
