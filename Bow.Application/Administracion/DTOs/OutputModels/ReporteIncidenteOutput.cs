using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class ReporteIncidenteOutput : EntityDto, IOutputDto
    {
        public int TipoReporteId { get; set; }
        public string TipoReporteIncidente { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Observaciones { get; set; }
        public bool EsActivo { get; set; }
    }
}
