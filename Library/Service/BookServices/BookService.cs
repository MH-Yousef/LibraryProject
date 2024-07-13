using Core.Domains;
using Data.Context;
using Data.DTOs.Book;
using Data.Validations;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Service.BaseResponses;
using Service.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BookServices
{
    public class BookService : BaseServices<BookService>, IBookService
    {
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;
        public BookService(AppDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }
        #region Add Book
        public async Task<ResponseResult> AddBook(BookDTO book)
        {
            try
            {
                if (book.file != null)
                {
                    book.Image = await _imageService.UploadImage(book.file, "Book");
                }
                var model = new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    ImageUrl = book.Image,
                    CategoryId = book.CategoryId,
                    CreatedAt = DateTime.Now,
                    ShelfId = book.ShelfId,
                    SectionId = book.SectionId
                };
                await _context.Books.AddAsync(model);
                await _context.SaveChangesAsync();
                return Success(Message: "The Book has been added successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }

        #endregion

        #region Delete Book
        public async Task<ResponseResult> DeleteBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new ResponseResult { Errors = ["Id is required"], IsSuccess = false };
                }
                var book = await GetBook(id);
                var model = new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    ImageUrl = book.Image,
                    CategoryId = book.CategoryId,
                    CreatedAt = book.CreatedAt,
                    UpdatedAt = book.UpdatedAt,
                    SectionId = book.SectionId,
                    ShelfId = book.ShelfId
                };
                if (book.IsDeleted == true)
                {
                    _context.Books.Remove(model);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    model.IsDeleted = true;
                    _context.Books.Update(model);
                    await _context.SaveChangesAsync();
                }
                return Success(Message: "The Book has been deleted successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion

        #region Get Book
        public async Task<BookDTO> GetBook(int id)
        {
            try
            {
                var result = await _context.Books.Where(x => x.Id == id).Select(x => new BookDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description,
                    Image = x.ImageUrl,
                    CategoryId = x.CategoryId,
                    SectionId = x.SectionId,
                    ShelfId = x.ShelfId,
                    CategoryName = _context.Categories.Where(c => c.Id == x.CategoryId).Select(c => c.Name).FirstOrDefault(),
                    SectionName = _context.Sections.Where(s => s.Id == x.SectionId).Select(s => s.Name).FirstOrDefault(),
                    ShelfNumber = _context.Shelves.Where(s => s.Id == x.ShelfId).Select(s => s.ShelfNumber).FirstOrDefault(),
                }).FirstOrDefaultAsync();

                return result ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Get Books
        public async Task<List<BookDTO>> GetBooks()
        {

            try
            {
                var books = await _context.Books.Where(x => x.IsDeleted == false).Select(x => new BookDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description,
                    Image = x.ImageUrl,
                    CategoryId = x.CategoryId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    SectionId = x.SectionId,
                    ShelfId = x.ShelfId,
                    CategoryName = _context.Categories.Where(c => c.Id == x.CategoryId).Select(c => c.Name).FirstOrDefault(),
                    SectionName = _context.Sections.Where(s => s.Id == x.SectionId).Select(s => s.Name).FirstOrDefault(),
                    ShelfNumber = _context.Shelves.Where(s => s.Id == x.ShelfId).Select(s => s.ShelfNumber).FirstOrDefault(),
                }).ToListAsync();
                return books ?? null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Update Book
        public async Task<ResponseResult> UpdateBook(BookDTO book)
        {
            try
            {
                var OldBookImage = await _context.Books.Where(x => x.Id == book.Id).Select(x => x.ImageUrl).FirstOrDefaultAsync(); // eski görsel


                if (book.Image != OldBookImage)
                {
                    book.Image = _imageService.UploadImage(book.file, "Book").Result;
                }
                else
                {
                    book.Image = OldBookImage;
                }
                var model = new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    ImageUrl = book.Image,
                    CategoryId = book.CategoryId,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = book.CreatedAt,
                    SectionId = book.SectionId,
                    ShelfId = book.ShelfId
                };
                _context.Update(model);
                await _context.SaveChangesAsync();

                return Success(Message : "The Book has been updated successfully");
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
        #endregion
    }
}

