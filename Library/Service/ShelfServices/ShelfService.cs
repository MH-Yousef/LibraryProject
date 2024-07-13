using Core.Domains;
using Data.Context;
using Data.DTOs.Shlef;
using Microsoft.EntityFrameworkCore;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ShelfServices
{
    public class ShelfService : BaseServices<ShelfService>, IShelfService
    {
        private readonly AppDbContext _context;

        public ShelfService(AppDbContext context)
        {
            _context = context;
        }
        #region Add Shelf
        public async Task<ResponseResult> AddShelf(ShelfDTO shelf)
        {
            try
            {
                if (shelf == null)
                {
                    return Error();
                }
                var model = new Shelf
                {
                    ShelfNumber = shelf.ShelfNumber,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    SectionId = shelf.SectionId,
                    CategoryId = shelf.CategoryId
                };
                _context.Shelves.Add(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The Shelf has been added successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Delete Shelf
        public async Task<ResponseResult> DeleteShelf(int id)
        {
            try
            {
                var model = _context.Shelves.FirstOrDefault(x => x.Id == id);
                if (model.IsDeleted == true)
                {
                    _context.Shelves.Remove(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    model.IsDeleted = true;
                    _context.Shelves.Update(model);
                    await _context.SaveChangesAsync();
                }
                return Success(Message: "The Shelf has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Get Shelf
        public async Task<ShelfDTO> GetShelf(int id)
        {
            try
            {
                var model = await _context.Shelves.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                {
                    return null;
                }
                var shelf = new ShelfDTO
                {
                    Id = model.Id,
                    ShelfNumber = model.ShelfNumber,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    SectionId = model.SectionId,
                    CategoryId = model.CategoryId
                };
                return shelf ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Shelves
        public async Task<List<ShelfDTO>> GetShelves()
        {
            try
            {
                var result = await _context.Shelves.Where(x=> x.IsDeleted == false).Select(x => new ShelfDTO
                {
                    Id = x.Id,
                    ShelfNumber = x.ShelfNumber,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    SectionId = x.SectionId,
                    CategoryId = x.CategoryId,
                    CategoryName = _context.Categories.Where(c => c.Id == x.CategoryId).Select(c => c.Name).FirstOrDefault(),
                    SectionName = _context.Sections.Where(s => s.Id == x.SectionId).Select(s => s.Name).FirstOrDefault()
                }).ToListAsync();
                return result ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Update Shelf
        public async Task<ResponseResult> UpdateShelf(ShelfDTO shelf)
        {
            try
            {
                var model = new Shelf
                {
                    Id = shelf.Id,
                    ShelfNumber = shelf.ShelfNumber,
                    CreatedAt = shelf.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    SectionId = shelf.SectionId,
                    CategoryId = shelf.CategoryId
                };
                _context.Update(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The Shelf has been updated successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion
    }
}
