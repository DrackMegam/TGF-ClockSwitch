using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SemanaController : ControllerBase
    {

        private readonly ILogger<SemanaController> _logger;
        private readonly ClockSwitchDbContext _context;

        public SemanaController(ILogger<SemanaController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("{user}")]
        public List<HistorialDto> GetSemana(string user)
        {
            UsuarioDto? userFound = _context.Usuario.Where(e => e.Username.Equals(user)).FirstOrDefault();
            if (userFound == null)
                return new List<HistorialDto>();



            //_logger.LogDebug("Recuperando información de  <" + personFound.Nombre + "> con DNI <" + personFound.Dni + ">");
            return new List<HistorialDto>();

        }
    }
}
