﻿using chatService.core.DTO;
using chatService.core.Repositories.Main;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace chatService.data.Repositories.Main
{
    public class SocketRepository : ISocketRepository
    {
        private readonly Socket _socket;
        private IPEndPoint _ipEndPoint;
        private SocketError _socketError;

        byte[] _data = new byte[1024];

        private static string _sessionID = "";
        public string SessionID { get { return _sessionID; } }

        private static Guid _sessionGUID = Guid.NewGuid();
        public Guid SessionGUID { get { return _sessionGUID; } }

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
            _sessionID = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(datatoEncrypt)));
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
                List<ArraySegment<byte>> arrSegList = new();
                arrSegList.Add(new ArraySegment<byte>(memoryStream.ToArray()));
                #endregion

                #region data tranfer is started
                _socket.BeginSend(arrSegList, SocketFlags.None, out _socketError, (asyncResult) =>
                 {

                     int sentDataLength = _socket.EndSend(asyncResult, out _socketError);
                     if (sentDataLength <= 0 || _socketError != SocketError.Success)
                     {
                         Console.WriteLine("#SOCKETREPOTRANSFER# There is no data. Connection must be controlled..");
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

        public void OnConnected(IAsyncResult asyncResult)
        {
            try
            {
                #region firstly ended async connecting process , then listen the socket that connected
                _socket.EndConnect(asyncResult);

                _socket.BeginReceive(_data, 0, _data.Length, SocketFlags.None, OnReceived, null);

                Console.WriteLine("... Listening is successfuly... \n    IpAddress         : {0} \n    Port No           : {1} \n    Global Session ID : {2} \n    Local  Session ID : {3}", _ipEndPoint.Address, _ipEndPoint.Port, SessionID, SessionGUID);
                Console.WriteLine("");
                Console.WriteLine("What is your nickname ?");
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
                Console.WriteLine("#SOCKETREPOONRECEIVED# There is no data. Connection must be controlled...");
                Console.WriteLine("");
                return;
            }

            #region if data receiving is successfuly, continue listening the socket
            _socket.BeginReceive(_data, 0, _data.Length, SocketFlags.None, OnReceived, null);
            #endregion
        }
    }
}
