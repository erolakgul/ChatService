using chatService.core.DTO;
using chatService.core.Repositories.Main;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace chatService.data.Repositories.Main
{
    public class SocketRepository : ISocketRepository
    {
        private readonly Socket _socket;
        private readonly IPEndPoint _ipEndPoint;
        private SocketError _socketError;

        byte[] _data = new byte[1024];
        public SocketRepository(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;

            #region defined socket configuration such as type, protocol and network ecosystem
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #endregion
        }

        public void Start()
        {
            #region async call back for onconnected
            _socket.BeginConnect(_ipEndPoint, OnConnected, null);
            #endregion
        }

        public void TransferData(MessageDto messageDto)
        {
            using (MemoryStream memoryStream =new ())
            {
                #region convert the format of data which transfered
                new BinaryFormatter().Serialize(memoryStream, messageDto);
                List<ArraySegment<byte>> arrSegList = new();
                arrSegList.Add(new ArraySegment<byte>(memoryStream.ToArray()));
                #endregion

                #region data tranfer is started
                _socket.BeginSend(arrSegList, SocketFlags.Broadcast, out _socketError, (asyncResult) =>
                 {
               
                     int sentDataLength = _socket.EndSend(asyncResult,out _socketError);
                     if (sentDataLength > 0 || _socketError != SocketError.Success)
                     {
                         Console.WriteLine("There is no data. Connection must be controlled..");
                         return;
                     }
               
                 }, null);

                if (_socketError != SocketError.Success)
                {
                    Console.WriteLine("Connection must be controlled..");
                }
                #endregion


            }
        }

        public void OnConnected(IAsyncResult asyncResult)
        {
            try
            {
                #region firstly ended async connecting process , then listen the socket that connected
                _socket.EndDisconnect(asyncResult);

                _socket.BeginReceive(_data, 0, _data.Length, SocketFlags.Broadcast, OnReceived, null);
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Process is unsuccessfuly => " + ex.Message);
            }
        }

        public void OnReceived(IAsyncResult asyncResult)
        {
            #region measuring the size of data which received
            int dataLength = _socket.EndReceive(asyncResult, out _socketError); 
            #endregion

            if (dataLength <= 0 || _socketError != SocketError.Success)
            {
                Console.WriteLine("There is no data. Connection must be controlled...");
                Console.WriteLine("");
                return;
            }

            #region if data receiving is successfuly, continue listening the socket
            _socket.BeginReceive(_data, 0, _data.Length, SocketFlags.Broadcast, OnReceived, null); 
            #endregion
        }
    }
}
