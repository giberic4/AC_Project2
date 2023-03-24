using System.Text;

namespace Models;
public class Sellinfo
{
    private int _itemId;
    private int _quantity;
    private int _sellerId;
    private int _price;
       
    public Sellinfo(int itemId, int quantity, int sellerId, int price)
    {
        _itemId=itemId;
        _quantity=quantity;
        _sellerId=sellerId;
        _price=price;
    }
    public int ItemId {
        set {
            _itemId=value;
        }
        get {
            return _itemId;
        }
    }
    public int Quantity {
        set {
            _quantity=value;
        }
        get {
            return _quantity;
        }
    }
    public int SellerId {
        set {
            _sellerId=value;
        }
        get {
            return _sellerId;
        }
    }
    public int Price {
        set {
            _price=value;
        }
        get {
            return _price;
        }
    }

    // public override string ToString()
    // {
    //     StringBuilder sb = new();
    //     sb.Append($"ID: {this.ListingId} | ItemID: {this.ItemId}");
    //     return sb.ToString();
    // }
}




