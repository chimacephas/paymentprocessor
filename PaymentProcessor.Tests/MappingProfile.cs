using AutoMapper;
using PaymentProcessor.Application.Features.Payments.Commands;
using PaymentProcessor.Application.Features.Payments.Models;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Tests
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewPaymentDto, PaymentPrecessorCommand>();
            CreateMap<PaymentPrecessorCommand, Payment>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentState, PaymentStateDto>();
        }
    }
}
