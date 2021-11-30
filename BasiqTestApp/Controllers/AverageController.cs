using BasiqTestApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasiqTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AverageController : ControllerBase
    {

        private static Manager manager = new Manager();
        private readonly ILogger<AverageController> _logger;

        public AverageController(ILogger<AverageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{code}")]
        public async Task<string> GetAsync(int code)
        {
            var average = await manager.GetAverageValueAsync(Settings.DefaultUserId, code);
            return average.ToString();
        }
    }
}
