using PaymentProcessor.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Domain.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        public DateTime ExpirationDate { get; set; }

        [StringLength(3,MinimumLength =3)]
        public string SecurityCode { get; set; }

        public decimal Amount { get; set; }

        public PaymentState PaymentState { get; set; }
    }
}
