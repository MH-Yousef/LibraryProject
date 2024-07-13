using Data.DTOs.Book;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.BaseResponses;

namespace Service.BookServices
{
    public interface IBookService
    {
        public Task<ResponseResult> AddBook(BookDTO book);
        public Task<ResponseResult> UpdateBook(BookDTO book);
        public Task<ResponseResult> DeleteBook(int id);
        public Task<BookDTO> GetBook(int id);
        public Task<List<BookDTO>> GetBooks();
    }
}
