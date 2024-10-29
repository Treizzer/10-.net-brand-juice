using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators {

    public class JuiceInsertValidator : AbstractValidator<JuiceInsertDto> {

        public JuiceInsertValidator () {
            RuleFor(j => j.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(j => j.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(j => j.BrandId).NotNull().WithMessage(j => "La marca es obligatoria");
            RuleFor(j => j.BrandId).GreaterThan(0).WithMessage(j => "Error con el id de la marca");
            RuleFor(j => j.Milliliter).GreaterThan(100).WithMessage(j => "El {PropertyName} debe ser mayor a cien");
        }

    }

}
