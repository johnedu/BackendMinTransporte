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
    public class TipoVehiculoMap : MultiTenantMap<TipoVehiculo>
    {
        public TipoVehiculoMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            //Llaves Foráneas
            HasMany<ReporteCalificaciones>(tipoVehiculo => tipoVehiculo.ReportesCalificaciones)
              .WithRequired(reporteCalificaciones => reporteCalificaciones.TipoVehiculoReporte)
              .HasForeignKey(reporteCalificaciones => reporteCalificaciones.TipoVehiculoId)
              .WillCascadeOnDelete(true);

            //Tabla
            ToTable("tipo_vehiculo");
        }
    }
}
