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
    #region delegate listener method
    //public delegate void OnMessageReceived(MessageDto messageDto);
    #endregion
    /// <summary>
    /// FOR CLIENTS
    /// </summary>
    public class SocketRepository : ISocketRepository
    {
        private Socket _socket;
        private IPEndPoint _ipEndPoint;
        private SocketError _socketError;

        byte[] _data = new byte[1024];

        #region creater ID
        private static string _sessionGlobalID = "";
        public string GlobalSessionID { get { return _sessionGlobalID; } }

        private static Guid _sessionLocalID = GuidProvider.GetInstance().Id;
        public Guid LocalSessionID { get { return _sessionLocalID; } }
        #endregion

        public SocketRepository()
        {
            #region defined socket configuration such as type, protocol and network ecosystem
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #endregion
        }

        public void Start(IPEndPoint ipEndPoint)
        {
            #region SESSION ID
            SHA1 sha = new SHA1CryptoServiceProvider();
            string datatoEncrypt = ipEndPoint.Port.ToString();
            _sessionGlobalID = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(datatoEncrypt)));
            #endregion

            //#region SESSION GUID
            //_sessionGUID = Guid.NewGuid();
            //#endregion

            if (ipEndPoint is null)
            {
                Console.WriteLine("IPEndPoint could not identify");
                return;
            }
            _ipEndPoint = ipEndPoint;

            #region async call back for onconnected
            _socket.BeginConnect(_ipEndPoint, OnConnected, null);
            #endregion
        }

        public void TransferData(MessageDto messageDto)
        {
            using (MemoryStream memoryStream = new())
            {
                #region convert the format of data which transfered
                new BinaryFormatter().Serialize(memoryStream, messageDto);
                IList<ArraySegment<byte>> arrSegList = new List<ArraySegment<byte>>();
                arrSegList.Add(new ArraySegment<byte>(memoryStream.ToArray()));
                #endregion


                #region data tranfer is started
                _socket.BeginSend(arrSegList, SocketFlags.None, out _socketError, (asyncResult) =>
                 {
                     int sentDataLength = _socket.EndSend(asyncResult, out _socketError);
                     if (sentDataLength <= 0 || _socketError != SocketError.Success)
                     {
                         Console.WriteLine("#Socket_OnConnected# There is no data. Connection must be controlled..");
                         return;
                     }
                 }, null);


                if (_socketError != SocketError.Success)
                {
                    Console.WriteLine("#Socket_OnConnected# Connection must be controlled..");
                }
                #endregion

                memoryStream.Close();
                memoryStream.Flush();
                memoryStream.Dispose();

            }
        }

        public void OnConnected(IAsyncResult asyncResult)
        {
            try
            {
                #region firstly ended async connecting process , then receive the socket that connected
                _socket.EndConnect(asyncResult);

                _socket.BeginReceive(_data, 0, _data.Length, SocketFlags.None, OnReceived, null);

                Console.WriteLine("... Listening is successfuly... \n    IpAddress         : {0} \n    Port No           : {1} \n    Global Session ID : {2} \n    Local  Session ID : {3}", _ipEndPoint.Address, _ipEndPoint.Port, GlobalSessionID, LocalSessionID);
                Console.WriteLine("");
                Console.WriteLine("What is your nickname ?");
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("#Socket_OnConnected# Process is unsuccessfuly => " + ex.Message);
            }
        }

        public void OnReceived(IAsyncResult asyncResult)
        {
            #region measuring the size of data which received
            int dataLength = _socket.EndReceive(asyncResult, out _socketError);
            #endregion

            if (dataLength <= 0 || _socketError != SocketError.Success)
            {
                Console.WriteLine("#Socket_OnConnected# There is no data. Connection must be controlled...");
                Console.WriteLine("");
                return;
            }

            #region if data receiving is successfuly, continue listening the socket
            _socket.BeginReceive(_data, 0, _data.Length, SocketFlags.None, OnReceived, null);
            #endregion
        }
    }
}
