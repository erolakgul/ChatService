using chatService.core.DTO;
using chatService.core.UOW;
using chatService.service.Bussiness.Basis;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;

#region app environment
Console.WriteLine("...................Server Application...................");
Console.WriteLine("");
#endregion

#region get dependency injection
ServiceProvider serviceProvider = new CustomServiceProvider().GetServiceProvider();
IUnitOfWork iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
#endregion

#region getting json path
ConnectionSettings connectionSettings = new();
string filePath = connectionSettings.GetLibraryPath();
connectionSettings = connectionSettings.ReadJsonFile(filePath);
#endregion

Console.WriteLine(connectionSettings.PortNumber + " numbered port is listened...");
Console.WriteLine("");

#region getting listener service
ListenerService listenerService = new(iUnitOfWork);

listenerService.Start(Convert.ToInt32(connectionSettings.PortNumber), connectionSettings.MaxCountQueue);
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