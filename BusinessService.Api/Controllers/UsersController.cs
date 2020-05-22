using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessService.Data.DBModel;
using BusinessService.Domain.Services;
using BusinessService.Domain.DomainModel;


namespace BusinessService.Api.Controllers
{
    [Authorize]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }        

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();                
                return Ok(users);                
            }
            catch
            {
                return new ConflictResult();
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))         
                return Forbid();
        
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}