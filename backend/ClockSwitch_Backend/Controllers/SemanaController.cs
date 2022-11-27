using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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


        [HttpGet("WeekSummary/{user}/{year}/{week}")]
        public List<HistorialDto> GetSemana(int user, int year, int week)
        {
            int actualYear = DateTime.Today.Year;
            CultureInfo myCI = new CultureInfo("es-ES"); // https://learn.microsoft.com/en-us/dotnet/api/system.globalization.calendar.getweekofyear?view=net-7.0
            int actualWeek = myCI.Calendar.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);

            List<HistorialDto> historial = _context.Historial.Where(e => e.Ano == year && e.Semana == week && e.IdUsuario == user).ToList();

            if (historial.Count == 0)
            {
                historial.Add(new HistorialDto
                {
                    Ano = 1928, // Añado una entrada para indicar que NO hay nada esta semana.
                    Semana = actualWeek,
                    IdUsuario = user,
                });
            }

            //_logger.LogDebug("Recuperando información de  <" + personFound.Nombre + "> con DNI <" + personFound.Dni + ">");
            return historial;
        }


        [HttpGet("WeekFullSummary/{user}/{year}/{week}")]
        public List<HistorialFullDto> GetFullSemana(int user, int year, int week)
        {
            List<HistorialFullDto> fullHistory = new List<HistorialFullDto>();
            List<HistorialDto> historial = GetSemana(user, year, week);


            foreach(var e in historial)
                fullHistory.Add(new HistorialFullDto
                {
                    Id = e.Id,
                    Ano = e.Ano,
                    Semana = e.Semana,
                    IdTarea = e.IdTarea,
                    IdUsuario = e.IdUsuario,
                    HorasLunes = e.HorasLunes,
                    HorasMartes = e.HorasMartes,
                    HorasMiercoles = e.HorasMiercoles,
                    HorasJueves = e.HorasJueves,
                    HorasViernes = e.HorasViernes,
                    HorasSabado = e.HorasSabado,
                    HorasDomingo = e.HorasDomingo,
                    NombreTarea = _context.Tarea.Where(i => i.IdTarea == e.IdTarea).FirstOrDefault()?.Nombre,
                    EstadoTarea = _context.Tarea.Where(i => i.IdTarea == e.IdTarea).FirstOrDefault()?.Estado,
                });;



            //_logger.LogDebug("Recuperando información de  <" + personFound.Nombre + "> con DNI <" + personFound.Dni + ">");
            return fullHistory;
        }

        [HttpGet("GetAvailableTask")]
        public List<TareaDto> GetTask()
        {
            return _context.Tarea.Where(e => !e.Estado.Equals("Cancelada")).ToList();
        }

        [HttpGet("GetSingleTask/{id}")]
        public TareaDto GetSingleTask(int id)
        {
            TareaDto? uniqueTask = _context.Tarea.Where(e => e.IdTarea == id).FirstOrDefault();

            return uniqueTask!; // Esto NUNCA va a ser nulo por que desde el FRONT no puede hacer la petición manualmente.
        }

        [HttpGet("AddFirstHistory/{idTarea}/{idUsuario}")]
        public bool AddFirstHistory(int idTarea, int idUsuario)
        {
            int actualYear = DateTime.Today.Year;
            CultureInfo myCI = new CultureInfo("es-ES"); // https://learn.microsoft.com/en-us/dotnet/api/system.globalization.calendar.getweekofyear?view=net-7.0
            int actualWeek = myCI.Calendar.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);
            try
            {
                _context.Historial.Add(new HistorialDto()
                {
                    IdTarea = idTarea,
                    IdUsuario = idUsuario,
                    Ano = actualYear,
                    Semana = actualWeek,
                    HorasLunes = 0,
                    HorasMartes = 0,
                    HorasMiercoles = 0,
                    HorasJueves = 0,
                    HorasViernes = 0,
                    HorasSabado = 0,
                    HorasDomingo = 0
                });
                _context.SaveChanges();
            }catch(Exception e)
            {
                _logger.LogDebug("Error al añadir primera entrada al historial:"+e);
                return false;
            }
            return true; 
        }
    }
}
