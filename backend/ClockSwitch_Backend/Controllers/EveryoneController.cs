using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EveryoneController : ControllerBase
    {

        private readonly ILogger<EveryoneController> _logger;
        private readonly ClockSwitchDbContext _context;

        public EveryoneController(ILogger<EveryoneController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("AllHours/{year}/{week}")]
        public List<HorasGlobalesDto> Get(int year, int week)
        {
            List<HorasGlobalesDto> horasGlobales = new List<HorasGlobalesDto>();
            List<int> userIds = _context.Usuario.Select(e => e.IdUsuario).ToList();
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
