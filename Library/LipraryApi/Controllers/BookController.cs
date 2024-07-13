using Data.DTOs.Book;
using Microsoft.AspNetCore.Mvc;
using Service.BaseResponses;
using Service.BookServices;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<List<BookDTO>> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return books;
        }
        [HttpGet("{id}")]
        public async Task<BookDTO> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);
            return book;
        }
        [HttpPost]
        public async Task<ResponseResult> AddBook([FromForm] BookDTO book)
        {
            var result = await _bookService.AddBook(book);
            return result;
        }

        [HttpPut]
        public async Task<ResponseResult> UpdateBook([FromForm] BookDTO book)
        {
            var result = await _bookService.UpdateBook(book);
            return result;
        }
        [HttpDelete]
        public async Task<ResponseResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            return result;
        }
    }
}
