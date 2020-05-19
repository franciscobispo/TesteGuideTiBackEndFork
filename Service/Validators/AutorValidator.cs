using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Validators
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        public AutorValidator()
        {
            RuleFor(c => c)
                    .NotNull()
                    .OnAnyFailure(x =>
                    {
                        throw new ArgumentNullException("Objeto não localizado.");
                    });

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar o Nome.")
                .NotNull().WithMessage("É necessário informar o Nome.")
                .MinimumLength(3);            
        }
    }
}
