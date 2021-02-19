using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Common.Repository.Interface
{
    public interface IPaymentRepository : IRepository<Payment>
    {
    }
}
