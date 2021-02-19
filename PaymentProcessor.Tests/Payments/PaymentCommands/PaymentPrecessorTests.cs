using AutoMapper;
using FluentAssertions;
using Moq;
using PaymentProcessor.Application.Common.Repository;
using PaymentProcessor.Application.Common.Repository.Interface;
using PaymentProcessor.Application.Common.Services;
using PaymentProcessor.Application.Common.Services.Interface;
using PaymentProcessor.Application.Features.Payments.Commands;
using PaymentProcessor.Application.Features.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using PaymentProcessor.Domain.Entities;

namespace PaymentProcessor.Tests.Payments.PaymentCommands
{
    public class PaymentPrecessorTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IPaymentRepository> _paymentRepository;
        private readonly IMapper _mapper;

        private readonly Mock<List<IPaymentGateway>> _PaymentGateways;
        public PaymentPrecessorTests()
        {
            _paymentRepository = new Mock<IPaymentRepository>();
            _PaymentGateways = new Mock<List<IPaymentGateway>>();

            _unitOfWork = new Mock<IUnitOfWork>();

            _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }


        [Fact]
        public async Task Handle_AmountIsLessThan20Pounds_ProcessedByCheapPaymentGateway()
        {
            //Arrange
            var Payment = new PaymentPrecessorCommand
            {
                Amount = 19,
                CardHolder = "Chima Okoli",
                CreditCardNumber = "5344765787649836",
                 ExpirationMonth = "06",
                  ExpirationYear = "2021",
                   SecurityCode = "777"
            };

            _PaymentGateways.Object.Add(new CheapPaymentGateway());

            var handler = new PaymentPrecessorCommandHandler(_PaymentGateways.Object.AsEnumerable(), _paymentRepository.Object, _unitOfWork.Object, _mapper);


            //Act
            var result = await handler.Handle(Payment, CancellationToken.None);


            //Assert
            _paymentRepository.Verify(x => x.Add(It.IsAny<Payment>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            result.Should().BeOfType<PaymentDto>();
        }

     

        [Fact]
        public async Task Handle_AmountIsBetween20and500Pounds_ExpensiveGatewayIsAvailable_ProcessedByExpensivePaymentGateway()
        {
            var Payment = new PaymentPrecessorCommand
            {
                Amount = 21,
                CardHolder = "Chima Okoli",
                CreditCardNumber = "5344765787649836",
                ExpirationMonth = "06",
                ExpirationYear = "2021",
                SecurityCode = "777"
            };

            _PaymentGateways.Object.Add(new ExpensivePaymentGateway());

            var handler = new PaymentPrecessorCommandHandler(_PaymentGateways.Object.AsEnumerable(), _paymentRepository.Object, _unitOfWork.Object, _mapper);


            //Act
            var result = await handler.Handle(Payment, CancellationToken.None);


            //Assert
            _paymentRepository.Verify(x => x.Add(It.IsAny<Payment>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            result.Should().BeOfType<PaymentDto>();
        }

        [Fact]
        public async Task Handle_AmountIsBetween20and500Pounds_ExpensiveGatewayIsNotAvailable_ProcessedByCheapPaymentGateway()
        {
            var Payment = new PaymentPrecessorCommand
            {
                Amount = 21,
                CardHolder = "Chima Okoli",
                CreditCardNumber = "5344765787649836",
                ExpirationMonth = "06",
                ExpirationYear = "2021",
                SecurityCode = "777"
            };

            _PaymentGateways.Object.Add(new ExpensivePaymentGateway { IsAvailable = false });
            _PaymentGateways.Object.Add(new CheapPaymentGateway());

            var handler = new PaymentPrecessorCommandHandler(_PaymentGateways.Object.AsEnumerable(), _paymentRepository.Object, _unitOfWork.Object, _mapper);


            //Act
            var result = await handler.Handle(Payment, CancellationToken.None);


            //Assert
            _paymentRepository.Verify(x => x.Add(It.IsAny<Payment>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            result.Should().BeOfType<PaymentDto>();
        }

        [Fact]
        public async Task Handle_AmountIsGreaterThan500Pounds_PremiumPaymentService()
        {
            var Payment = new PaymentPrecessorCommand
            {
                Amount = 501,
                CardHolder = "Chima Okoli",
                CreditCardNumber = "5344765787649836",
                ExpirationMonth = "06",
                ExpirationYear = "2021",
                SecurityCode = "777"
            };

            _PaymentGateways.Object.Add(new PremiumPaymentService());

            var handler = new PaymentPrecessorCommandHandler(_PaymentGateways.Object.AsEnumerable(), _paymentRepository.Object, _unitOfWork.Object, _mapper);


            //Act
            var result = await handler.Handle(Payment, CancellationToken.None);


            //Assert
            _paymentRepository.Verify(x => x.Add(It.IsAny<Payment>()), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            result.Should().BeOfType<PaymentDto>();
        }

       
    }
}
