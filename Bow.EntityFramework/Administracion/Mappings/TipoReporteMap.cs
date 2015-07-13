using Bow.MappingsBase;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Mappings
{
    public class TipoReporteMap : MultiTenantMap<TipoReporte>
    {
        public TipoReporteMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<ReporteIncidentes>(tipoReporte => tipoReporte.ReporteIncidentes)
              .WithRequired(reporteIncidente => reporteIncidente.TipoReporteIncidente)
              .HasForeignKey(reporteIncidente => reporteIncidente.TipoReporteId)
              .WillCascadeOnDelete(true);

            //Tabla
            ToTable("tipo_reporte");
        }
    }
}
