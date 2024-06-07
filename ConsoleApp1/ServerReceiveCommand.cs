using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
        public class ServerReceiveCommand : ICommand
        {
            private UdpClient udpClient;
            private IPEndPoint iPEndPoint;
            private string messageText;
            private MessageList messageList;

            public ServerReceiveCommand(UdpClient udpClient, IPEndPoint iPEndPoint, string messageText, MessageList messageList)
            {
                this.udpClient = udpClient;
                this.iPEndPoint = iPEndPoint;
                this.messageText = messageText;
                this.messageList = messageList;
            }

            public void Execute()
            {
                if (messageText.ToLower() == "exit")
                {
                    Console.WriteLine("Сервер завершает работу...");
                }
                else
                {
                    Message message = Message.DeserializeFromJson(messageText);
                    messageList.AddMessage(message);
                    messageList.PrintMessages();

                    byte[] confirmationData = Encoding.UTF8.GetBytes("Сообщение успешно получено");
                    udpClient.Send(confirmationData, confirmationData.Length, iPEndPoint);
                }
            }
        }
    
}
