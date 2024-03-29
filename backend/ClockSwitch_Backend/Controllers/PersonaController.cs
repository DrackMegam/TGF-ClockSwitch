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

        private readonly ILogger<PersonaController> _logger;
        private readonly ClockSwitchDbContext _context;

        public PersonaController(ILogger<PersonaController> logger, ClockSwitchDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<PersonaDto> Get()
        {
            List<PersonaDto> data = _context.Persona.ToList();

            return data.ToArray();
        }

        [HttpGet("{dni}")]
        public PersonaDto GetSinglePerson(string dni)
        {
            PersonaDto? personFound = _context.Persona.Where(e => e.Dni.Equals(dni)).FirstOrDefault();
            if (personFound == null)
                return new PersonaDto()
                {
                    Dni = "nulo",
                };
            _logger.LogDebug("Recuperando información de  <" + personFound.Nombre + "> con DNI <" + personFound.Dni + ">");
            return personFound;

        }
    }
}
