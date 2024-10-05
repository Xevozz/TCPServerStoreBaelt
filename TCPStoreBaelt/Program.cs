// See https://aka.ms/new-console-template for more information

using TCPStoreBaelt;

Console.WriteLine("Hello, World!");

TcPServerStoreBaelt tcPServerStoreBaelt = new TcPServerStoreBaelt();
tcPServerStoreBaelt.Start();

TcpServerStorebaeltJson tcpServerStorebaeltJson = new TcpServerStorebaeltJson();
tcpServerStorebaeltJson.Start();

Console.ReadKey();