using Core.Domains;
using Data.Context;
using Data.DTOs.Category;
using Data.Validations;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CategoryServices
{
    public class CategoryService : BaseServices<CategoryService>, ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        #region Add Category
        public async Task<ResponseResult> AddCategory(CategoryDTO category)
        {
            try
            {
                var model = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    SectionId = category.SectionId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _context.Categories.AddAsync(model);
                await _context.SaveChangesAsync();

                return Success(Message: "The Category has been added successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }

        #endregion

        #region Update Category
        public async Task<ResponseResult> UpdateCategory(CategoryDTO category)
        {
            try
            {
                var model = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = category.CreatedAt,
                    SectionId = category.SectionId
                };
                _context.Update(model);
                await _context.SaveChangesAsync();

                return Success(Message: "The Category has been updated successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Get Categories
        public async Task<List<CategoryDTO>> GetCategories()
        {
            var categories = await _context.Categories.Where(x => x.IsDeleted == false).Select(x => new CategoryDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Section = _context.Sections.Where(s => s.Id == x.SectionId).Select(s => s.Name).FirstOrDefault()
            }).ToListAsync();

            return categories ?? null;


        }

        #endregion

        #region Get Category By Id
        public async Task<CategoryDTO> GetCategory(int id)
        {
            var result = await _context.Categories.Select(x => new CategoryDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                SectionId = x.SectionId,
            }).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        #endregion

        #region Delete Category
        public async Task<ResponseResult> DeleteCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return null;
                }
                var result = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (result.IsDeleted == true)
                {
                    _context.Categories.Remove(result);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    result.IsDeleted = true;
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                return Success(Message: "The Category has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion
    }
}
