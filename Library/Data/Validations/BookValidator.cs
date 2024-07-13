using Data.DTOs.Book;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Validations
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator(bool IsUpdate = false)
        {
            if (IsUpdate) {
                RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
                RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
            }
            else
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
                RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
                RuleFor(x => x.file).NotEmpty().WithMessage("Image is required");
                RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required");
            }
        }
    }
}
