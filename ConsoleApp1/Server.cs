using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    public interface ICommand
    {
        void Execute();
    }

    
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Server("Server", cancellationToken);

            Console.WriteLine("Нажмите любую клавишу для завершения работы сервера...");
            Console.ReadKey();

            cancellationTokenSource.Cancel();
        }

        public static void Server(string name, CancellationToken cancellationToken)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            MessageList messageList = new MessageList();

            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (!cancellationToken.IsCancellationRequested)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                string messageText = Encoding.UTF8.GetString(buffer);

                ICommand command = new ServerReceiveCommand(udpClient, iPEndPoint, messageText, messageList);
                ThreadPool.QueueUserWorkItem(obj => { command.Execute(); });
            }

            udpClient.Close();
        }

        internal static void SentMessage(string v1, int v2, string v3)
        {
            throw new NotImplementedException();
        }

        internal static void GetUnreadMessages(UdpClient udpClient, IPEndPoint iPEndPoint, string v)
        {
            throw new NotImplementedException();
        }
    }
}