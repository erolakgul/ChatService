﻿
using chatService.core.Services.Basis;
using chatService.core.UOW;
using chatService.data.UOW;
using chatService.service.Bussiness.Basis;
using Microsoft.Extensions.DependencyInjection;

namespace chatService.startup.Provider
{
    public class CustomServiceProvider
    {
        ServiceProvider serviceProvider;

        public ServiceProvider GetServiceProvider()
        {
            serviceProvider = new ServiceCollection()
                                     .AddTransient<IMessageService, MessageService>()
                                     .AddTransient<IErrorService, ErrorService>()
                                     .AddTransient<IUnitOfWork, UnitOfWork>()
                                     .BuildServiceProvider();

            return serviceProvider;
        }
    }
}