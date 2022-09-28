using chatService.core.DTO;
using chatService.core.Provider;
using chatService.core.UOW;
using chatService.helper.Service.Concretes;
using chatService.helper.UOW.Interface;
using chatService.service.Bussiness.Basis;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;

//System.Threading.Thread.Sleep(3000);
#region app environment
Console.WriteLine(".................................Server Application................................");
Console.WriteLine("");
#endregion

#region get dependency injection instance
ServiceProvider serviceProvider = CustomServiceProvider.GetInstance().serviceProvider;
IUnitOfWork iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
ICustomHelperUOW<Guid> customHelperGuidUOW = serviceProvider.GetService<ICustomHelperUOW<Guid>>();
#endregion

#region getting json path, then read
ConnectionSettings connectionSettings = new();
string filePath = connectionSettings.GetLibraryPath();
connectionSettings = connectionSettings.ReadJsonFile(filePath);
#endregion

Console.WriteLine("");
Console.WriteLine(" Server started => " + connectionSettings.IpAddress + " " + connectionSettings.PortNumber + "          " + DateTime.Now);
Console.WriteLine("");

#region getting socket service
SocketService socketService = new(iUnitOfWork);
System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(connectionSettings.IpAddress);
socketService.Start(new IPEndPoint(ipAddress, connectionSettings.PortNumber));
#endregion

#region getting listener service for server
ListenerService listenerService = new(iUnitOfWork);
listenerService.Start(Convert.ToInt32(connectionSettings.PortNumber), connectionSettings.MaxCountQueue);
#endregion

#region get instance for messagedto caching service
MessageService messageService = new MessageService(unitOfWork: iUnitOfWork);
#endregion

#region variables
MessageDto messageDto = null;
ErrorDto errorDto = null;
string nickNameKey = Console.ReadLine();
string _content = String.Empty;
#endregion

#region get instance for singleton guid with di
Guid sessionGuid = GuidProvider.GetInstance().Id;
CustomCacheService<Guid> customCacheService = new(customHelperGuidUOW);
customCacheService.SetMemoryCache("LOCALSESSIONGUID", sessionGuid);
#endregion

//#region info for server (socket ten instance alınmayacaksa burası açık kalsın)
//Console.WriteLine("... Listening is successfuly... \n    IpAddress         : {0} \n    Port No           : {1} \n    Global Session ID : {2} \n    Local  Session ID : {3}", connectionSettings.IpAddress, connectionSettings.PortNumber, listenerService.GlobalSessionID, listenerService.LocalSessionID);
//Console.WriteLine("");
//#endregion

#region communication operation
if (nickNameKey.ToString().Length > 0)
{
    Console.WriteLine("....starting to send a new message");
    Console.WriteLine("");

    int processCount = 0;
    int errorCount = 0;

    bool isCanBeSentMessage = true;


    while (isCanBeSentMessage)
    {
        //Console.WriteLine("What is your message ?");
        _content = Console.ReadLine();
        if (_content.Length > 0)
        {
            Console.WriteLine("");
            Console.WriteLine("");

            #region fill dto and save cache
            messageDto = new MessageDto()
            { ID = Guid.NewGuid(), GlobalSessionID = listenerService.GlobalSessionID, LocalSessionID = listenerService.LocalSessionID, Content = _content, NickName = nickNameKey.ToString(), IsRead = false, CreatedDate = System.DateTime.Now };
            #endregion

            try
            {
                listenerService.CustomSendFromServer(messageDto).Wait();

                #region if message sending is success , save the cache
                messageService.FillMessage(nickNameKey, sessionGuid, messageDto); 
                #endregion
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("#server_program# ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("#server_program# SocketException : {0}", se.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("#server_program# : {0}", ex.Message);
            }
        }
    }
}
#endregion



Console.ReadLine();




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
