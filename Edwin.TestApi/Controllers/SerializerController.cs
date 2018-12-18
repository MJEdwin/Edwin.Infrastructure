using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edwin.Infrastructure.Serializer;
using Edwin.TestApi.Domain.User;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Edwin.TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SerializerController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public void Get()
        {
            var user = new User
            {
                Password = "123",
                UserName = "hello"
            };
            var serializer = new JsonSerializer<User>().Append(new EncodingSerializer("utf-8"));
            var result = serializer.Serialize(user);
            var back = serializer.Deserialize(result);
        }
    }
}
