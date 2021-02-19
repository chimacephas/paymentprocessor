using PaymentProcessor.Application.Common.Repository.Interface;
using PaymentProcessor.Domain.Entities;
using PaymentProcessor.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Persistence.Repository.Implementations
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
