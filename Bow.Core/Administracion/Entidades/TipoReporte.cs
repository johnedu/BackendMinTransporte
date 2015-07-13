using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class TipoReporte : EntidadMultiTenant
    {
        public string Nombre { get; set; }

        public virtual ICollection<ReporteIncidentes> ReporteIncidentes { get; set; }

        public TipoReporte()
        {
            ReporteIncidentes = new List<ReporteIncidentes>();
        }
    }
}
