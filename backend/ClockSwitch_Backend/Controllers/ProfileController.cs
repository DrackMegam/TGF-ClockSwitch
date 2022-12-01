using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly ClockSwitchDbContext _context;

        public ProfileController(ILogger<ProfileController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("GetPerson/{dni}")]
        public PersonaDto GetPerson(string dni)
        {
            return _context.Persona.Where(i => i.Dni == dni).FirstOrDefault()!;
        }

        [HttpGet("GetOwner/{userId}")]
        public UsuarioDto GetOwner(int userId)
        {
            UsuarioDto? userData = _context.Usuario.Where(i => i.IdUsuario == userId).FirstOrDefault();

            if (userData == null)
                return new UsuarioDto() { DniPersona = "SYSTEM" }; // Usuario independiente a ninguna persona física.

            return userData;
        }

        [HttpGet("GetUsersOwned/{dni}")]
        public List<UsuarioDto> GetUsersOwned(string dni)
        {
            return _context.Usuario.Where(i => i.DniPersona == dni).ToList();
        }
    }
}
