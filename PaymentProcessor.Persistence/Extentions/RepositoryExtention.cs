using Microsoft.Extensions.DependencyInjection;
using PaymentProcessor.Application.Common.Repository;
using PaymentProcessor.Application.Common.Repository.Interface;
using PaymentProcessor.Persistence.Repository;
using PaymentProcessor.Persistence.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Persistence.Extentions
{
    public static class RepositoryExtention
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

        }
    }
}
