using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepository _repository;
        private readonly IMapper _mapper;

        public ConsultaController(IConsultaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ConsultaParams parametro)
        {
            var consultas = await _repository.GetConsulta(parametro);

            var consultasRetorno = _mapper.Map<IEnumerable<ConsultaDetalhesDto>>(consultas);

            return consultasRetorno.Any()
                ? Ok(consultasRetorno) 
                : BadRequest("Nenhuma consulta encontrada com os parâmetros informados!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest("Consulta inválida");

            var consulta = await _repository.GetConsultaById(id);

            var consultaRetorno = _mapper.Map<ConsultaDetalhesDto>(consulta);

            return consultaRetorno != null
                ? Ok(consultaRetorno)
                : BadRequest("Consulta não encontrada.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConsultaAdicionarDto consulta)
        {
            if (consulta == null) return BadRequest("Dados inválidos");

            var consultaAdicionar = _mapper.Map<Consulta>(consulta);

            _repository.Add(consultaAdicionar);

            return await _repository.SaveChangesAsync()
                ? Ok("Consulta realizada") 
                : BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ConsultaAtualziarDto consulta)
        {
            if (consulta == null) return BadRequest("Dados inválidos");

            var consultaBanco = await _repository.GetConsultaById(id);

            if (consultaBanco == null) BadRequest("Consulta não existe na base de dados");

            if(consulta.DataHorario == new DateTime())
            {
                consulta.DataHorario = consultaBanco.DataHorario;
            }

            var consultaAtualizar = _mapper.Map(consulta, consultaBanco);

            _repository.Update(consultaAtualizar);

            return await _repository.SaveChangesAsync()
                ? Ok("Consulta atualizada com sucesso!")
                : BadRequest("Erro ao atualizar consulta");
        }
    }
}