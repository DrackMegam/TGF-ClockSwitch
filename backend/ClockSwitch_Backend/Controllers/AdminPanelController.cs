using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {

        private readonly ILogger<AdminPanelController> _logger;
        private readonly ClockSwitchDbContext _context;

        public AdminPanelController(ILogger<AdminPanelController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("AddPerson/{dni}/{nombre}/{apellido}/{descripcion}/{telefono}/{localidad}")]
        public bool AddPerson(string dni, string nombre, string apellido, string descripcion, int telefono, string localidad)
        {
            if (dni.Length != 9)
                return false;

            PersonaDto newPerson = new PersonaDto()
            {
                Dni = dni,
                Nombre = nombre ?? "", // Por si escribe valores nulos.
                Apellidos = apellido ?? "",
                Descripcion = descripcion ?? "",
                Telefono = telefono,
                Localidad = localidad ?? ""
            };

            try
            {
                _context.Persona.Add(newPerson);
                int resultado = _context.SaveChanges();
                _logger.LogDebug("Se ha ejecutado: AddPersona <" + resultado + ">");
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar insertar en la tabla <Persona>: " + e.ToString());
                return false;
            }
            return true;

        }

        [HttpGet("AddUser/{dniPersona}/{username}/{password}/{isAdminString}")]
        public bool AddUser(string dniPersona, string username, string password, string isAdminString)
        {

            if (username.Length == 0 || password.Length == 0)
                return false;

            bool isAdmin = isAdminString.Equals("true"); // Por URL solo recibe texto.

            UsuarioDto newUser = new UsuarioDto()
            {
                DniPersona = dniPersona ?? "",
                Username = username,
                Password = password,
                IsAdmin = isAdmin ? 1 : 0 // En C++ es un bool, pero lo que necesito es un 1 o un 0.
            };

            try
            {
                _context.Usuario.Add(newUser);
                int resultado = _context.SaveChanges();
                _logger.LogDebug("Se ha ejecutado: AddUser <" + resultado + ">");
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar insertar en la tabla <Usuario>: " + e.ToString());
                return false;
            }
            return true;
        }

        [HttpGet("AddTask/{nombre}/{descripcion}/{estado}")]
        public bool AddTask(string nombre, string descripcion, string estado)
        {
            if (nombre.Length == 0 || estado.Length == 0)
                return false;

            TareaDto newTask = new TareaDto()
            {
                Nombre = nombre,
                Descripcion = descripcion ?? "",
                Estado = estado
            };

            try
            {
                _context.Tarea.Add(newTask);
                int resultado = _context.SaveChanges();
                _logger.LogDebug("Se ha ejecutado: AddTask <" + resultado + ">");

            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar insertar en la tabla <Tarea>: " + e.ToString());
                return false;
            }
            return true;
        }

        [HttpGet("RemovePerson/{dni}")]
        public bool RemovePerson(string dni)
        {
            if (dni.Length != 9)
                return false;

            PersonaDto? targetPerson = _context.Persona.FirstOrDefault(x => x.Dni == dni);
            if (targetPerson == null)
                return false;

            try
            {
                _context.Persona.Remove(targetPerson);
                int resultado = _context.SaveChanges();
                _logger.LogDebug("Se ha ejecutado: RemovePerson <" + resultado + ">");
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar eliminar en la tabla <Persona>: " + e.ToString());
                return false;
            }
            return true;

        }

        [HttpGet("RemoveUser/{id}")]
        public bool RemoveUser(int id)
        {
            UsuarioDto? targetUser = _context.Usuario.FirstOrDefault(x => x.IdUsuario == id);
            if (targetUser == null)
                return false;

            try
            {
                _context.Usuario.Remove(targetUser);
                int resultado = _context.SaveChanges();
                _logger.LogDebug("Se ha ejecutado: RemoveUsuario <" + resultado + ">");
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar eliminar en la tabla <Usuario>: " + e.ToString());
                return false;
            }
            return true;
        }

        [HttpGet("RemoveTask/{id}")]
        public bool RemoveTask(int id)
        {
            TareaDto? targetTask = _context.Tarea.FirstOrDefault(x => x.IdTarea == id);
            if (targetTask == null)
                return false;

            try
            {
                // Pese existir un método "Update" lo mejor es modificar el objeto y guardar los cambios en el contexto.
                // El por qué no lo elimino es debido a que deseo tener un registro de TODAS las tareas. Cerradas, canceladas u olvidadas.
                targetTask.Estado = "Cancelada";
                int resultado = _context.SaveChanges();
                _logger.LogDebug("Se ha ejecutado: RemoveTask <" + resultado + ">");
            }
            catch (Exception e)
            {
                _logger.LogDebug("Error al intentar eliminar en la tabla <Tarea>: " + e.ToString());
                return false;
            }
            return true;
        }

        [HttpGet("GetPerson")]
        public List<PersonaDto> GetPerson()
        {
            return _context.Persona.ToList();
        }

        [HttpGet("GetUser/{option}")]
        public List<UsuarioDto> GetUser(string option)
        {
            return option switch
            {
                "all" => _context.Usuario.ToList(),
                "admins" => _context.Usuario.Where(e => e.IsAdmin == 1).ToList(),
                "noAdmins" => _context.Usuario.Where(e => e.IsAdmin == 0).ToList(),
                _ => _context.Usuario.ToList(),
            };
        }

        [HttpGet("GetTask/{status}")]
        public List<TareaDto> GetTask(string status)
        {
            // Pendiente - En progreso - Bloqueada - Completada - Cancelada
            return status switch
            {
                "Pendiente" => _context.Tarea.Where(e => e.Estado.Equals("Pendiente")).ToList(),
                "En progreso" => _context.Tarea.Where(e => e.Estado.Equals("En progreso")).ToList(),
                "Bloqueada" => _context.Tarea.Where(e => e.Estado.Equals("Bloqueada")).ToList(),
                "Completada" => _context.Tarea.Where(e => e.Estado.Equals("Completada")).ToList(),
                "Cancelada" => _context.Tarea.Where(e => e.Estado.Equals("Cancelada")).ToList(),
                "all" => _context.Tarea.ToList(),
                _ => _context.Tarea.ToList(),
            };
        }

        [HttpGet("Gimme1")]
        public int Gimme1()
        {
            return 1;
        }

        [HttpGet("Gimme2")]
        public int Gimme2()
        {
            return 2;
        }

    }
}
