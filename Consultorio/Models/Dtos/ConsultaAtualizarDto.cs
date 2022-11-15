using System;

namespace Consultorio.Models.Dtos
{
    public class ConsultaAtualziarDto
    {
        public DateTime DataHorario { get; set; }
        public int Status { get; set; }
        public decimal Preco { get; set; }
        public int ProfissionalId { get; set; }
    }
}