using System.Text;

namespace Models;
public class Misc
{
    private int _listingId;
    private int _quantity;
    private int _buyerId;
       
    public Misc(int listingId, int quantity, int buyerId)
    {
        _listingId=listingId;
        _quantity=quantity;
        _buyerId=buyerId;
    }
    public int ListingId {
        set {
            _listingId=value;
        }
        get {
            return _listingId;
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
    public int BuyerId {
        set {
            _buyerId=value;
        }
        get {
            return _buyerId;
        }
    }
    // public int SellerId {
    //     set {
    //         _sellerId=value;
    //     }
    //     get {
    //         return _sellerId;
    //     }
    // }

    // public override string ToString()
    // {
    //     StringBuilder sb = new();
    //     sb.Append($"ID: {this.ListingId} | ItemID: {this.ItemId}");
    //     return sb.ToString();
    // }
}




