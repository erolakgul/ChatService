using chatService.core.DTO;
using chatService.core.Provider;
using chatService.core.Repositories.Main;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace chatService.data.Repositories.Main
{
    public class ListenerRepository : IListenerRepository
    {
        private Socket _socket;
        private int _portNumber;
        private int _maxConnectionQueues;
        private SocketError _socketError;


        #region creater ID
        private static string _sessionGlobalID = "";
        public string GlobalSessionID { get { return _sessionGlobalID; } }

        private static Guid _sessionLocalID = GuidProvider.GetInstance().Id;
        public Guid LocalSessionID { get { return _sessionLocalID; } }
        #endregion

        public ListenerRepository()
        {
            #region defined socket configuration such as type, protocol and network ecosystem
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #endregion
        }

        public void Start(int portNumber, int maxConnectionQueues)
        {
            #region SESSION ID
#pragma warning disable SYSLIB0021 // Type or member is obsolete
            SHA1 sha = new SHA1CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
            string datatoEncrypt = portNumber.ToString();
            _sessionGlobalID = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(datatoEncrypt)));
            #endregion

            //#region SESSION GUID
            //_sessionGUID = Guid.NewGuid();
            //#endregion

            _portNumber = portNumber;
            _maxConnectionQueues = maxConnectionQueues;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _portNumber);

            #region ip configuration bind to the socket then start to listen connection coming from socket lastly if there is connection, accepting.. 
            _socket.Bind(endPoint);
            _socket.Listen(_maxConnectionQueues);

            try
            {
                _socket.BeginAccept(OnAccepted, _socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Listener Start Error => " + ex.Message);
            }

            #endregion
        }

        public void OnAccepted(IAsyncResult asyncResult)
        {
            #region ended asyn and start to listen client again
            Socket socket = _socket.EndAccept(asyncResult);
            ClientRepository clientRepository = new ClientRepository();
            #endregion

            #region data sending from client
            //clientRepository.Start(socket);
            clientRepository._onMessageReceived += new OnMessageReceived(OnMessageReceived);
            clientRepository.Start(socket);
            #endregion

            #region continue to listen 
            _socket.BeginAccept(OnAccepted, null);
            #endregion
        }

        public void OnMessageReceived(MessageDto messageDto)
        {
            if (messageDto is not null)
            {
                try
                {
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("*****************************" + messageDto.NickName + "*****************************");
                    Console.WriteLine(" G.Session ID         : {0} \n Nickname             : {1} \n Message id           : {2} \n Your Message Content : {3} \n Message Sent Date    : {4}", messageDto.GlobalSessionID, messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate);
                    Console.WriteLine("");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("#listener#OnMessageReceived :" + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("#listener#OnMessageReceived : MessageDto is null");
            }

        }

        public async Task CustomSendAsync(MessageDto messageDto)
        {
            using (MemoryStream memoryStream = new())
            {
                #region convert the format of data which transfered
                new BinaryFormatter().Serialize(memoryStream, messageDto);
                List<ArraySegment<byte>> arrSegList = new();
                arrSegList.Add(new ArraySegment<byte>(memoryStream.ToArray()));
                #endregion

                #region data tranfer is started
                _socket.BeginSend(arrSegList, SocketFlags.None, out _socketError, (asyncResult) =>
                {

                    int sentDataLength = _socket.EndSend(asyncResult, out _socketError);
                    if (sentDataLength <= 0 || _socketError != SocketError.Success)
                    {
                        Console.WriteLine("#LİSTENERCustomSendAsync# There is no data. Connection must be controlled..");
                        return;
                    }

                }, null);

                if (_socketError != SocketError.Success)
                {
                    Console.WriteLine("#SOCKETREPOTRANSFER# Connection must be controlled..");
                }
                #endregion
            }
        }
    }
}
