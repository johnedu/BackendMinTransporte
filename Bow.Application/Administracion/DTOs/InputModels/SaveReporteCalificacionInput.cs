using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class SaveReporteCalificacionInput : IInputDto
    {
        [Required]
        public int TipoVehiculoId { get; set; }
        [MaxLength(100)]
        public string Placa { get; set; }
        [Required]
        [MaxLength(512)]
        public string Empresa { get; set; }
        [Required]
        [MaxLength(2048)]
        public string Observaciones { get; set; }
        public decimal Calificacion { get; set; }
        [Required]
        [MaxLength(512)]
        public string UrlImagen { get; set; }
    }
}
