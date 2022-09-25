// See https://aka.ms/new-console-template for more information
#region app environment
using chatService.startup.Configurations;
using System.Reflection;

Console.WriteLine("...................Client Application...................");
#endregion

#region getting json path
ConnectionSettings connectionSettings = new ();

string filePath = connectionSettings.GetLibraryPath();
connectionSettings = connectionSettings.ReadJsonFile(filePath);
#endregion


Console.WriteLine();
Console.WriteLine(" Client started => " + connectionSettings.IpAddress + " " + connectionSettings.PortNumber);
Console.WriteLine();

Console.ReadLine();