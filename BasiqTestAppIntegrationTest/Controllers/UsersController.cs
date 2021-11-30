using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BasiqTestAppIntegrationTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<BasiqFakeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BasiqFakeController>/5
        [HttpGet("{userId}/transactions")]
        public string Get(string userId)
        {
            return TestCaseRunner.CurrentTestCase.JsonData;
            //return "{\"type\":\"list\",\"count\":500,\"size\":1494,\"data\":[{\"amount\":\"-19.20\",\"subClass\":{\"title\":\"Cafes, Restaurants and Takeaway Food Services\",\"code\":\"451\"}},{\"amount\":\"-144.35\",\"subClass\":{\"title\":\"Supermarket and Grocery Stores\",\"code\":\"411\"}},{\"amount\":\"-17.30\",\"subClass\":{\"title\":\"Cafes, Restaurants and Takeaway Food Services\",\"code\":\"451\"}}],\"links\":{\"next\":\"\"}}";
        }

        // POST api/<BasiqFakeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BasiqFakeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BasiqFakeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
