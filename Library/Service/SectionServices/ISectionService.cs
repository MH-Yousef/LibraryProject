using Data.DTOs.Section;
using Service.BaseResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SectionServices
{
    public interface ISectionService
    {
        public Task<SectionDTO> GetSection(int id);
        public Task<List<SectionDTO>> GetSections();
        public Task<ResponseResult> AddSection(SectionDTO section);
        public Task<ResponseResult> UpdateSection(SectionDTO section);
        public Task<ResponseResult> DeleteSection(int id);
    }
}
