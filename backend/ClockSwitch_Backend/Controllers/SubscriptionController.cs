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


        [HttpGet("GetSubsData/{userId}/{year}/{week}")]
        public List<HorasGlobalesDto> GetSubsData(int userId, int year, int week)
        {
            List<HorasGlobalesDto> horasGlobales = new List<HorasGlobalesDto>();
            List<int> userIds = _context.Suscripcion.Where(e => e.IdSuscriptor == userId).Select(i => i.IdObjetivo).ToList();
            foreach (int id in userIds)
            {
                List<HistorialDto> historial = _context.Historial.Where(e => e.Ano == year && e.Semana == week && e.IdUsuario == id).ToList(); // Historial de la semana actual
                HorasGlobalesDto sumCurrentWeek = new HorasGlobalesDto()
                { // Inicializo los valores para poder ir haciendo el sumatorio. Si empieza como null, se quedaría como null.
                    SumatorioLunes = 0,
                    SumatorioMartes = 0,
                    SumatorioMiercoles = 0,
                    SumatorioJueves = 0,
                    SumatorioViernes = 0,
                    SumatorioSabado = 0,
                    SumatorioDomingo = 0
                };
                foreach (HistorialDto item in historial)
                { // Sumatorio de todo el historial en esa semana.
                    sumCurrentWeek.SumatorioLunes += item.HorasLunes;
                    sumCurrentWeek.SumatorioMartes += item.HorasMartes;
                    sumCurrentWeek.SumatorioMiercoles += item.HorasMiercoles;
                    sumCurrentWeek.SumatorioJueves += item.HorasJueves;
                    sumCurrentWeek.SumatorioViernes += item.HorasViernes;
                    sumCurrentWeek.SumatorioSabado += item.HorasSabado;
                    sumCurrentWeek.SumatorioDomingo += item.HorasDomingo;
                }
                // Datos independiente a que semana sea.
                sumCurrentWeek.IdUsuario = id;
                sumCurrentWeek.NombreUsuario = _context.Usuario.Where(e => e.IdUsuario == id).Select(i => i.Username).FirstOrDefault();
                sumCurrentWeek.Total = (sumCurrentWeek.SumatorioLunes + sumCurrentWeek.SumatorioMartes + sumCurrentWeek.SumatorioMiercoles
                    + sumCurrentWeek.SumatorioJueves + sumCurrentWeek.SumatorioViernes + sumCurrentWeek.SumatorioSabado + sumCurrentWeek.SumatorioDomingo);
                horasGlobales.Add(sumCurrentWeek);
            }
            return horasGlobales.OrderByDescending(i => i.Total).ToList();
        }
    }
}
