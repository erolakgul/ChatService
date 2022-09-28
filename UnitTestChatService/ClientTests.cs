using chatService.startup.Configurations;
using NUnit.Framework;

namespace UnitTestChatService
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public void Check_Client_ConnectionIP_IsActýve_WhenExecuted()
        {
            #region  Arrange
            ConnectionSettings connectionSettings = new();
            #endregion

            #region act
            string filePath = connectionSettings.GetLibraryPath();
            connectionSettings = connectionSettings.ReadJsonFile(filePath);
            bool response = connectionSettings.IsActive;
            #endregion

            #region assert
            Assert.AreEqual(response, true); 
            Assert.AreNotSame(connectionSettings.PortNumber, connectionSettings.MaxCountQueue);
            #endregion
        }


    }
}