using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_for__NUnit_Test.Model;
using User_for__NUnit_Test.Service;

namespace User_for__NUnit_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Iservice service;
        public UserController(Iservice service)
        {
            this.service=service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.GetAll());
        }
        [HttpGet("{Mobile_No}")]
        public IActionResult Get(string Mobile_No) 
        {
            return Ok(service.GetUserByMobileNo(Mobile_No));
        }
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            service.Add(user);
            return StatusCode(200, "New User added successfully.");
        }
        [HttpPut("{Mobile_No}")]
        public IActionResult Put(User user,string Mobile_No)
        {
            service.UpdateUser(Mobile_No,user);
            return StatusCode(200, "User Updated");
        }
        [HttpDelete]
        public IActionResult Delete(string Mobile_No)
        {
            service.DeleteUser(Mobile_No);
            return StatusCode(200, "User deleted");
        }
    }
}
