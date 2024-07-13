using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Shelf : BaseEntity
    {
        public int ShelfNumber { get; set; }
        public ICollection<Book> Books { get; set; } // Raftaki Kitaplar


        #region İlişkiler

        //=======================================================================================================Kategori
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Kategori
        //=======================================================================================================Bölüm
        [ForeignKey(nameof(SectionId))]
        public int SectionId { get; set; }
        public Section Section { get; set; } // Bölüm
        //=======================================================================================================

        #endregion
    }
}
