using chatService.core.UOW;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace UnitTestChatService
{
    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void Check_Server_LocalSessionID_IsEmpty_WhenExecuted()
        {
            #region arrange
            ConnectionSettings connectionSettings = new();
            #endregion

            #region act
            string filePath = connectionSettings.GetLibraryPath();
            connectionSettings = connectionSettings.ReadJsonFile(filePath);


              #region get dependency injection instance
              ServiceProvider serviceProvider = CustomServiceProvider.GetInstance().serviceProvider;
              IUnitOfWork iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
              #endregion
              #region getting listener service for server
              ListenerService listenerService = new(iUnitOfWork);
              listenerService.Start(Convert.ToInt32(connectionSettings.PortNumber), connectionSettings.MaxCountQueue);
            #endregion

            #endregion

            #region assert
            Assert.AreNotEqual(listenerService.LocalSessionID, Guid.Empty);
            #endregion

        }
    }
}
