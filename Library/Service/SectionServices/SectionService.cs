using Core.Domains;
using Data.Context;
using Data.DTOs.Section;
using Microsoft.EntityFrameworkCore;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SectionServices
{
    public class SectionService : BaseServices<SectionService>, ISectionService
    {
        private readonly AppDbContext _context;

        public SectionService(AppDbContext context)
        {
            _context = context;
        }
        #region Add Section
        public async Task<ResponseResult> AddSection(SectionDTO section)
        {
            try
            {
                var model = new Section
                {
                    Name = section.Name,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Sections.Add(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The Section has been added successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Delete Section
        public async Task<ResponseResult> DeleteSection(int id)
        {
            try
            {
                var section = await GetSection(id);
                if (section == null)
                {
                    return Error();
                }

                var model = await _context.Sections.FirstOrDefaultAsync(x => x.Id == id);
                if (model == null)
                {
                    return Error();
                }

                if (section.IsDeleted)
                {
                    _context.Sections.Remove(model);
                }
                else
                {
                    model.UpdatedAt = DateTime.Now;
                    model.IsDeleted = true;
                    _context.Sections.Update(model);
                }

                await _context.SaveChangesAsync();
                return Success(Message: "The Section has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Get Section
        public async Task<SectionDTO> GetSection(int id)
        {
            try
            {
                if (id == 0)
                {
                    return null;
                }
                var section = await _context.Sections.FirstOrDefaultAsync(x => x.Id == id);
                var model = new SectionDTO
                {
                    Id = section.Id,
                    Name = section.Name,
                    CreatedAt = section.CreatedAt,
                };
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Sections
        public async Task<List<SectionDTO>> GetSections()
        {
            try
            {
                var result = await _context.Sections.Where(x => x.IsDeleted == false).Select(x => new SectionDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    IsDeleted = x.IsDeleted
                }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Update Section
        public async Task<ResponseResult> UpdateSection(SectionDTO section)
        {
            try
            {
                if (section == null)
                {
                    return Error();
                }
                var model = new Section
                {
                    Id = section.Id,
                    Name = section.Name,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = section.CreatedAt
                };
                _context.Update(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The Section has been updated successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion
    }
}
