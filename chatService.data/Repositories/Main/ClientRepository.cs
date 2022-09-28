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
            ///
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
            #region size of incoming data
            int dataLength = _socket.EndReceive(asyncResult, out _socketError); 
            #endregion

            if (dataLength <= 0 || _socketError != SocketError.Success)
            {
                Console.WriteLine("#Client_RepoOnReceivedCallBack# There is no data. Connection must be controlled...");
                Console.WriteLine("");
                return;
            }

            byte[] dataBuffer = new byte[dataLength];
            Array.Copy(_dataBuffer, 0, dataBuffer, 0, dataBuffer.Length);

            // incoming data is processed
            HandleReceivedData(dataBuffer);


            // continue to listen the socket after the data is processed
            _socket.BeginReceive(_dataBuffer, 0, _dataBuffer.Length, SocketFlags.None, OnReceivedCallBack, null);
        }

        public void HandleReceivedData(byte[] resizedOfBuffer)
        {
            if (_onMessageReceived != null)
            {
                using (MemoryStream memoryStream = new(resizedOfBuffer,0, resizedOfBuffer.Length))
                {
                    MessageDto messageDto = null;

                    try
                    {
                        //memoryStream.Position = 0; // added end of parse problem
                        //memoryStream.Seek(0, SeekOrigin.Begin);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                        messageDto = (MessageDto) new BinaryFormatter().Deserialize(memoryStream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("#Client_RepoOnReceivedCallBack# :" + ex.Message);
                    }
                    finally
                    {
                        memoryStream.Close();
                        memoryStream.Flush();
                        memoryStream.Dispose();
                    }
                    
                    _onMessageReceived(messageDto);
                }
            }
        }

      
    }
}
