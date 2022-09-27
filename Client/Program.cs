using chatService.core.DTO;
using chatService.core.UOW;
using chatService.helper.Service.Concretes;
using chatService.helper.UOW.Interface;
using chatService.service.Bussiness.Basis;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

#region app environment
Console.WriteLine("...................Client Application...................");
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

#region message and error dto
MessageDto messageDto = null;
ErrorDto errorDto = null;
#endregion

#region getting socket service for client
SocketService socketService = new(iUnitOfWork);

System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(connectionSettings.IpAddress);
socketService.Start(new IPEndPoint(ipAddress, connectionSettings.PortNumber));
#endregion

Console.WriteLine();
Console.WriteLine(" Client started => " + connectionSettings.IpAddress + " " + connectionSettings.PortNumber);
Console.WriteLine();

#region get instance for errordto caching service
ErrorService errorService = new ErrorService(unitOfWork: iUnitOfWork);
#endregion

#region get instance for messagedto caching service
MessageService messageService = new MessageService(unitOfWork: iUnitOfWork);
#endregion

#region get instance for singleton guid with di
Guid sessionGuid = socketService.LocalSessionID;
CustomCacheService<Guid> customCacheService = new(customHelperGuidUOW);
customCacheService.SetMemoryCache("LOCALSESSIONGUID", sessionGuid);
#endregion


#region variables
string nickNameKey = Console.ReadLine();
string _content = String.Empty;
#endregion

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
            Console.WriteLine(".."); Console.WriteLine("...");
            Console.WriteLine("your message sending....");
        }

        #region fill dto and save cache
        messageDto = new MessageDto()
        { ID = Guid.NewGuid(), GlobalSessionID = socketService.GlobalSessionID, LocalSessionID = socketService.LocalSessionID, Content = _content, NickName = nickNameKey.ToString(), IsRead = false, CreatedDate = System.DateTime.Now };

        messageService.FillMessage(nickNameKey, sessionGuid, messageDto);
        #endregion

        #region chat service with socket
        try
        {
            processCount += processCount;
            socketService.TransferData(messageDto);
        }
        catch (Exception ex)
        {
            errorCount += errorCount;

            errorDto = new() { CountOfError = errorCount, CreatedBy = messageDto.NickName, CreatedDate = System.DateTime.Now, ErrorCode = ex.Source, ErrorReason = ex.Message, GlobalSessionID = messageDto.GlobalSessionID,LocalSessionID = messageDto.LocalSessionID , ID = Guid.NewGuid() };
            errorService.FillError(nickNameKey, sessionGuid, errorDto);

            //System.Threading.Thread.Sleep(1000);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" !!! Failed Communication !!! \n \n Session ID     : {0} \n Nickname             : {1} \n Message id: {2} \n Your Message Content : {3} \n Message Sent Date    : {4} \n Error Reason         : {5} \n Error Count          : {6}", sessionGuid, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate, errorDto.ErrorReason, errorDto.CountOfError);
            Console.WriteLine("");
            Console.WriteLine("");
            isCanBeSentMessage = false;
        }

        #endregion

        if (errorCount == 0)
        {
            #region on service
            messageDto = messageService.GetMessage(nickNameKey, sessionGuid);
            #endregion

            //System.Threading.Thread.Sleep(1000);
            Console.WriteLine(" L.Session ID         : {0} \n Nickname             : {1} \n Message id           : {2} \n Your Message Content : {3} \n Message Sent Date    : {4}", sessionGuid, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate);
            Console.WriteLine("");
            Console.WriteLine("");
        }

    }
}

#endregion



Console.ReadLine();