using PaymentProcessor.Application.Common.Services.Interface;
using PaymentProcessor.Application.Common.Utils;
using PaymentProcessor.Application.Features.Payments.Commands;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Common.Services
{

    public class CheapPaymentGateway : IPaymentGateway
    {
        public bool IsAvailable { get; set; } = true;

        public bool Process(Payment payment)
        {
            Console.WriteLine($"Processing cheap payment... {payment.CardHolder} {payment.Amount}");
            return true;
        }
    }

 
}
