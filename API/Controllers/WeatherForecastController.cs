using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserServices _service;

    public UserController(ILogger<UserController> logger, UserServices service)
    {
        _logger = logger;
        _service = service;
    }


    [HttpPost]
    public ActionResult<User> Create(int id)
    {
        User user = new User();
        user.Id=id;
        return Created("/user-inventory", _service.ViewPersonalInventory(user));
    }

    // [HttpPut]
    // public ActionResult Put(WorkoutSession sessionToModify) {
    //     return Ok();
    // }
}
