
using DataAccess;
using Models;

namespace Services;
public class ItemServices{

    private readonly IRepository _iRepo;


    public ItemServices(IRepository iRepo){
        _iRepo = iRepo;
    }
    public void sellItem(int[] sellinfo) => _iRepo.sellItem(sellinfo);
    public void buyItem(int[] buyinfo) => _iRepo.buyItem(buyinfo);
}
