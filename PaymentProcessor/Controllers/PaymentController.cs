using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentProcessor.Application.Features.Payments.Commands;
using PaymentProcessor.Application.Features.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IMapper _mapper;

        public PaymentController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("Processpayment")]
        public async Task<IActionResult> ProcessPayment(NewPaymentDto dto)
        {
            var result = await Mediator.Send(_mapper.Map<PaymentPrecessorCommand>(dto));

            return Ok(result);
        }
    }
}
