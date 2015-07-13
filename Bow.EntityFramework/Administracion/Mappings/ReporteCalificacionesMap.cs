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
    public class ReporteCalificacionesMap : MultiTenantMap<ReporteCalificaciones>
    {
        public ReporteCalificacionesMap()
        {
            //Atributos
            Property(d => d.Placa).HasMaxLength(50);
            Property(d => d.Placa).IsRequired();

            Property(d => d.Empresa).HasMaxLength(512);

            Property(d => d.Observaciones).HasMaxLength(2048);

            Property(d => d.Calificacion).IsRequired();

            Property(d => d.EsActiva).IsRequired();

            //Tabla
            ToTable("reporte_calificaciones");
        }
    }
}
