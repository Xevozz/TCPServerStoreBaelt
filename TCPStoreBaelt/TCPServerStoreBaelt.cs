namespace TCPStoreBaelt;
using TCPStoreBaelt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class TcPServerStoreBaelt
{
    private const int PORT = 7;
    
    private readonly Random _random = new();

    public void Start()
    {
        // definerer server med port nummer
        TcpListener server = new TcpListener(PORT);
        server.Start();
        Console.WriteLine("Server startet på port " + PORT);

        while (true)
        {
            // venter på en klient 
            TcpClient socket = server.AcceptTcpClient();

            Task.Run(
                () =>
                {
                    TcpClient tempsocket = socket;
                    DoOneClient(tempsocket);
                }
            );

        }
        //server.Stop();
    }

    private void DoOneClient(TcpClient socket)
    {
        Console.WriteLine($"Min egen (IP, port) = {socket.Client.LocalEndPoint}");
        Console.WriteLine($"Accepteret client (IP, port) = {socket.Client.RemoteEndPoint}");


        // åbner for tekst strenge
        StreamReader sr = new StreamReader(socket.GetStream());
        StreamWriter sw = new StreamWriter(socket.GetStream());

        DoEkko(sr, sw);

        sr?.Close();
        sw?.Close();
    }

    private void DoEkko(StreamReader sr, StreamWriter sw)
    {
        // læser linje fra nettet
        string l = sr.ReadLine();
        Console.WriteLine("Modtaget: " + l);

        // skriver linje tilbage - stadig ekko
        sw.WriteLine("input numbers");
        sw.Flush();
        
        string t = sr.ReadLine();
        Console.WriteLine("Ekko: " + t);

       string[]numbers = t.Split(" "); 
       
       int number = Convert.ToInt32(numbers[0]);
       int number2 = Convert.ToInt32(numbers[1]);
       
       int result = 0;
       
       if (l == "add") { result = number + number2; }
       if (l == "sub") { result = number - number2; }
       if (l == "random") { result = _random.Next(number, number2);  }
       
       sw.WriteLine(result);
        sw.Flush();
    }

    /*private void DoCount(StreamReader sr, StreamWriter sw)
    {
        // læser linje fra nettet
        string l = sr.ReadLine();
        Console.WriteLine("Modtaget: " + l);


        // hvis tælle ord
        string[] strings = l.Split();
        int antalord = strings.Length;

        // skriver linje tilbage - stadig ekko
        sw.WriteLine($"Antal ord er {antalord}");
        sw.Flush();
    }*/
    
}