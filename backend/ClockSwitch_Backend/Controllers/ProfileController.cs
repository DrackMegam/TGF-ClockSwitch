using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {

        private readonly ILogger<SubscriptionController> _logger;
        private readonly ClockSwitchDbContext _context;

        public SubscriptionController(ILogger<SubscriptionController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("GetCurrentSubscriptions/{userId}")]
        public List<UsuarioDto> GetCurrentSubscriptions(int userId)
        {
            List<int> currentSubscription = _context.Suscripcion.Where(e => e.IdSuscriptor == userId).Select(i => i.IdObjetivo).ToList();
            List<UsuarioDto> currentSubsData = new List<UsuarioDto>();

            foreach (int idSub in currentSubscription)
                currentSubsData.Add(_context.Usuario.Where(e => e.IdUsuario == idSub).FirstOrDefault()!);

            return currentSubsData;
        }

        [HttpGet("GetAvailableSubscriptions/{userId}")]
        public List<UsuarioDto> GetAvailableSubscriptions(int userId)
        {
            List<int> currentSubscription = _context.Suscripcion.Where(e => e.IdSuscriptor == userId).Select(i => i.IdObjetivo).ToList();
            List<int> allIds = _context.Usuario.Select(e => e.IdUsuario).ToList();
            List<int> availableSubscription = allIds.Except(currentSubscription).ToList(); // Diferencia entre una lista y la otra.

            List<UsuarioDto> currentSubsData = new List<UsuarioDto>();

            foreach (int id in availableSubscription)
                currentSubsData.Add(_context.Usuario.Where(e => e.IdUsuario == id).FirstOrDefault()!);

            return currentSubsData;
        }

        [HttpGet("Suscribe/{userId}/{idTarget}")]
        public bool Suscribe(int userId, int idTarget)
        {
            try
            {
                _context.Suscripcion.Add(new SuscripcionDto()
                {
                    IdSuscriptor = userId,
                    IdObjetivo = idTarget
                });
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar insertar en la tabla <Suscripción>: " + e.ToString());
                return false;
            }

            return true;
        }

        [HttpGet("Unsuscribe/{userId}/{idTarget}")]
        public bool Unsuscribe(int userId, int idTarget)
        {
            try
            {
                _context.Suscripcion.Remove(_context.Suscripcion.Where(e => e.IdSuscriptor == userId && e.IdObjetivo == idTarget).FirstOrDefault()!);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar eliminar en la tabla <Suscripción>: " + e.ToString());
                return false;
            }

            return true;
        }
    }
}
