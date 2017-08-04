
//the Server class
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class FileServer
{
    static string hostName = "127.0.0.1";
    static int port = 65000; //if this port is not available on your machine; pick up some other
    IPAddress localAddr = IPAddress.Parse(hostName);

    public void Start()
    {            
        TcpListener tcpListner = new TcpListener(localAddr, port);
        tcpListner.Start(); //start listening to client request
        for (; ; ) //infinite loop
        {
            //blocks until a client request comes
            Socket socket = tcpListner.AcceptSocket(); 
            if (socket.Connected)
            {
                //Delegate to the SendFileToClient method
                SendFileToClient(socket);
                socket.Close();
            }
        }
    }

    void SendFileToClient(Socket socket)
    {
        NetworkStream netStream = new NetworkStream(socket);
        StreamWriter writer = new StreamWriter(netStream);
        //make sure the specified file exists on your server machine
        FileStream fileStream = File.Open(@"D:\csharp.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
        StreamReader reader = new StreamReader(fileStream);
        string strReadLine = null;
        do
        {
            strReadLine = reader.ReadLine();
            if(strReadLine!= null) writer.WriteLine(strReadLine);
            writer.Flush(); //make sure content is flushed and reaches the client
        } while (strReadLine != null); //exit when nothing more to read
        writer.Close();
    }
}

//the Client class
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

public class Client
{
    //modify the host name if you are running the server in a different machine
    static string hostName = "127.0.0.1"; 
    static int port = 65000;

    public void FetchFileFromServer()
    {
        TcpClient client = new TcpClient(hostName, port);
        if (client.Connected)
        {
            NetworkStream netStream = client.GetStream();
            StreamReader reader = new StreamReader(netStream);
            string strReadLine = null;
            //note: make sure that the file location/ name is different than the server's location
            //if you are running the server and client on the same machine
            FileStream fileStream =
                File.Open(@"C:\csharp.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter writer = new StreamWriter(fileStream);
            do
            {
                strReadLine = reader.ReadLine();                    
                if (strReadLine != null)
                    writer.WriteLine(strReadLine);
            } while (strReadLine != null);  //exit when there is nothing to receive              
            writer.Close();
            netStream.Close();
        }
    }
}		

/*to test use this line of code*/
/*
            new Thread(new ThreadStart(new FileServer().Start)).Start();
            Thread client = new Thread(new ThreadStart(new Client().FetchFileFromServer));
            client.Start();
            client.Join();
            //the Server Thread keeps running