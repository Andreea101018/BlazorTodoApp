using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController] //This attribute marks this class as a Web API controller, so that the Web API framework will know about our class.
[Route("[controller]")] // specifies the sub-URI to access this controller class

public class UsersController: ControllerBase //The class extends ControllerBase to get access to various utility methods.
{
    private readonly IUserLogic userLogic;

    public UsersController(IUserLogic userLogic) //Then a field variable, injected through the constructor, so we can get access to the application layer, i.e. the logic.
    {
        this.userLogic = userLogic;
    }
    
//The endpoint - should take the relevant data, pass it on to the logic layer, and return the result back to the client.
    
    [HttpPost] //we mark the method as [HttpPost] to say that POST requests to /users should hit this endpoint
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsynnc(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet] //We mark the method with [HttpGet] so that GET requests to this controller ends here.
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}