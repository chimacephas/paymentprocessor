using PaymentProcessor.Application.Features.Payments.Commands;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Common.Services.Interface
{
    public interface IPaymentGateway
    {
        bool Process(Payment payment);
        public bool IsAvailable { get; set; }
    }
}
