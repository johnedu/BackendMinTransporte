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
        public string TipoCategoria { get; set; }
        public string UrlImagen { get; set; }

        public virtual ICollection<ReporteIncidentes> ReporteIncidentes { get; set; }
        public virtual ICollection<HistoriaVial> HistoriasViales { get; set; }

        public TipoReporte()
        {
            ReporteIncidentes = new List<ReporteIncidentes>();
            HistoriasViales = new List<HistoriaVial>();
        }
    }
}
