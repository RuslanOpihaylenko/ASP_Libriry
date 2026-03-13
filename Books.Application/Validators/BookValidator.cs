using Books.Application.DTOs.BookDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Validators
{
    public class BookValidator : AbstractValidator<BookCreateDto>
    {
        public BookValidator()
        {
            // Назва: не порожня, максимум 200 символів
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Назва книги обов'язкова")
                .MaximumLength(200).WithMessage("Назва не може бути довшою за 200 символів");

            // Автор: обов'язкове поле
            RuleFor(book => book.AuthorsId)
                .NotNull().WithMessage("Вкажіть автора книги")
                .Must(b => b.Count()>0).WithMessage("Вкажіть автора книги");

            //// Жанр: обов'язкове поле
            RuleFor(book => book.GenreId)
                .NotEmpty().WithMessage("Вкажіть жанр книги");


            //RuleFor(book => book.ImageUrl);

            // Ціна: має бути додатною
            RuleFor(book => book.Price)
                .GreaterThan(0).WithMessage("Ціна має бути більшою за 0");

            // Рік видання: не може бути в майбутньому
            RuleFor(book => book.Year)
                .InclusiveBetween(1500, DateTime.Now.Year)
                .WithMessage($"Рік видання має бути між 1500 та {DateTime.Now.Year}");
        }
    }
}
