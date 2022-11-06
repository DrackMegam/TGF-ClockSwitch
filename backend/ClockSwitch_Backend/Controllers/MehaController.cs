using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MehaController : ControllerBase
    {

        private readonly ILogger<MehaController> _logger;

        public MehaController(ILogger<MehaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Meha> Get()
        {
            List<Meha> data = new List<Meha>();
            data.Add(new Meha
            {
                Id = 1928,
                Name = "Mehamius"
            });
            data.Add(new Meha
            {
                Id = 497,
                Name = "Rrcr"
            });


            return data.ToArray();
        }
    }
}
