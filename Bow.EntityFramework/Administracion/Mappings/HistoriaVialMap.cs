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
    public class HistoriaVialMap : MultiTenantMap<HistoriaVial>
    {
        public HistoriaVialMap()
        {
            //Atributos
            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            Property(d => d.Descripcion).HasMaxLength(2048);
            Property(d => d.Descripcion).IsRequired();

            Property(d => d.NombrePersona).HasMaxLength(512);

            Property(d => d.Url).HasMaxLength(2048);

            Property(d => d.EsActiva).IsRequired();

            //Tabla
            ToTable("historia_vial");
        }
    }
}
