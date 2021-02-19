using PaymentProcessor.Application.Common.ProfileMapping;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Features.Payments.Models
{
    public class PaymentDto : IMapFrom<Payment>
    {
        public Guid Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentStateDto PaymentState { get; set; }
    }
   
}
