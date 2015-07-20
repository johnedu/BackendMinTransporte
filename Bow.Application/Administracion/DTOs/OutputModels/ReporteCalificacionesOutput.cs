using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class ReporteCalificacionesOutput : EntityDto, IOutputDto
    {
        public int TipoVehiculoId { get; set; }
        public string TipoVehiculoReporte { get; set; }
        public string TipoReporteImagen { get; set; }
        public string Placa { get; set; }
        public string Empresa { get; set; }
        public string Observaciones { get; set; }
        public decimal Calificacion { get; set; }
        public bool EsActiva { get; set; }
    }
}
