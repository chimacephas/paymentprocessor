using PaymentProcessor.Application.Common.ProfileMapping;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Features.Payments.Models
{
    public class PaymentStateDto : IMapFrom<PaymentState>
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
