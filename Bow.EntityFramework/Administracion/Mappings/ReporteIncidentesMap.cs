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
    public class ReporteIncidentesMap : MultiTenantMap<ReporteIncidentes>
    {
        public ReporteIncidentesMap()
        {
            //Atributos
            Property(d => d.Direccion).HasMaxLength(512);

            Property(d => d.Latitud).HasMaxLength(100);
            Property(d => d.Latitud).IsRequired();
            Property(d => d.Longitud).HasMaxLength(100);
            Property(d => d.Longitud).IsRequired();

            Property(d => d.Observaciones).HasMaxLength(2048);

            Property(d => d.EsActivo).IsRequired();

            //Tabla
            ToTable("reporte_incidentes");
        }
    }
}
