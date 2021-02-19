using PaymentProcessor.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Entities
{
    public class PaymentState : BaseEntity
    {
        public string State { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
