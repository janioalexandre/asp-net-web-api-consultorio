﻿using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        private readonly IMapper _mapper;

        public PacienteController(IPacienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pacientes = await _repository.GetPacientesAsync();

            return pacientes.Any()
                ? Ok(pacientes)
                : BadRequest("Paciente não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _repository.GetPacientesByIdAsync(id);

            var pacienteRetorno = _mapper.Map<PacienteDetalhesDto>(paciente);

            return pacienteRetorno != null
                    ? Ok(pacienteRetorno)
                    : BadRequest("Paciente não encontrado.");
        }
    }
}