
using DataAccess;
using Models;

namespace Services;
public class ItemServices{

    private readonly IRepository _iRepo;


    public ItemServices(IRepository iRepo){
        _iRepo = iRepo;
    }
    public void sellItem(Sellinfo sellinfo) => _iRepo.sellItem(sellinfo);
    public void buyItem(Misc buyinfo) => _iRepo.buyItem(buyinfo);
}
