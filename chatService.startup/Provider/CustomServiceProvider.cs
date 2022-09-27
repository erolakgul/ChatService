
using chatService.core.Services.Basis;
using chatService.core.Services.Main;
using chatService.core.UOW;
using chatService.data.UOW;
using chatService.helper.UOW.Concrete;
using chatService.helper.UOW.Interface;
using chatService.service.Bussiness.Basis;
using chatService.service.Bussiness.Main;
using Microsoft.Extensions.DependencyInjection;

namespace chatService.startup.Provider
{
    public class CustomServiceProvider
    {
        private static CustomServiceProvider _instance;

        #region nesne bir kere oluşturulur
        private static ServiceProvider _serviceProvider = new ServiceCollection()
                                   .AddSingleton<IMessageService, MessageService>()
                                   .AddSingleton<IErrorService, ErrorService>()
                                   .AddSingleton<ISocketService, SocketService>()
                                   .AddSingleton<IListenerService, ListenerService>()
                                   .AddSingleton<IClientService, ClientService>()
                                   .AddSingleton<IUnitOfWork, UnitOfWork>()
                                   .AddSingleton(typeof(ICustomHelperUOW<>), typeof(CustomHelperUOW<>))
                                   .AddSingleton(typeof(helper.Service.Interfaces.ICustomCacheService<>), typeof(helper.Service.Concretes.CustomCacheService<>))
                                   //.AddSingleton<ISocketService,GuidProviderService>()
                                   .BuildServiceProvider();

        #endregion
        public ServiceProvider serviceProvider { get { return _serviceProvider; } }

        //ServiceProvider serviceProvider;
        private CustomServiceProvider()
        {

        }

        public static CustomServiceProvider GetInstance()
        {
            if (_instance == null)
                _instance = new CustomServiceProvider();

            return _instance;
        }
    }
}
