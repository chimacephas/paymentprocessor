using FluentValidation;
using PaymentProcessor.Application.Features.Payments.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Features.Payments.Models
{
    public class PaymentPrecessorCommandValidator : AbstractValidator<PaymentPrecessorCommand>
    {
        public PaymentPrecessorCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);

            RuleFor(x => x.CardHolder).NotEmpty();

            //RuleFor(x => x.ExpirationDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(x => x.ExpirationMonth).NotEmpty()
                .Must(p=> {

                    if (!int.TryParse(p, out var result))
                        return false;

                    if (result < 1 || result > 12)
                        return false;

                    return true;
                }).WithMessage("Expiry Month must not be less than 0 or greater than 12");

              RuleFor(x => x.ExpirationYear).NotEmpty()
                .Must(p => {

                    if (!int.TryParse(p, out var result))
                        return false;

                    if (result < 2021)
                        return false;

                    return true;
                }).WithMessage("Expiry Year must not be before 2021");

            RuleFor(x => x.CreditCardNumber).NotEmpty().Matches(@"^5[1-5]\d{14}$");

            When(x => !string.IsNullOrEmpty(x.SecurityCode), () => {
                RuleFor(x => x.SecurityCode).Length(3);
            });

        }
    }
}
