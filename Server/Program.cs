using chatService.core.UOW;
using chatService.helper.Service.Concretes;
using chatService.helper.UOW.Interface;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;

//System.Threading.Thread.Sleep(3000);
#region app environment
Console.WriteLine("...................Server Application...................");
#endregion

#region get dependency injection instance
ServiceProvider serviceProvider = CustomServiceProvider.GetInstance().serviceProvider;
IUnitOfWork iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
ICustomHelperUOW<Guid> customHelperGuidUOW = serviceProvider.GetService<ICustomHelperUOW<Guid>>();
#endregion

#region getting socket service
SocketService socketService = new(iUnitOfWork);
#endregion


#region getting json path
ConnectionSettings connectionSettings = new();
string filePath = connectionSettings.GetLibraryPath();
connectionSettings = connectionSettings.ReadJsonFile(filePath);
#endregion

Console.WriteLine("");
Console.WriteLine(" Server started => " + connectionSettings.IpAddress + " " + connectionSettings.PortNumber);
Console.WriteLine("");

#region getting listener service for server
ListenerService listenerService = new(iUnitOfWork);

listenerService.Start(Convert.ToInt32(connectionSettings.PortNumber), connectionSettings.MaxCountQueue);
#endregion

#region get instance for singleton guid with di
Guid sessionGuid = GuidProviderService.GetInstance().Id;
CustomCacheService<Guid> customCacheService = new(customHelperGuidUOW);
customCacheService.SetMemoryCache("LOCALSESSIONGUID", sessionGuid);
//Console.WriteLine("LOCALSESSIONGUID" + sessionGuid);
#endregion

#region getting sessionıd
//Console.WriteLine("GLOBALSESSIONID :" + listenerService.SessionID);
#endregion

#region info for server
Console.WriteLine("... Listening is successfuly... \n    IpAddress         : {0} \n    Port No           : {1} \n    Global Session ID : {2} \n    Local  Session ID : {3}", connectionSettings.IpAddress, connectionSettings.PortNumber, listenerService.SessionID, listenerService.SessionGUID);
Console.WriteLine("");
#endregion

/*
#region get instance for messagedto caching operation
MessageService messageService = new MessageService(unitOfWork : iUnitOfWork);
Guid sessionGuid = Guid.NewGuid();

Console.WriteLine("What is your nickname ?");
string nickNameKey = Console.ReadLine();

string _content = String.Empty;

if (nickNameKey.ToString().Length > 0)
{
    Console.WriteLine("....To send a new message, simply type NEW");
    Console.WriteLine("");

    while(Console.ReadLine() == "NEW")
    {
        Console.WriteLine("What is your message ?");
        _content = Console.ReadLine();
        if (_content.Length > 0)
        {
            Console.WriteLine(".."); Console.WriteLine("...");
            Console.WriteLine("your message sending....");
        }

        messageService.FillMessage(nickNameKey, sessionGuid, new MessageDto()
        { ID = Guid.NewGuid(), Content = _content, NickName = nickNameKey.ToString(), IsRead = false, CreatedDate = System.DateTime.Now }
                          );

        #region on service
        MessageDto messageDto = messageService.GetMessage(nickNameKey, sessionGuid);
        #endregion

        System.Threading.Thread.Sleep(1000);
        Console.WriteLine(" Communication ID     : {0} \n Nickname             : {1} \n Message id           : {2} \n Your Message Content : {3} \n Message Sent Date    : {4}", sessionGuid, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate);
        Console.WriteLine("");
        Console.WriteLine("");
    }
}
#endregion
*/


Console.ReadLine();