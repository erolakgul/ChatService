using chatService.core.DTO;
using chatService.core.UOW;
using chatService.service.Bussiness.Basis;
using chatService.startup.Provider;
using Microsoft.Extensions.DependencyInjection;

#region app environment
Console.WriteLine("Server Application");
Console.WriteLine("");
#endregion

#region get dependency injection
ServiceProvider serviceProvider = new CustomServiceProvider().GetServiceProvider();
IUnitOfWork iUnitOfWork = serviceProvider.GetService<IUnitOfWork>();
#endregion

#region get instance for messagedto caching operation
MessageService messageService = new MessageService(unitOfWork : iUnitOfWork);
Guid guid = Guid.NewGuid();

Console.WriteLine("What is your nickname ?");
string nickNameKey = Console.ReadLine();

string _content = String.Empty;

if (nickNameKey.ToString().Length > 0)
{
    Console.WriteLine("What is your message ?");
    _content = Console.ReadLine();
    if (_content.Length > 0)
    {
        Console.WriteLine("your message sending....");
    }
}

messageService.FillMessage(nickNameKey, guid, new MessageDto()
             { ID = Guid.NewGuid(), Content = _content , NickName = nickNameKey.ToString(), IsRead = false, CreatedDate = System.DateTime.Now }
                          );
#endregion

#region on service
MessageDto messageDto = messageService.GetMessage(nickNameKey, guid);
#endregion

System.Threading.Thread.Sleep(1000);
Console.WriteLine(" Message id :{0} \n Nickname :{1} \n Your Message Content : {2} \n Message Sent Date : {3}", messageDto.ID, messageDto.NickName, messageDto.Content ,messageDto.CreatedDate);

Console.ReadLine();