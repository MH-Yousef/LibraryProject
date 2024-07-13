using Data.DTOs.Shlef;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ShelfServices
{
    public interface IShelfService
    {
        public Task<ShelfDTO> GetShelf(int id);
        public Task<List<ShelfDTO>> GetShelves();
        public Task<ResponseResult> AddShelf(ShelfDTO shelf);
        public Task<ResponseResult> UpdateShelf(ShelfDTO shelf);
        public Task<ResponseResult> DeleteShelf(int id);
    }
}
