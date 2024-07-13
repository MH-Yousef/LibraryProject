using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; } // Ad Soyad
        //=======================================================================================================
        public string Image { get; set; } // Profil Fotoğrafı
        //=======================================================================================================
        public virtual ICollection<Friendship> SentFriendships { get; set; } // Kullanıcının Gönderdiği Arkadaşlık İstekleri
        //=======================================================================================================
        public virtual ICollection<Friendship> ReceivedFriendships { get; set; } // Kullanıcının Aldığı Arkadaşlık İstekleri
        //=======================================================================================================


    }
    public class AppRole : IdentityRole<int>
    {
        public AppRole() : base() { }
        public AppRole(string roleName) : base(roleName) { }
    }
}
