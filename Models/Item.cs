using System.Text;

namespace Models;
public class Item
{
    private int _id;
    private string? _name;
    private int _balance = 0;
    public Item(int id, string? name, int balance=0)
    {
        _id = id;
        _name = name;
        _balance=balance;        
    }
    public int Id {
        set {
            _id = value;
        }
        get {
            return _id;
        }
    }
    public string? Name {
        set {
            _name = value;
        }
        get {
            return _name;
        }
    }

    public int Balance {
        set {
            _balance = value;
        }
        get {
            return _balance;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"ID: {this.Id} | Name: {this.Name}");
        return sb.ToString();
    }
}




