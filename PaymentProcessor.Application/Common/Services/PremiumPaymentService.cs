using PaymentProcessor.Application.Common.Services.Interface;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Common.Services
{
    public class PremiumPaymentService : IPaymentGateway
    {
        public bool IsAvailable { get; set; } = true;

        public bool Process(Payment payment)
        {
            Console.WriteLine($"Processing premium payment... {payment.CardHolder} {payment.Amount}");
            return true;
        }
    }
}
