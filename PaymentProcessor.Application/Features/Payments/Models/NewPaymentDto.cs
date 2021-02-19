using AutoMapper;
using PaymentProcessor.Application.Common.ProfileMapping;
using PaymentProcessor.Application.Features.Payments.Commands;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Features.Payments.Models
{
    public class NewPaymentDto : IMapFrom<Payment>
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        //public DateTime ExpirationDate { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPaymentDto, PaymentPrecessorCommand>();
            profile.CreateMap<PaymentPrecessorCommand, Payment>();
        }
    }
}
