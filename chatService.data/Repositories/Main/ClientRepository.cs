using chatService.core.DTO;
using chatService.core.Repositories.Main;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace chatService.data.Repositories.Main
{
    #region delegate listener method
    public delegate void OnMessageReceived(MessageDto messageDto); 
    #endregion
    public class ClientRepository : IClientRepository
    {
        #region initial variable
        public OnMessageReceived _onMessageReceived;
        Socket _socket;
        SocketError _socketError;
        byte[] _dataBuffer = new byte[1024];
        #endregion

        public ClientRepository()
        {

        }

        public void Start(Socket socket)
        {
            _socket = socket;

            #region starting listen the date coming from socket
            _socket.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, SocketFlags.None, OnReceivedCallBack, null);  
            #endregion
        }

        public void OnReceivedCallBack(IAsyncResult asyncResult)
        {
            int dataLength = _socket.EndReceive(asyncResult, out _socketError);

            if (dataLength <= 0 || _socketError != SocketError.Success)
            {
                Console.WriteLine("#CLIENTREPOONRECEIVEDCALLBACK# There is no data. Connection must be controlled...");
                Console.WriteLine("");
                return;
            }

            byte[] dataBuffer = new byte[dataLength];
            Array.Copy(_dataBuffer, 0, dataBuffer, 0, dataBuffer.Length);

            // Gelen datayı burada ele alacağız.
            HandleReceivedData(dataBuffer);

            // Tekrardan socket üzerinden data dinlemeye başlıyoruz.
            // Start();

            // Socket üzerinden data dinlemeye başlıyoruz.
            _socket.BeginReceive(_dataBuffer, 0, dataBuffer.Length, SocketFlags.None, OnReceivedCallBack, null);

        }

        public void HandleReceivedData(byte[] resizedOfBuffer)
        {
            if (_onMessageReceived != null)
            {
                using (MemoryStream memoryStream = new(resizedOfBuffer))
                {
                    MessageDto messageDto = new BinaryFormatter().Deserialize(memoryStream) as MessageDto;
                    _onMessageReceived(messageDto);
                }
            }
        }

      
    }
}
