using eCommerceApi.Application.ViewModels.Products;
using FluentValidation;

namespace eCommerceApi.Application.Repositories.Validators.Products;

public class CreateProductValidator : AbstractValidator<VM_Product_Create>
{
    public CreateProductValidator()
    {
        RuleFor(c=>c.Name)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
            .MaximumLength(150)
                .WithMessage("Ürün adı en fazla 150 karakter olabilir.")
            .MinimumLength(5)
                .WithMessage("Ürün adı en az 5 karakter olabilir.");
            
        RuleFor(s=>s.Stock)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen ürün stokunu boş geçmeyiniz.")
            .Must(s=>s>=0)
                .WithMessage("Ürün stokunu 0'dan büyük olarak giriniz.");
        
        RuleFor(p=>p.Price)
            .NotEmpty()
            .NotNull()
                .WithMessage("Lütfen ürün fiyatını boş geçmeyiniz.")
            .Must(p=>p>=0)
                .WithMessage("Ürün fiyatı 0'dan büyük olarak giriniz.");
            
    }
}