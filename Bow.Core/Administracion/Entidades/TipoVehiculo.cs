using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class TipoVehiculo : EntidadMultiTenant
    {
        public string Nombre { get; set; }

        public virtual ICollection<ReporteCalificaciones> ReportesCalificaciones { get; set; }

        public TipoVehiculo()
        {
            ReportesCalificaciones = new List<ReporteCalificaciones>();
        }
    }
}
