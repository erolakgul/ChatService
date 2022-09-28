﻿using chatService.core.DTO;
using chatService.core.UOW;
using chatService.helper.Service.Concretes;
using chatService.helper.UOW.Interface;
using chatService.service.Bussiness.Basis;
using chatService.service.Bussiness.Main;
using chatService.startup.Configurations;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

//System.Threading.Thread.Sleep(3000);
#region app environment
Console.WriteLine("...........................Client Anatolia Application.............................");
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

Console.WriteLine();
Console.WriteLine(" Client Anatolia started => " + connectionSettings.IpAddress + " " + connectionSettings.PortNumber + "          " + DateTime.Now);
Console.WriteLine();

#region variables
MessageDto messageDto = null;
ErrorDto errorDto = null;
string nickNameKey = Console.ReadLine();
string _content = String.Empty;
#endregion

#region getting socket service  for client
SocketService socketService = new(iUnitOfWork);

IPAddress ipAddress = System.Net.IPAddress.Parse(connectionSettings.IpAddress);
socketService.Start(new IPEndPoint(ipAddress, connectionSettings.PortNumber));
#endregion

#region get instance for errordto caching service
ErrorService errorService = new ErrorService(unitOfWork: iUnitOfWork);
#endregion

#region get instance for messagedto caching service
MessageService messageService = new MessageService(unitOfWork: iUnitOfWork);
#endregion

#region get instance for singleton guid with di
CustomCacheService<Guid> customCacheService = new(customHelperGuidUOW);
customCacheService.SetMemoryCache("LOCALSESSIONGUID", socketService.LocalSessionID);
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
            Console.WriteLine("");
            Console.WriteLine("");

            #region fill dto
            messageDto = new MessageDto()
            { ID = Guid.NewGuid(), GlobalSessionID = socketService.GlobalSessionID, LocalSessionID = socketService.LocalSessionID, Content = _content, NickName = nickNameKey.ToString(), IsRead = false, CreatedDate = System.DateTime.Now };
            #endregion

            #region sending message from Client Anatolia to server
            try
            {
                processCount += processCount;
                socketService.TransferData(messageDto);

                #region if message sending is success , save the cache
                messageService.FillMessage(nickNameKey, socketService.LocalSessionID, messageDto);
                #endregion
            }
            catch (Exception ex)
            {
                errorCount += errorCount;

                errorDto = new() { CountOfError = errorCount, CreatedBy = messageDto.NickName, CreatedDate = System.DateTime.Now, ErrorCode = ex.Source, ErrorReason = ex.Message, GlobalSessionID = messageDto.GlobalSessionID, LocalSessionID = messageDto.LocalSessionID, ID = Guid.NewGuid() };
                errorService.FillError(nickNameKey, socketService.LocalSessionID, errorDto);

                //System.Threading.Thread.Sleep(1000);
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine(" !!! #clientAnatolia_program# Failed Communication !!! \n \n Session ID     : {0} \n Nickname             : {1} \n Message id: {2} \n Your Message Content : {3} \n Message Sent Date    : {4} \n Error Reason         : {5} \n Error Count          : {6}", socketService.LocalSessionID, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate, errorDto.ErrorReason, errorDto.CountOfError);
                Console.WriteLine("");
                Console.WriteLine("");
                isCanBeSentMessage = false;
            }

            #endregion

            if (errorCount == 0)
            {
                #region on service
                messageDto = messageService.GetMessage(nickNameKey, socketService.LocalSessionID);
                #endregion

                //System.Threading.Thread.Sleep(1000);
                Console.WriteLine("  #clientAnatolia_program# \n G.Session ID         : {0} \n L.Session ID         : {1} \n Nickname             : {2} \n Message id           : {3} \n Your Message Content : {4} \n Message Sent Date    : {5}", socketService.GlobalSessionID, socketService.LocalSessionID, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate);
                Console.WriteLine("");
                Console.WriteLine("");
            }

        }

    }
}

#endregion



Console.ReadLine();