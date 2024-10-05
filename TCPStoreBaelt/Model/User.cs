namespace TCPStoreBaelt.Model;

public class User
{
    public string Method { get; set; }
    public int Tal1 { get; set; }
    public int Tal2 { get; set; }

    public User(string method, int tal1, int tal2)
    {
        Method = method;
        Tal1 = tal1;
        Tal2 = tal2;
    }
    
    public User():this("", 1, 2)
    {
        
    }

    public override string ToString()
    {
        return
            $"{nameof(Method)}: {Method}, {nameof(Tal1)}: {Tal1}, {nameof(Tal2)}: {Tal2}";
    }
}