using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators {

    public class JuiceUpdateValidator: AbstractValidator<JuiceUpdateDto> {

        public JuiceUpdateValidator () {
            RuleFor(j => j.Id).NotNull().WithMessage("El Id es obligatorio");
            RuleFor(j => j.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            RuleFor(j => j.Name).Length(2, 20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            RuleFor(j => j.BrandId).NotNull().WithMessage(j => "La marca es obligatoria");
            RuleFor(j => j.BrandId).GreaterThan(0).WithMessage(j => "Error con el valor enviado en {PropertyName}");
            RuleFor(j => j.Milliliter).GreaterThan(100).WithMessage(j => "El {PropertyName} debe ser mayor a cien");
        }

    }

}
