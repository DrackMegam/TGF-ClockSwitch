using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ClockSwitchDbContext _context;

        public LoginController(ILogger<LoginController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{username}/{password}")]
        public bool Get(string username, string password)
        {
            UsuarioDto? userFound = _context.Usuario.Where(e => e.Username.Equals(username)).FirstOrDefault();
            if (userFound == null)
                return false; // No se ha encontrado el usuario.
            if (!password.Equals(userFound.Password))
                return false; // Comprobación de la contraseña.

            _logger.LogDebug("Se ha logeado el usuario <" + username + "> con password <" + password + ">");
            return true;
        }

        [HttpGet("GimmeId/{username}")]
        public int GetUserId(string username)
        {
            return _context.Usuario.Where(e => e.Username == username).Select(i => i.IdUsuario).FirstOrDefault();
        }

        [HttpGet("AmIAdmin/{username}")]
        public int AmIAdmin(string username)
        {
            return _context.Usuario.Where(e => e.Username == username).Select(i => i.IsAdmin).FirstOrDefault();
        }


    }
}
