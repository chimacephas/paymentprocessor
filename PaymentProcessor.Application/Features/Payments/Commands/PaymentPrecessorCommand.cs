using AutoMapper;
using MediatR;
using OneOf;
using OneOf.Types;
using PaymentProcessor.Application.Common.Repository;
using PaymentProcessor.Application.Common.Repository.Interface;
using PaymentProcessor.Application.Common.Services;
using PaymentProcessor.Application.Common.Services.Interface;
using PaymentProcessor.Application.Common.Utils;
using PaymentProcessor.Application.Features.Payments.Models;
using PaymentProcessor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentProcessor.Application.Features.Payments.Commands
{
    public class PaymentPrecessorCommand : IRequest<PaymentDto>
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
    }


    public class PaymentPrecessorCommandHandler : IRequestHandler<PaymentPrecessorCommand, PaymentDto>
    {
        private readonly IEnumerable<IPaymentGateway> _paymentGateways;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentPrecessorCommandHandler(IEnumerable<IPaymentGateway> paymentGateways, 
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork,IMapper mapper)
        {
            _paymentGateways = paymentGateways;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<PaymentDto> Handle(PaymentPrecessorCommand request, CancellationToken cancellationToken)
        {


            var payment = _mapper.Map<Payment>(request);
            payment.ExpirationDate = new DateTime(int.Parse(request.ExpirationYear), int.Parse(request.ExpirationMonth), 1);
            payment.CreatedAt = DateTime.Now;
            payment.Id = Guid.NewGuid();

            payment.PaymentState = new PaymentState
            {
                Id = Guid.NewGuid(),
                State = Constants.Pending,
                CreatedAt = DateTime.Now,
                PaymentId = payment.Id,
            };


            if(request.Amount < 20)
            {
                var cheapgateway = _paymentGateways.OfType<CheapPaymentGateway>().First();

                var result = cheapgateway.Process(payment);

                if (result)
                    payment.PaymentState.State = Constants.Processed;
                else
                    payment.PaymentState.State = Constants.Failed;

                _paymentRepository.Add(payment);

            }
            else if(request.Amount >= 20 && request.Amount <= 500)
            {
                var expensivegateway = _paymentGateways.OfType<ExpensivePaymentGateway>().First();

                if (expensivegateway.IsAvailable)
                {
                    var result = expensivegateway.Process(payment);

                    if (result)
                        payment.PaymentState.State = Constants.Processed;
                    else
                        payment.PaymentState.State = Constants.Failed;

                    _paymentRepository.Add(payment);
                }
                else
                {
                    var cheapgateway = _paymentGateways.OfType<CheapPaymentGateway>().First();

                    var result = cheapgateway.Process(payment);

                    if (result)
                        payment.PaymentState.State = Constants.Processed;
                    else
                        payment.PaymentState.State = Constants.Failed;

                    _paymentRepository.Add(payment);

                }
            }
            else if(request.Amount > 500)
            {
                var premiumgateway = _paymentGateways.OfType<PremiumPaymentService>().First();

                bool result = false;

                int count = 0;

                while (!result && count != 3)
                {
                     result = premiumgateway.Process(payment);
                    if (result)
                    {
                        payment.PaymentState.State = Constants.Processed;
                        result = true;
                    }
                    else
                        payment.PaymentState.State = Constants.Failed;

                    count++;
                }
               
                _paymentRepository.Add(payment);
            }

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PaymentDto>(payment);
        }
    }
}
