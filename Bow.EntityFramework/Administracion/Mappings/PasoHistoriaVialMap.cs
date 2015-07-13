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
    public class PasoHistoriaVialMap : MultiTenantMap<PasoHistoriaVial>
    {
        public PasoHistoriaVialMap()
        {
            //Atributos

            Property(d => d.HistoriaVialId).IsRequired();

            Property(d => d.Nombre).HasMaxLength(512);
            Property(d => d.Nombre).IsRequired();

            Property(d => d.Descripcion).HasMaxLength(2058);
            Property(d => d.Descripcion).IsRequired();

            //Tabla
            ToTable("paso_historia_vial");
        }
    }
}
