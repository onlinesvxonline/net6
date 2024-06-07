using System.Net.Sockets;
using System.Net;
using System.Text;
using ConsoleApp1;
using System.Threading;

public class ServerClientTests
{
    private object? cancellationToken;

    public void TestServerReceiveMessage()
    {   
        ThreadPool.QueueUserWorkItem((state) => 
        {
            Program.Server("Server", (CancellationToken)state);
        }, cancellationToken);

        UdpClient udpClient = new UdpClient();
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        byte[] messageBytes = Encoding.UTF8.GetBytes("Test message");
        udpClient.Send(messageBytes, messageBytes.Length, iPEndPoint);

    }

    public void TestClientSendMessage()
    {

        UdpClient udpClient = new UdpClient();
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        Program.SentMessage("Test", 123, "127.0.0.1");

    }

    public void TestClientGetUnreadMessages()
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        Program.GetUnreadMessages(udpClient, iPEndPoint, "127.0.0.1");

    }
}