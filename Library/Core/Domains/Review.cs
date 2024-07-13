using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Review : BaseEntity
    {
        public string Content { get; set; } // İçerik

        //=======================================================================================================
        public ReviewShareType ShareType { get; set; } // Paylaşım Türü

        #region İlişkiler

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } // Kitap
        public int BookId { get; set; } // Kitap Id
        //=======================================================================================================
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } // Kullanıcı
        public int UserId { get; set; } // Kullanıcı Id

        #endregion
    }
}
