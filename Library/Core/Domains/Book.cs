using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } // Kitap Adı
        //=======================================================================================================
        public string Author { get; set; } // Yazar
        //=======================================================================================================
        public string Description { get; set; } // Açıklama
        //=======================================================================================================
        public string ImageUrl { get; set; } // Resim Url
        //=======================================================================================================
        public ICollection<Review> Reviews { get; set; } // Kitaba yapılan yorumlar


        #region İlişkiler
        //=======================================================================================================Bölüm
        [ForeignKey(nameof(SectionId))]
        public int SectionId { get; set; }
        public Section Section { get; set; } // Bölüm

        //=======================================================================================================Raf
        [ForeignKey(nameof(ShelfId))]
        public int ShelfId { get; set; }
        public Shelf Shelf { get; set; } // Raf

        //=======================================================================================================Kategori
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; } // Kategori Id
        public Category Category { get; set; } // Kategori

        #endregion
    }
}
