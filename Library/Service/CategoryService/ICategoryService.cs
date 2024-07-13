using Data.DTOs.Category;
using FluentValidation.Results;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CategoryServices
{
    public interface ICategoryService
    {
        public Task<List<CategoryDTO>> GetCategories();
        public Task<CategoryDTO> GetCategory(int id);
        public Task<ResponseResult> AddCategory(CategoryDTO category);
        public Task<ResponseResult> UpdateCategory(CategoryDTO category);
        public Task<ResponseResult> DeleteCategory(int id);
    }
}
