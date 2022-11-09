﻿using ClockSwitch_Backend.Context;
using ClockSwitch_Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClockSwitch_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {

        private readonly ILogger<MehaController> _logger;
        private readonly ClockSwitchDbContext _context;

        public PersonaController(ILogger<MehaController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context; // Inyecto el contexto de mi BBDD en el controllador.
        }

        [HttpGet]
        public IEnumerable<PersonaDto> Get()
        {
            List<PersonaDto> data = _context.Persona.ToList();


            //data.Add(new PersonaDto
            //{
            //    Id = 192,
            //    Name = "SoyPersona"
            //});
            //data.Add(new PersonaDto
            //{
            //    Id = 497,
            //    Name = "DesdeDB"
            //});


            return data.ToArray();
        }
    }
}
