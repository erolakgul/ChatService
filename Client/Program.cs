// See https://aka.ms/new-console-template for more information
using chatService.core.UOW;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

#region app environment
Console.WriteLine("...................Client Application...................");
#endregion

#region getting json path
ConnectionSettings connectionSettings = new();
string filePath = connectionSettings.GetLibraryPath();
connectionSettings = connectionSettings.ReadJsonFile(filePath);
#endregion

#region get dependency injection instance
ServiceProvider serviceProvider = new CustomServiceProvider().GetServiceProvider();
IUnitOfWork iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
#endregion

#region getting socket service
SocketService socketService = new(iUnitOfWork);

System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(connectionSettings.IpAddress);
socketService.Start(new IPEndPoint(ipAddress, connectionSettings.PortNumber));
#endregion


Console.WriteLine();
Console.WriteLine(" Client started => " + connectionSettings.IpAddress + " " + connectionSettings.PortNumber);
Console.WriteLine();


Console.ReadLine();