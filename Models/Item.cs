using System.Text;

namespace Models;
public class Item
{
    private int _id;
    private string? _name;
    private int _balance = 0; //Added for joining Item and User_items tables and showing item quantitry in the table of user items
    private int _unitPrice = 0; //Added for joining Item and Marketplace tables and showing item price in the table of marketplace items
    private string _url="";
    public Item(int id, string? name, int balance=0, int unitprice=0, string url="")
    {
        _id = id;
        _name = name;
        _balance=balance;
        _unitPrice=unitprice;
        _url=url;        
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

     public int UnitPrice {
        set {
            _unitPrice = value;
        }
        get {
            return _unitPrice;
        }
    }

     public string Url {
        set {
            _url = value;
        }
        get {
            return _url;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"ID: {this.Id} | Name: {this.Name}");
        return sb.ToString();
    }
}




