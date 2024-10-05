using System.Net.Sockets;
using System.Text.Json;

namespace TCPStoreBaelt;

public class TcpServerStorebaeltJson
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
        var inputJson = sr.ReadLine();
        Console.WriteLine("Modtaget: " + inputJson);

        try
        {
            var model = JsonSerializer.Deserialize<Contract>(inputJson!)!;

            var result = model.Operation.ToLower() switch
            {
                "add" => model.Number1 + model.Number2,
                "sub" => model.Number1 - model.Number2,
                "random" => _random.Next(model.Number1, model.Number2),
                _=> throw new Exception("Invalid operation")
            };

            sw.WriteLine(result);
            sw.Flush();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            sw.WriteLine("Something went wrong");
            sw.Flush();
        }
    }
    
    public class Contract
    {
        public string Operation { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
}