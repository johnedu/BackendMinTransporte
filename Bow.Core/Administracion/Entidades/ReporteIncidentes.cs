using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class ReporteIncidentes : EntidadMultiTenant
    {
        public int TipoReporteId { get; set; }
        public TipoReporte TipoReporteIncidente { get; set; }
        public string Direccion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Observaciones { get; set; }
        public bool EsActivo { get; set; }
    }
}
