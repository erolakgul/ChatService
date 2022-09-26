﻿using chatService.core.DTO;
using chatService.core.Repositories.Main;
using System.Net;
using System.Net.Sockets;

namespace chatService.data.Repositories.Main
{
    public class ListenerRepository : IListenerRepository
    {
        private readonly Socket _socket;
        private int _portNumber;
        private int _maxConnectionQueues;

        public ListenerRepository()
        {
            #region defined socket configuration such as type, protocol and network ecosystem
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            #endregion
        }

        public void Start(int portNumber, int maxConnectionQueues)
        {
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
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("*****************************" + messageDto.NickName + "*****************************");
            Console.WriteLine(" Session ID     : {0} \n Nickname             : {1} \n Message id           : {2} \n Your Message Content : {3} \n Message Sent Date    : {4}", messageDto.SessionID , messageDto.NickName, messageDto.ID, messageDto.Content, messageDto.CreatedDate);
            Console.WriteLine("");
            Console.WriteLine("");
        }

       
    }
}
