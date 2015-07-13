using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class SaveReporteIncidentesInput : IInputDto
    {
        [Required]
        public int TipoReporteId { get; set; }
        [MaxLength(512)]
        public string Direccion { get; set; }
        [Required]
        [MaxLength(100)]
        public string Latitud { get; set; }
        [Required]
        [MaxLength(100)]
        public string Longitud { get; set; }
        [MaxLength(2048)]
        public string Observaciones { get; set; }
    }
}
