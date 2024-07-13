using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } // Kategori Adı
        //=======================================================================================================
        public string Description { get; set; } // Açıklama
        //=======================================================================================================
        public ICollection<Book> Books { get; set; } // Kategorinin Kitapları
        //=======================================================================================================
        public ICollection<Shelf> Shelves { get; set; } // Kategorinin Rafları
        //=======================================================================================================

        #region İlişkiler
        //=======================================================================================================
        [ForeignKey(nameof(SectionId))]
        public int SectionId { get; set; }
        public Section Section { get; set; } // Bölüm
        //=======================================================================================================
        #endregion
    }
}
