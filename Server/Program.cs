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
//SocketService socketService = new(iUnitOfWork);
//System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse(connectionSettings.IpAddress);
//socketService.Start(new IPEndPoint(ipAddress, connectionSettings.PortNumber));
#endregion

#region getting listener service for server
ListenerService listenerService = new(iUnitOfWork);
listenerService.Start(Convert.ToInt32(connectionSettings.PortNumber), connectionSettings.MaxCountQueue);
#endregion

#region get instance for messagedto caching service
MessageService messageService = new MessageService(unitOfWork: iUnitOfWork);
ErrorService errorService = new ErrorService(unitOfWork: iUnitOfWork);
#endregion

#region variables
MessageDto messageDto = null;
ErrorDto errorDto = null;
string _content = String.Empty;
List<MessageDto> messageDtosForCaching = new List<MessageDto>();
List<ErrorDto> errorDtosForCaching = new List<ErrorDto>();
#endregion

#region get instance for singleton guid with di
Guid sessionGuid = GuidProvider.GetInstance().Id;
CustomCacheService<Guid> customCacheService = new(customHelperGuidUOW);
customCacheService.SetMemoryCache("LOCALSESSIONGUID", sessionGuid);
#endregion

#region info for server (socket ten instance alınmayacaksa burası açık kalsın)
Console.WriteLine("... Listening is successfuly... \n    IpAddress         : {0} \n    Port No           : {1} \n    Global Session ID : {2} \n    Local  Session ID : {3}", connectionSettings.IpAddress, connectionSettings.PortNumber, listenerService.GlobalSessionID, listenerService.LocalSessionID);
Console.WriteLine("");
#endregion

Console.WriteLine("What is your message ?");
#region read nickname
string nickNameKey = Console.ReadLine();
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
        #region listing the saved messages
        List<MessageDto> cahceMessageList = messageService.GetMessageList(nickNameKey, listenerService.LocalSessionID);
        List<ErrorDto> cahceErrorList = errorService.GetErrorList(nickNameKey, listenerService.LocalSessionID);
        #endregion

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

                if (cahceMessageList is null)
                {
                    messageDtosForCaching.Add(messageDto);
                    cahceMessageList = new List<MessageDto>();
                    cahceMessageList.AddRange(messageDtosForCaching);
                }
                else
                {
                    cahceMessageList.Add(messageDto);
                }

                messageService.AddMessageList(nickNameKey, listenerService.LocalSessionID, cahceMessageList);
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

                errorCount += errorCount;

                errorDto = new() { CountOfError = errorCount, CreatedBy = messageDto.NickName, CreatedDate = System.DateTime.Now, ErrorCode = ex.Source, ErrorReason = ex.Message, GlobalSessionID = messageDto.GlobalSessionID, LocalSessionID = messageDto.LocalSessionID, ID = Guid.NewGuid() };

                #region if message sending is failed , save the error cache

                if (cahceMessageList is null)
                {
                    errorDtosForCaching.Add(errorDto);
                    cahceErrorList = new List<ErrorDto>();
                    cahceErrorList.AddRange(errorDtosForCaching);
                }
                else
                {
                    cahceErrorList.Add(errorDto);
                }

                errorService.AddErrorList(nickNameKey, listenerService.LocalSessionID, cahceErrorList);
                #endregion

                #region  list on service
                List<ErrorDto> errorDtoList = errorService.GetErrorList(nickNameKey, listenerService.LocalSessionID);
                #endregion

                //System.Threading.Thread.Sleep(1000);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" !!! #server_program# Failed Communication !!! \n \n Session ID     : {0} \n Nickname             : {1} \n Message id: {2} \n Your Message Content : {3} \n Message Sent Date    : {4} \n Error Reason         : {5} \n Error Count          : {6}", listenerService.LocalSessionID, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate, errorDto.ErrorReason, errorDto.CountOfError);
                Console.WriteLine("");
                Console.WriteLine("");
                isCanBeSentMessage = false;
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
